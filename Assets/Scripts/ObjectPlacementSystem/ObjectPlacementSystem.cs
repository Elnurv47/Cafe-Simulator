using Utils;
using System;
using GridSystem;
using UnityEngine;

public class ObjectPlacementSystem : MonoBehaviour
{
    private GameObject _spawnedObject;

    [SerializeField] private GridXZ _objectPlacementGrid;
    [SerializeField] private LayerMask _objectPlacementNodeLayerMask;

    public static Action<IPlaceableObject> OnObjectPlaced;

    private void Update()
    {
        if (_spawnedObject == null) return;

        Vector3 mousePosition = Utility.GetMouseWorldPosition3D(_objectPlacementNodeLayerMask);

        _spawnedObject.transform.position = new Vector3(mousePosition.x, 2f, mousePosition.z);

        if (Input.GetMouseButtonDown(0))
        {
            ObjectPlacementSystemNode clickedNode = _objectPlacementGrid.GetNode(mousePosition) as ObjectPlacementSystemNode;

            IPlaceableObject objectToPlace = _spawnedObject.GetComponent<IPlaceableObject>();

            if (objectToPlace.CanPlace(_objectPlacementGrid, clickedNode))
            {
                objectToPlace.MoveTo(_objectPlacementGrid.GetNodeCenter(mousePosition) + Vector3.up * 2f);
                clickedNode.PlacedObject = objectToPlace;
                _spawnedObject = null;
            }

            /*ObjectPlacementSystemNode clickedNode = _objectPlacementGrid.GetNode(mousePosition) as ObjectPlacementSystemNode;
            if (!clickedNode.IsEmpty) return;

            _spawnedObject.transform.position = _objectPlacementGrid.GetNodeCenter(mousePosition) + Vector3.up * 2f;
            clickedNode.PlacedObject = _spawnedObject;
            _spawnedObject = null;*/
        }
    }

    public void TryPlaceObject(GameObject placeObject)
    {
        Vector3 mousePosition = Utility.GetMouseWorldPosition3D();
        _spawnedObject = Instantiate(placeObject, mousePosition, Quaternion.identity);
        OnObjectPlaced?.Invoke(_spawnedObject.GetComponent<IPlaceableObject>());
    }
}

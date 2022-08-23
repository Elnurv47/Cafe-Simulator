using Utils;
using GridSystem;   
using UnityEngine;

public class ObjectPlacementSystem : MonoBehaviour
{
    private GameObject _spawnedObject;
    [SerializeField] private GridXZ _objectPlacementGrid;
    [SerializeField] private LayerMask _objectPlacementNodeLayerMask;

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Utility.GetMouseWorldPosition3D();
            Debug.Log("Click Position: " + clickPosition);
            Node node = _objectPlacementGrid.GetNode(clickPosition);
            node.gameObject.GetComponent<Renderer>().material.color = Color.red;

        }*/

        if (_spawnedObject == null) return;

        Vector3 mousePosition = Utility.GetMouseWorldPosition3D(_objectPlacementNodeLayerMask);

        Debug.Log(mousePosition);

        _spawnedObject.transform.position = new Vector3(mousePosition.x, 2f, mousePosition.z);

        if (Input.GetMouseButtonDown(0))
        {
            _spawnedObject.transform.position = _objectPlacementGrid.GetNodeCenter(mousePosition) + Vector3.up * 2f;
            _spawnedObject = null;
        }
    }

    public void TryPlaceObject(GameObject placeObject)
    {
        Vector3 mousePosition = Utility.GetMouseWorldPosition3D();
        _spawnedObject = Instantiate(placeObject, mousePosition, Quaternion.identity);
    }
}

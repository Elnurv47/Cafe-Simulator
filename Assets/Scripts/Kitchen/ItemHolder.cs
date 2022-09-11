using TaskSystem;
using UnityEngine;

public class ItemHolder : MonoBehaviour, IItemHolder, ITaskObject
{
    private Plate _plate;

    [SerializeField] private Transform _platePositionTransform;
    [SerializeField] private Transform _interactionPoint;

    public bool CanPut(StorableItem item)
    {
        return _plate == null && item is Plate;
    }

    public void Put(StorableItem storableItem)
    {
        if (storableItem is Plate)
        {
            _plate = storableItem as Plate;
            _plate.transform.position = _platePositionTransform.position;
            _plate.transform.SetParent(_platePositionTransform);
        }
        else
        {
            Debug.Log("Only plates can be placed..");
        }
    }

    public Task GetTask()
    {
        return new PutItemToContainerTask(_interactionPoint.position, this);
    }
}

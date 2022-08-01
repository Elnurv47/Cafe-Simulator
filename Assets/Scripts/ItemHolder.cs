using TaskSystem;
using UnityEngine;

public interface IItemHolder
{
    bool CanPut(StorableItem item);
    void Put(StorableItem item);
}

public class ItemHolder : MonoBehaviour, IItemHolder, ITaskObject
{
    private Plate _plate;

    [SerializeField] private Transform _platePositionTransform;

    public bool CanPut(StorableItem item)
    {
        return true;
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
        return new PutItemToContainerTask(_platePositionTransform.position, this);
    }
}

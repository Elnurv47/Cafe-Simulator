using TaskSystem;
using UnityEngine;

public class Plate : StorableItem, IItemHolder, ITaskObject
{
    private StorableItem _item;
    [SerializeField] private Transform _itemSpawnTransform;

    public bool CanPut(StorableItem storableItem)
    {
        return true;
    }

    public void Put(StorableItem item)
    {
        _item = item;
        _item.transform.position = _itemSpawnTransform.position;
        _item.transform.SetParent(_itemSpawnTransform);
    }

    public Task GetTask()
    {
        return new PutItemToContainerTask(_itemSpawnTransform.position, this);
    }
}

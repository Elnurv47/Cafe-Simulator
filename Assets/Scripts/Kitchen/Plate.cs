using TaskSystem;
using UnityEngine;

public class Plate : StorableItem, IItemHolder, ITaskObject
{
    private StorableItem _storedItem;

    [SerializeField] private Transform _itemSpawnTransform;

    public bool CanPut(StorableItem storableItem)
    {
        return _storedItem == null && !(storableItem is Plate);
    }

    public void Put(StorableItem storableItem)
    {
        _storedItem = storableItem;
        _storedItem.transform.position = _itemSpawnTransform.position;
        _storedItem.transform.SetParent(_itemSpawnTransform);
    }

    public Task GetTask()
    {
        return new PutItemToContainerTask(_itemSpawnTransform.position, this);
    }
}

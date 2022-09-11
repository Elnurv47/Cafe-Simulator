using TaskSystem;
using UnityEngine;

public class Crate : MonoBehaviour, IContainer, ITaskObject
{
    [SerializeField] private StorableItem _storableItem;
    [SerializeField] private int _storableItemAmount = 1000;
    [SerializeField] private Transform _interactionPoint;

    public bool ContainsFood()
    {
        return _storableItemAmount > 0;
    }

    public StorableItem GetStorableItem()
    {
        _storableItemAmount--;
        return Instantiate(_storableItem);
    }

    public Task GetTask()
    {
        Task task = new TakeItemFromContainerTask(_interactionPoint.position, this);
        return task;
    }
}

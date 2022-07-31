using TaskSystem;
using UnityEngine;

public class Crate : MonoBehaviour, IContainer, ITaskObject
{
    public int ID;

    [SerializeField] private Food _food;
    [SerializeField] private int _foodAmount = 1000;
    [SerializeField] private Transform _interactionPoint;

    public bool ContainsFood()
    {
        return _foodAmount > 0;
    }

    public IConsumable GetConsumable()
    {
        _foodAmount--;
        return Instantiate(_food);
    }

    public Task GetTask()
    {
        Task task = new TakeFoodFromContainerTask(_interactionPoint.position, this);
        return task;
    }
}

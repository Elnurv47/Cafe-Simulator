using UnityEngine;

namespace TaskSystem
{
    public class Plate : MonoBehaviour, IFoodHolder, ITaskObject
    {
        private IConsumable _holdConsumable;

        public int ID;

        [SerializeField] private Transform _frontInteractionPoint;
        [SerializeField] private Transform _backInteractionPoint;
        [SerializeField] private Transform _consumableSpawnPoint;

        public void PutItem(IConsumable consumable)
        {
            _holdConsumable = consumable;
            GameObject consumableObject = _holdConsumable.GetObject();
            consumableObject.transform.position = _consumableSpawnPoint.position;
            consumableObject.transform.SetParent(_consumableSpawnPoint);
        }

        public Task GetTask()
        {
            Task task = new PutItemToContainerTask(_frontInteractionPoint.position, this);
            return task;
        }
    }
}

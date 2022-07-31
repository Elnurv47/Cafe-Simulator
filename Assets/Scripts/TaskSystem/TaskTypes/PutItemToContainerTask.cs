using System;
using UnityEngine;

namespace TaskSystem
{
    public class PutItemToContainerTask : Task
    {
        private IFoodHolder _foodHolder;

        public PutItemToContainerTask(Vector3 targetPosition, IFoodHolder foodHolder) : base(targetPosition)
        {
            _foodHolder = foodHolder;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(Worker executor, Action onFinished)
        {
            executor.MoveTo(TargetPosition, () =>
            {
                Debug.Log("Arrived at position: " + TargetPosition);

                IConsumable consumable = executor.GetHoldConsumable();
                _foodHolder.PutItem(consumable);

            });
            
            onFinished();
        }
    }
}

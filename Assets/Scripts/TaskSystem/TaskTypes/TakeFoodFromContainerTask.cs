using System;
using UnityEngine;

namespace TaskSystem
{
    public class TakeFoodFromContainerTask : Task
    {
        private IContainer _container;

        public TakeFoodFromContainerTask(Vector3 targetPosition, IContainer container) : base(targetPosition)
        {
            _container = container;
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

                if (_container.ContainsFood())
                {
                    IConsumable consumable = _container.GetConsumable();
                    executor.HoldItem(consumable);
                }

                onFinished();
            });
        }
    }
}

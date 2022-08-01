using System;
using UnityEngine;

namespace TaskSystem
{
    public class TakeItemFromContainerTask : Task
    {
        private IContainer _container;

        public TakeItemFromContainerTask(Vector3 targetPosition, IContainer container) : base(targetPosition)
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
                    StorableItem storableItem = _container.GetStorableItem();
                    executor.HoldItem(storableItem);
                }

                onFinished();
            });
        }
    }
}

using System;
using UnityEngine;

namespace TaskSystem
{
    public class PutItemToContainerTask : Task
    {
        private IItemHolder _itemHolder;

        public PutItemToContainerTask(Vector3 targetPosition, IItemHolder itemHolder) : base(targetPosition)
        {
            _itemHolder = itemHolder;
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

                StorableItem storableItem = executor.GetHoldItem();
                if (_itemHolder.CanPut(storableItem))
                {
                    _itemHolder.Put(storableItem);
                }

                onFinished();
            });

        }
    }
}

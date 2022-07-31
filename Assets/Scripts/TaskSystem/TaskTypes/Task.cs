using System;
using UnityEngine;

namespace TaskSystem
{
    public abstract class Task
    {
        private Vector3 _targetPosition;
        public Vector3 TargetPosition { get => _targetPosition; }

        public Task(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public abstract bool CanExecute();

        public abstract void Execute(Worker executor, Action onFinished);
    }
}

using System;
using UnityEngine;
using System.Collections.Generic;

namespace TaskSystem
{
    public class Worker : MonoBehaviour
    {
        private bool _isExecutingTask;
        private WorkerVisual _workerVisual;
        private Queue<Task> _tasks;
        private IConsumable _holdConsumable;
        private WorkerMovement _movement;

        [SerializeField] private Transform _holder;

        public Vector3 Position { get => transform.position; set => transform.position = value; }

        private void Awake()
        {
            _workerVisual = new WorkerVisual(this);
            _tasks = new Queue<Task>();
            _movement = new WorkerMovement(this);
        }

        public void MoveTo(Vector3 targetPosition, Action onArrived)
        {
            StartCoroutine(_movement.MoveToCoroutine(targetPosition, onArrived));
        }

        public void HoldItem(IConsumable consumable)
        {
            var consumableObjectTransform = consumable.GetObject().transform;
            consumableObjectTransform.position = _holder.transform.position;
            consumableObjectTransform.SetParent(_holder);
            _holdConsumable = consumable;
        }

        public IConsumable GetHoldConsumable()
        {
            IConsumable holdConsumable = _holdConsumable;
            _holdConsumable = null;
            return holdConsumable;
        }

        public void AddTask(Task task)
        {
            _tasks.Enqueue(task);

            if (!_isExecutingTask)
            {
                ExecuteNextTask();
            }
        }

        private void ExecuteNextTask()
        {
            if (_tasks.Count == 0)
            {
                _isExecutingTask = false;
                return;
            }

            Task currentTask = _tasks.Dequeue();

            if (currentTask.CanExecute())
            {
                _isExecutingTask = true;

                currentTask.Execute(this, () =>
                {
                    Debug.Log("Task finished: ");
                    ExecuteNextTask();
                });
            }
            else
            {
                Debug.Log("Can't execute the task");
            }
        }

        public void ChangeColorTo(Color color) { _workerVisual.ChangeColorTo(color); }
    }
}

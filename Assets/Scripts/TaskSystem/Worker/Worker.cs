using UnityEngine;
using System.Collections.Generic;

namespace TaskSystem
{
    public class Worker : Character
    {
        private bool _isExecutingTask;
        private WorkerVisual _workerVisual;
        private Queue<Task> _tasks;

        private Plate _holdPlate;

        [SerializeField] private Transform _holder;

        public Vector3 Position { get => transform.position; set => transform.position = value; }

        private void Awake()
        {
            _workerVisual = new WorkerVisual(this);
            _tasks = new Queue<Task>();
        }

        public void HoldItem(StorableItem storableItem)
        {
            if (storableItem is Plate)
            {
                _holdPlate = storableItem as Plate;
                var holdItemTransform = storableItem.GetObject().transform;
                holdItemTransform.position = _holder.transform.position;
                holdItemTransform.SetParent(_holder);
            }
            else
            {
                if (_holdPlate.CanPut(storableItem)) _holdPlate.Put(storableItem);
            }
        }

        public Plate GetHoldPlate()
        {
            Plate plate = _holdPlate;
            _holdPlate = null;
            return plate;
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

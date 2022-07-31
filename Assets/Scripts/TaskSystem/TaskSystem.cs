using Utils;
using UnityEngine;

namespace TaskSystem
{
    public class TaskSystem : MonoBehaviour
    {
        private Worker _selectedWorker;

        [SerializeField] private Worker _workerPrefab;
        [SerializeField] private Vector3 _workerSpawnPosition;
        [SerializeField] private Vector3 _workerSpawnRotation;

        private void Start()
        {
            Instantiate(_workerPrefab, _workerSpawnPosition, Quaternion.Euler(_workerSpawnRotation));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject clickedObject = Utility.GetClickedObject3D();

                if (clickedObject.TryGetComponent(out Worker worker))
                {
                    _selectedWorker = worker;
                    _selectedWorker.ChangeColorTo(Color.green);
                    return;
                }

                if (clickedObject.TryGetComponent(out ITaskObject taskObject))
                {
                    _selectedWorker.AddTask(taskObject.GetTask());
                }
            }
        }
    }
}

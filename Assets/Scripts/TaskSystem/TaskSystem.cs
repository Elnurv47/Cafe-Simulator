using Utils;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameObject clickedObject = Utility.GetClickedObject3D();

                Worker worker = clickedObject.GetComponent<Worker>();

                if (worker != null)
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

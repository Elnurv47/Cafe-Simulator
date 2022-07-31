using UnityEngine;

namespace TaskSystem
{
    public class WorkerVisual
    {
        private Worker _worker;
        private Renderer[] _renderers;

        public WorkerVisual(Worker worker)
        {
            _worker = worker;
            _renderers = worker.gameObject.GetComponentsInChildren<Renderer>();
        }

        public void ChangeColorTo(Color color)
        {
            foreach (Renderer renderer in _renderers)
            {
                renderer.material.color = color;
            }
        }
    }
}

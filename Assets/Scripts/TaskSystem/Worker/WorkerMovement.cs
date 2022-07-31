using System;
using UnityEngine;
using System.Collections;

namespace TaskSystem
{
    public class WorkerMovement
    {
        private Worker _worker;

        private int _speed = 30;

        public WorkerMovement(Worker worker)
        {
            _worker = worker;
        }

        public IEnumerator MoveToCoroutine(Vector3 targetPosition, Action onArrived)
        {
            Path path = Pathfinding.Instance.FindPath(_worker.Position, targetPosition);

            foreach (PathNode node in path.Nodes)
            {
                while (GetDistance(_worker.Position, node.Position) > 0.1)
                {
                    Vector3 newPosition = new Vector3(node.Position.x, _worker.Position.y, node.Position.z);
                    _worker.Position = Vector3.MoveTowards(_worker.Position, newPosition, Time.deltaTime * _speed);
                    yield return Time.deltaTime;
                }
            }

            onArrived();
        }

        private float GetDistance(Vector3 firstPosition, Vector3 secondPosition)
        {
            return (float)Math.Pow(firstPosition.x - secondPosition.x, 2) + (float)Math.Pow(firstPosition.z - secondPosition.z, 2);
        }
    }
}

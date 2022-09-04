using System;
using UnityEngine;
using System.Collections;

namespace TaskSystem
{
    public class Movement
    {
        private static int _speed = 30;

        public static IEnumerator MoveToCoroutine(GameObject objectToMove, Vector3 targetPosition, Action onArrived)
        {
            Path path = Pathfinding.Instance.FindPath(objectToMove.transform.position, targetPosition);

            foreach (PathNode node in path.Nodes)
            {
                while (GetDistance(objectToMove.transform.position, node.Position) > 0.1)
                {
                    Vector3 newPosition = new Vector3(node.Position.x, objectToMove.transform.position.y, node.Position.z);
                    objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, newPosition, Time.deltaTime * _speed);
                    yield return Time.deltaTime;
                }
            }

            onArrived();
        }

        private static float GetDistance(Vector3 firstPosition, Vector3 secondPosition)
        {
            return (float)Math.Pow(firstPosition.x - secondPosition.x, 2) + (float)Math.Pow(firstPosition.z - secondPosition.z, 2);
        }
    }
}

using System;
using UnityEngine;
using System.Collections;

namespace TaskSystem
{
    public class Movement
    {
        private static int _speed = 30;
        private static int _rotationSpeed = 500;

        public static IEnumerator MoveToCoroutine(GameObject objectToMove, Vector3 targetPosition, Action onArrived)
        {
            Path path = Pathfinding.Instance.FindPath(objectToMove.transform.position, targetPosition);

            foreach (PathNode node in path.Nodes)
            {
                while (GetDistance(objectToMove.transform.position, node.Position) > 0.1)
                {
                    Vector3 newPosition = new Vector3(node.Position.x, objectToMove.transform.position.y, node.Position.z);
                    objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, newPosition, Time.deltaTime * _speed);

                    Vector3 movementDirection = newPosition - objectToMove.transform.position;
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    objectToMove.transform.rotation = Quaternion.RotateTowards(objectToMove.transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);

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

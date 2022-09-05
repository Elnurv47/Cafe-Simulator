using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static int _speed = 30;
    private static int _rotationSpeed = 500;

    public void MoveTo(Vector3 targetPosition, Action onArrived)
    {
        StartCoroutine(MoveToCoroutine(targetPosition, onArrived));
    }

    private IEnumerator MoveToCoroutine(Vector3 targetPosition, Action onArrived)
    {
        Path path = Pathfinding.Instance.FindPath(transform.position, targetPosition);

        foreach (PathNode node in path.Nodes)
        {
            while (GetDistance(transform.position, node.Position) > 0.1)
            {
                Vector3 newPosition = new Vector3(node.Position.x, transform.position.y, node.Position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * _speed);

                Vector3 movementDirection = newPosition - transform.position;
                RotateTo(movementDirection);

                yield return Time.deltaTime;
            }
        }

        onArrived();
    }

    private float GetDistance(Vector3 firstPosition, Vector3 secondPosition)
    {
        return (float)Math.Pow(firstPosition.x - secondPosition.x, 2) + (float)Math.Pow(firstPosition.z - secondPosition.z, 2);
    }

    public void RotateTo(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }
}

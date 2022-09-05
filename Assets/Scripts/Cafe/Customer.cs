using System;
using TaskSystem;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Table _seatedTable;

    public Table SeatedTable { get => _seatedTable; private set => _seatedTable = value; }

    public void GoToCafe()
    {
        MoveTo(gameObject, Cafe.EntrancePosition, () =>
        {
            Chair availableChair = Cafe.FindAvailableChair();

            if (availableChair == null) return;

            availableChair.SeatedCustomer = this;

            MoveTo(gameObject, availableChair.Position, () =>
            {
                Seat(availableChair);
                Order();
            });
        });
    }

    private void Seat(Chair chair)
    {
        transform.position = chair.SittingPosition;
        transform.forward = chair.ConnectedTable.transform.position - transform.position;
    }

    private void Order()
    {
        Debug.Log("Ordering");
    }

    private void MoveTo(GameObject gameObject, Vector3 position, Action onArrived)
    {
        StartCoroutine(Movement.MoveToCoroutine(gameObject, position, onArrived));
    }
}

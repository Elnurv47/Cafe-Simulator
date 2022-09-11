using TaskSystem;
using UnityEngine;

public class Customer : Character
{
    private Table _seatedTable;
    public Table SeatedTable { get => _seatedTable; private set => _seatedTable = value; }

    private Order _order;
    public Order Order { get => _order; set => _order = value; }

    [SerializeField] private TextFacingCamera3D _textFacingCamera3D;

    public void GoToCafe()
    {
        MoveTo(Cafe.EntrancePosition, () =>
        {
            Chair availableChair = Cafe.FindAvailableChair();

            if (availableChair == null) return;

            availableChair.SeatedCustomer = this;

            MoveTo(availableChair.Position, () =>
            {
                Seat(availableChair);
                MakeOrder();
            });
        });
    }

    private void Seat(Chair chair)
    {
        transform.position = chair.SittingPosition;
        transform.forward = chair.ConnectedTable.transform.position - transform.position;
        _seatedTable = chair.ConnectedTable;
    }

    private void MakeOrder()
    {
        _order = OrderSystem.Instance.CreateRandomOrder();
        _textFacingCamera3D.SetText("Ordering: " + _order.OrderedFood.Type);
    }
}

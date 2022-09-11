using TaskSystem;
using UnityEngine;

public class Food : StorableItem, IFood
{
    [SerializeField] private FoodType _type;
    public FoodType Type { get => _type; }
}

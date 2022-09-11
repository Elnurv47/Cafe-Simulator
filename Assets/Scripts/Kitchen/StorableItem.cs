using UnityEngine;

public enum FoodType
{
    Bread,
    Apple,
}

public class StorableItem : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get => _sprite; }

    public GameObject GetObject()
    {
        return gameObject;
    }
}

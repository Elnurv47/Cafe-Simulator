using TaskSystem;
using UnityEngine;

public class StorableItem : MonoBehaviour, IConsumable
{
    [SerializeField] private Sprite _sprite;

    public GameObject GetObject()
    {
        return gameObject;
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }
}

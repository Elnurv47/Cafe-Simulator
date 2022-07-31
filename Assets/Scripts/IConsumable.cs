using UnityEngine;

namespace TaskSystem
{
    public interface IConsumable
    {
        GameObject GetObject();
        Sprite GetSprite();
    }
}

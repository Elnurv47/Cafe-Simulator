using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ObjectPlacementSystem _objectPlacementSystem;

    public void OnClick()
    {
        _objectPlacementSystem.TryPlaceObject(_prefab);
    }
}

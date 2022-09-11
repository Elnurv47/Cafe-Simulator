using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    public static OrderSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Food[] _foodList;

    public Order CreateRandomOrder()
    {
        Food randomOrderedFood = GetRandomFood();
        return new Order(randomOrderedFood);
    }

    private Food GetRandomFood()
    {
        int randomIndex = Random.Range(0, _foodList.Length);
        return _foodList[randomIndex];
    }
}

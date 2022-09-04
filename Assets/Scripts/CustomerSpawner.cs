using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Button _spawnCustomerButton;
    [SerializeField] private Customer _customerPrefab;
    [SerializeField] private Vector3 _customerSpawnPosition;

    private void Start()
    {
        _spawnCustomerButton.onClick.AddListener(SpawnCustomer);
    }

    private void SpawnCustomer()
    {
        Customer spawnedCustomer = Instantiate(_customerPrefab, _customerSpawnPosition, Quaternion.identity);
        spawnedCustomer.GoToCafe();
    }
}

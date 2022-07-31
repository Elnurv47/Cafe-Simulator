using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private int _cameraSpeed;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        transform.Translate(movement * Time.deltaTime * _cameraSpeed, Space.World);
    }
}

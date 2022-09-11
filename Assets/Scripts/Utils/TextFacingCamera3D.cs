using UnityEngine;
using UnityEngine.UI;

public class TextFacingCamera3D : MonoBehaviour
{
    private Camera _mainCamera;
    private TextMesh _text;

    [SerializeField] private int _rotationSpeed;

    private void Start()
    {
        _mainCamera = Camera.main;
        _text = GetComponent<TextMesh>();
    }

    private void Update()
    {
        Vector3 direction = _mainCamera.transform.position - transform.position;
        RotateTo(direction);
    }

    private void RotateTo(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}

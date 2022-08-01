using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorOnHover : MonoBehaviour
{
    private Color _defaultColor;
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Renderer _renderer;

    private void Start()
    {
        _defaultColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        _renderer.material.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _defaultColor;
    }
}

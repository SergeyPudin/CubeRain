using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;

    private Renderer _renderer;
    private Color _newColor;
    private bool _isColorChanged;

    private void OnEnable()
    {
        _isColorChanged = false;

        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChanged == false && collision.transform.TryGetComponent<Floor>(out Floor floor))
        {
            _renderer.material.color = _newColor;

            _isColorChanged = true;
        }
    }

    private void OnDisable()
    {
        _isColorChanged = false;
    }

    public void SetNewColor(Color color)
    {
        if (color != null)
            _newColor = color;
    }
}
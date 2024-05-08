using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _newColor;
    private bool _isColorChanged;

    private void Start()
    {
        _isColorChanged = false;

        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChanged == false && collision.transform.TryGetComponent<Floor>(out Floor floor))
        {
            _renderer.material.color = _newColor;

            _isColorChanged = true;
        }
    }

    public void SetNewColor(Color color)
    {
        _newColor = color;
    }
}
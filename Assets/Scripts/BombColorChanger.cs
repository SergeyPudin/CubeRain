using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BombColorChanger : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();          
    }

    private void OnEnable()
    {
        ResetAlpha();
    }

    private void OnDisable()
    {
        ResetAlpha();
    }

    public void ChangeAlpha(float lifeTime)
    {
        float alphaValue = 0.0f;

        _renderer.material.DOFade(alphaValue, lifeTime);
    }

    public void ResetAlpha()
    {
        float alphaValue = 1.0f;
        Color color = _renderer.material.color;

        color.a = alphaValue;
        _renderer.material.color = color;
    }
}

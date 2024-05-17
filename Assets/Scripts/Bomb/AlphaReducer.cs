using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AlphaReducer : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();          
    }

    public void ReduceAlpha(float lifeTime)
    {
        float minAlphaValue = 0.0f;

        _renderer.material.DOFade(minAlphaValue, lifeTime);
    }

    public void ResetAlpha()
    {
        float maxAlphaValue = 1.0f;
        Color color = _renderer.material.color;

        color.a = maxAlphaValue;
        _renderer.material.color = color;
    }
}

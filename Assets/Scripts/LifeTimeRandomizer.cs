using UnityEngine;

public class LifeTimeRandomizer : MonoBehaviour
{
    [SerializeField] private float _minLiftime = 2.0f;
    [SerializeField] private float _maxLiftime = 5.0f;

    public float GetLifetime()
    {
        return Random.Range(_minLiftime, _maxLiftime);
    }
}
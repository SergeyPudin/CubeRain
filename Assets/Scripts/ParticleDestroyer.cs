using System.Collections;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float _effectLifetime = 1;

    private ParticleSystem _particleSystem;
    private Coroutine _selfDestructCoroutine;

    private void Start()
    {
        _selfDestructCoroutine = StartCoroutine(SelfDestruct());
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Play();
    }

    private IEnumerator SelfDestruct()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_effectLifetime);

        yield return waitForSeconds;

        Destroy(gameObject);

        StopCoroutine(_selfDestructCoroutine);
    }
}
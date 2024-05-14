using System.Collections;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float _effectLifetime = 1;

    private Coroutine _selfDestruct;
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _selfDestruct = StartCoroutine(SelfDestruct());
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Play();
    }

    private IEnumerator SelfDestruct()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_effectLifetime);

        yield return waitForSeconds;

        _selfDestruct = null;
        Destroy(gameObject);
    }
}
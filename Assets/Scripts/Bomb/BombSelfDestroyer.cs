using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AlphaReducer))]
public class BombSelfDestroyer : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _fXPrefab;

    private AlphaReducer _alphaReducer;
    private Coroutine _dieCoroutine;

    private void Awake()
    {
        _alphaReducer = GetComponent<AlphaReducer>();
    }

    public void GetLifeTime(float lifeTime)
    {
        _alphaReducer.ReduceAlpha(lifeTime);
        _dieCoroutine = StartCoroutine(Die(lifeTime));
    }

    private IEnumerator Die(float lifeTime)
    {
        WaitForSeconds waitForSeconds = new(lifeTime);

        yield return waitForSeconds;

        gameObject.SetActive(false);
        _alphaReducer.ResetAlpha();

        Explode();

        StopCoroutine(_dieCoroutine);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody)
            {
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                Instantiate(_fXPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AlphaChanger))]
public class BombSelfDestroyer : MonoBehaviour 
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _fXPrefab;

    private Coroutine _dieCoroutine;
    private AlphaChanger _alphaChanger;

    private void Awake()
    {
        _alphaChanger = GetComponent<AlphaChanger>();
    }

    public void GetLifeTime(float lifeTime)
    {
        _alphaChanger.ChangeAlpha(lifeTime);
        _dieCoroutine = StartCoroutine(Die(lifeTime));
    }

    private IEnumerator Die(float lifeTime)
    {
        WaitForSeconds waitForSeconds = new(lifeTime);

        yield return waitForSeconds;

        gameObject.SetActive(false);
        _alphaChanger.ResetAlpha();

        Explode();

        _dieCoroutine = null; 
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
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BombColorChanger))]
public class Bomb : MonoBehaviour 
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _explosionPrefab;

    private float _lifeTime;
    private Coroutine _dieCoroutine;
    private Coroutine _changeAlphaCoroutine;
    private BombColorChanger _changer;

    private void Awake()
    {
        _changer = GetComponent<BombColorChanger>();
    }

    public void GetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;

        _changeAlphaCoroutine = StartCoroutine(ChangeAlpha());
        _dieCoroutine = StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        WaitForSeconds waitForSeconds = new(_lifeTime);

        yield return waitForSeconds;

        gameObject.SetActive(false);
        Explode();
        _changer.ResetAlpha();

        _dieCoroutine = null; 
        _changeAlphaCoroutine= null;
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody)
            {
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator ChangeAlpha()
    {
        float delay = 0.5f;
        WaitForSeconds waitForSeconds = new (delay);

        yield return waitForSeconds;

        _changer.ChangeAlpha(_lifeTime);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SelfDestroyer : MonoBehaviour
{
    private float _lifeTime;
    private bool _isCountdowned;
    private Coroutine _dieCoroutine;

    public event UnityAction<Vector3, float> Destroyed;

    private void Start()
    {
        _isCountdowned = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCountdowned == false && collision.transform.TryGetComponent<Floor>(out Floor floor))
        {
            _dieCoroutine = StartCoroutine(Die());
        }
    }

    private void OnDisable()
    {
        if (_dieCoroutine != null)
            StopCoroutine(_dieCoroutine);
    }

    public void SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
    }

    private IEnumerator Die()
    {
        WaitForSeconds waitForSeconds = new(_lifeTime);

        if (waitForSeconds != null)
        {
            yield return waitForSeconds;

            gameObject.SetActive(false);

            Destroyed?.Invoke(transform.position, _lifeTime);
        }
    }
}
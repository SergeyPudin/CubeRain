using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BombPool), typeof(BombCounter))]
public class BombSpawner : MonoBehaviour
{
    private BombPool _pool;
    private Queue<SelfDestroyer> _destroyerQueue = new Queue<SelfDestroyer>();
    private BombCounter _counter;   

    public event UnityAction BombCreated;

    private void Start()
    {
        _pool = GetComponent<BombPool>();
        _counter = GetComponent<BombCounter>();
    }

    public void GetEvent(SelfDestroyer destroyer)
    {
        _destroyerQueue.Enqueue(destroyer);
        destroyer.Destroyed += CreateBomb;
    }

    private void CreateBomb(Vector3 position, float lifeTime)
    {
        if (_pool.RetrieveObject() != null)
        {
            Bomb bomb = _pool.RetrieveObject();
            bomb.transform.position = position;
            bomb.gameObject.SetActive(true);
            bomb.GetLifeTime(lifeTime);

            _counter.ChangedCount();

            BombCreated?.Invoke();

            if (_destroyerQueue.Count > 0)
            {
                SelfDestroyer destroyer = _destroyerQueue.Dequeue();
                destroyer.Destroyed -= CreateBomb;
            }
        }
    }
}
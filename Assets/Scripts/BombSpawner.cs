using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BombPool), typeof(BombCounter))]
public class BombSpawner : MonoBehaviour
{
    private BombPool _pool;
    private Queue<CubeSelfDestroyer> _destroyerQueue; 
    private BombCounter _counter;   

    public event UnityAction BombCreated;

    private void Start()
    {
        _pool = GetComponent<BombPool>();
        _counter = GetComponent<BombCounter>();

        _destroyerQueue = new Queue<CubeSelfDestroyer>();
    }

    public void GetEvent(CubeSelfDestroyer destroyer)
    {
        _destroyerQueue.Enqueue(destroyer);
        destroyer.CubeDestroyed += CreateBomb;
    }

    private void CreateBomb(Vector3 position, float lifeTime)
    {
        if (_pool.RetrieveObject() != null)
        {
            Bomb bomb = _pool.RetrieveObject();
            bomb.transform.position = position;
            bomb.gameObject.SetActive(true);
            bomb.gameObject.GetComponent<BombSelfDestroyer>().GetLifeTime(lifeTime);

            _counter.ChangedCount();

            BombCreated?.Invoke();

            if (_destroyerQueue.Count > 0)
            {
                CubeSelfDestroyer destroyer = _destroyerQueue.Dequeue();
                destroyer.CubeDestroyed -= CreateBomb;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : ObjectPool<Bomb>
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private BombTotalCounter _counter;

    private Queue<CubeSelfDestroyer> _destroyerQueue;

    private void Start()
    {
        _destroyerQueue = new Queue<CubeSelfDestroyer>();

        InitializePool(_bombPrefab);
    }

    public void GetEvent(CubeSelfDestroyer destroyer)
    {
        _destroyerQueue.Enqueue(destroyer);
        destroyer.CubeDestroyed += CreateBomb;
    }

    private void CreateBomb(Vector3 position, float lifeTime)
    {
        if (RetrieveObject() != null)
        {
            Bomb bomb = RetrieveObject();
            bomb.transform.position = position;
            bomb.gameObject.SetActive(true);
            bomb.gameObject.GetComponent<BombSelfDestroyer>().GetLifeTime(lifeTime);

            _counter.IncriminateCount();

            if (_destroyerQueue.Count > 0)
            {
                CubeSelfDestroyer destroyer = _destroyerQueue.Dequeue();
                destroyer.CubeDestroyed -= CreateBomb;
            }
        }
    }
}
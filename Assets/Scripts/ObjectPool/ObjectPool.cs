using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _quantity;
    [SerializeField] private Transform _parent;

    private List<T> _pool;

    private void Start()
    {
        _pool = new List<T>();

        Initialize();
    }

    public T RetrieveObject()
    {
        T item = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return item;
    }

    private void Initialize()
    {
        for (int i = 0; i < _quantity; i++)
        {
            T newObject = Instantiate(_prefab, transform.position, Quaternion.identity, _parent);
            newObject.gameObject.SetActive(false);
            _pool.Add(newObject);
        }
    }
}
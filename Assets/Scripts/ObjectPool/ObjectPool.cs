using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _objectsQuantity;
    [SerializeField] private Transform _parent;

    private int _quantityActiveObjects;
    private List<T> _pool;

    public event UnityAction<int> QuantityActivObjectsChanged;

    private void Start()
    {
        _pool = new List<T>();

        Initialize();
        ResetActiveObjectsQuantity();
    }

    private void Update()
    {
        if (_quantityActiveObjects != CountActiveObjects())
        {
            _quantityActiveObjects = CountActiveObjects();
            QuantityActivObjectsChanged?.Invoke(_quantityActiveObjects);
        }
    }

    public T RetrieveObject()
    {
        T item = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return item;
    }

    private void Initialize()
    {
        for (int i = 0; i < _objectsQuantity; i++)
        {
            T newObject = Instantiate(_prefab, transform.position, Quaternion.identity, _parent);
            newObject.gameObject.SetActive(false);
            _pool.Add(newObject);
        }
    }

    private int CountActiveObjects()
    {
        int quantity = 0;

        foreach (T item in _pool)
        {
            if (item.gameObject.activeSelf == true)
                quantity++;
        }

        return quantity;
    }

    private void ResetActiveObjectsQuantity()
    {
        _quantityActiveObjects = 0;
        QuantityActivObjectsChanged?.Invoke(_quantityActiveObjects);
    }
}
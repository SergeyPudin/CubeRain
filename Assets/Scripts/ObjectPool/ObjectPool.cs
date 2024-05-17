using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private int _poolSize = 25;
    [SerializeField] private Transform _parent;

    private List<T> _pool;
    private int _quantityActiveObjects;

    public event UnityAction<int> QuantityActivObjectsChanged;

    private void Awake()
    {
        _pool = new List<T>();
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
        T monoBehaviorItem = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return monoBehaviorItem;
    }

    protected void InitializePool(T objectPrefab)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            T newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity, _parent);
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
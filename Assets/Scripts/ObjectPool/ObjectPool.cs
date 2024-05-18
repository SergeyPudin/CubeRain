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

    public bool RetrieveObjectMonoBehavior(out T objectMonoBehavior) 
    {
        objectMonoBehavior = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        if (objectMonoBehavior != null)
            return true;

        return false;
    }

    protected void InitializePool(T objectPrefabMonoBehavior)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            T newObjectMonoBehavior = Instantiate(objectPrefabMonoBehavior, transform.position, Quaternion.identity, _parent);
            newObjectMonoBehavior.gameObject.SetActive(false);
            _pool.Add(newObjectMonoBehavior);
        }
    }

    private int CountActiveObjects()
    {
        int quantity = 0;

        foreach (T itemMonoBehavior in _pool)
        {
            if (itemMonoBehavior.gameObject.activeSelf == true)
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
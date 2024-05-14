using UnityEngine;
using UnityEngine.Events;

public abstract class Counter: MonoBehaviour
{
    protected int _count;

    public event UnityAction<int> ValueChanged;

    private void Start()
    {
        Reset();
    }

    public void ChangedCount()
    {
        _count++;
        ValueChanged?.Invoke(_count);
    }

    private void Reset()
    {
        _count = 0;
        ValueChanged?.Invoke(_count);
    }
}
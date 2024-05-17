using UnityEngine;
using UnityEngine.Events;

public abstract class TotalCounter: MonoBehaviour
{
    protected int _count;

    public event UnityAction<int> ValueChanged;

    private void Start()
    {
        Reset();
    }

    public void IncriminateCount()
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
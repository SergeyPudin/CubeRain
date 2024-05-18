using UnityEngine;
using UnityEngine.Events;

public abstract class TotalCounter: MonoBehaviour
{
    protected int Count;

    public event UnityAction<int> ValueChanged;

    private void Start()
    {
        Reset();
    }

    public void IncriminateCount()
    {
        Count++;
        ValueChanged?.Invoke(Count);
    }

    private void Reset()
    {
        Count = 0;
        ValueChanged?.Invoke(Count);
    }
}
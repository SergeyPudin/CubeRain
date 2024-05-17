using TMPro;
using UnityEngine;

public class CountViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TotalCounter _counter;

    private void OnEnable()
    {
        _counter.ValueChanged += ShowValue;
    }

    private void OnDisable()
    {
        _counter.ValueChanged -= ShowValue;
    }

    private void ShowValue(int value)
    {
        _text.text = value.ToString();
    }
}
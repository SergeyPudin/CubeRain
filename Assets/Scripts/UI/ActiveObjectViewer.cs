using TMPro;
using UnityEngine;

public class ActiveObjectViewer : MonoBehaviour
{
    [SerializeField] private ObjectPool<Cube> _cubePool;
    [SerializeField] private ObjectPool<Bomb> _bombPool;

    [SerializeField] private TMP_Text _cubeText;
    [SerializeField] private TMP_Text _bombText;
    
    private void OnEnable()
    {
        _cubePool.QuantityActivObjectsChanged += ShowValue<Cube>;
        _bombPool.QuantityActivObjectsChanged += ShowValue<Bomb>; 
    }

    private void OnDisable()
    {
        _cubePool.QuantityActivObjectsChanged -= ShowValue<Cube>;
        _bombPool.QuantityActivObjectsChanged -= ShowValue<Bomb>;
    }

    private void ShowValue<T>(int quantityMonoBehavior) where T : MonoBehaviour
    {
        if (typeof(T) == typeof(Cube))
            _cubeText.text = quantityMonoBehavior.ToString();
        else if (typeof(T) == typeof(Bomb))
            _bombText.text = quantityMonoBehavior.ToString();
    }
}

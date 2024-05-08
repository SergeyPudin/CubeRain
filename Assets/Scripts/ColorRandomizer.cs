using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    public Color GetRandomColor()
    {
       return  new Color(Random.value, Random.value, Random.value);
    }
}
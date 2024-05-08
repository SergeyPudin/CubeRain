using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifeTimeRandomizer), typeof(LifeTimeRandomizer), typeof(ColorRandomizer))]

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Floor _floor;
    [SerializeField] private float _offsetY;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _timeBetweenCreation;

    private Coroutine _createNewCubeCoroutine;
    private ColorRandomizer _colorRandomizer;
    private LifeTimeRandomizer _lifeTimeRandomizer;

    private void Start()
    {
        _colorRandomizer = GetComponent<ColorRandomizer>();
        _lifeTimeRandomizer = GetComponent<LifeTimeRandomizer>();

        _createNewCubeCoroutine = StartCoroutine(CreateNewCube());
    }

    private void OnDisable()
    {
        StopCoroutine(_createNewCubeCoroutine);
    }

    private Vector3 GetSpawnPosition()
    {
        float positionX;
        float positionY;
        float positionZ;

        float halving = 0.5f;

        float minValueX = _floor.transform.position.x - _floor.transform.localScale.x * halving;
        float maxValueX = _floor.transform.position.x + _floor.transform.localScale.x * halving;

        float minValueZ = _floor.transform.position.z - _floor.transform.localScale.z * halving;
        float maxValueZ = _floor.transform.position.z + _floor.transform.localScale.z * halving;

        positionX = Random.Range(minValueX, maxValueX);
        positionY = _floor.transform.position.y + _offsetY;
        positionZ = Random.Range(minValueZ, maxValueZ);

        return new Vector3(positionX, positionY, positionZ);
    }

    private IEnumerator CreateNewCube()
    {
        while (true)
        {
            Color color = _colorRandomizer.GetRandomColor();
            float lifeTime = _lifeTimeRandomizer.GetLifetime();

            yield return new WaitForSeconds(_timeBetweenCreation);

            Cube newCube = Instantiate(_cubePrefab, GetSpawnPosition(), CubeRotation());
            newCube.GetComponent<ColorChanger>().SetNewColor(color);
            newCube.GetComponent<SelfDestroyer>().SetLifeTime(lifeTime);
        }
    }

    private Quaternion CubeRotation()
    {
        float minValue = 0;
        float maxValue = 45;

        float randomX = Random.Range(minValue, maxValue);
        float randomY = Random.Range(minValue, maxValue);
        float randomZ = Random.Range(minValue, maxValue);

        return Quaternion.Euler(randomX, randomY, randomZ);
    }
}
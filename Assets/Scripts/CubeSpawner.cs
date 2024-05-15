using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifeTimeRandomizer), typeof(LifeTimeRandomizer), typeof(ColorRandomizer))]
[RequireComponent (typeof(ObjectPool<>), typeof(CubeCounter))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Floor _floor;
    [SerializeField] private float _offsetY;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _timeBetweenCreation;
    [SerializeField] private BombSpawner _bombSpawner;

    private Coroutine _createNewCubeCoroutine;
    private ColorRandomizer _colorRandomizer;
    private LifeTimeRandomizer _lifeTimeRandomizer;
    private CubePool _cubePool;
    private CubeCounter _cubeCounter;
        
    private void Start()
    {
        _colorRandomizer = GetComponent<ColorRandomizer>();
        _lifeTimeRandomizer = GetComponent<LifeTimeRandomizer>();
        _cubeCounter = GetComponent<CubeCounter>();
        _cubePool = GetComponent<CubePool>();

        _createNewCubeCoroutine = StartCoroutine(TurnCubeOn());
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

    private IEnumerator TurnCubeOn()
    {
        while (true)
        {
            Color color = _colorRandomizer.GetRandomColor();
            float lifeTime = _lifeTimeRandomizer.GetLifetime();

            yield return new WaitForSeconds(_timeBetweenCreation);

            if (_cubePool.RetrieveObject() != null)
            {
                Cube newCube = _cubePool.RetrieveObject();
                newCube.transform.position = GetSpawnPosition();
                newCube.transform.rotation = CubeRotation();
                newCube.gameObject.SetActive(true);

                newCube.GetComponent<ColorChanger>().SetNewColor(color);
                newCube.GetComponent<CubeSelfDestroyer>().SetLifeTime(lifeTime);

                _cubeCounter.ChangedCount();

                _bombSpawner.GetEvent(newCube.GetComponent<CubeSelfDestroyer>());
            }
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
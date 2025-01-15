using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Tooltip("Exploder object link")]
    [SerializeField] private Exploder _exploder;

    [Tooltip("Initial cube object")]
    [SerializeField] private Cube _initialCube;

    [Tooltip("Minimum cube split")]
    [SerializeField, Range(1, 6)] private int _countClonesMin = 2;
    [Tooltip("Maximum cube split")]
    [SerializeField, Range(1, 6)] private int _countClonesMax = 6;

    private int _clonesCount;

    private float _dispersion = 0.04f;

    public event Action<Cube> Spawned;

    private void OnValidate()
    {
        if (_countClonesMin >= _countClonesMax)
            _countClonesMax = _countClonesMax - 1;
    }

    private void Start()
    {
        _initialCube.Splitting += Spawn;
    }

    private void OnCubeSpawned(Cube cube)
    {
        cube.Splitting += Spawn;
    }

    private void OnCubeDestroyed(Cube cube)
    {
        cube.Splitting -= Spawn;
    }

    private void Spawn(Cube cube)
    {
        _clonesCount = Random.Range(_countClonesMin, _countClonesMax + 1);

        OnCubeDestroyed(cube);

        for (int i = 0; i < _clonesCount; i++)
        {
            Cube cubeClone = Instantiate(
                cube, RandomizePosition(cube.transform.position), Quaternion.identity
                );

            cubeClone.Init(cube.SplitChance);

            OnCubeSpawned(cubeClone);

            Spawned?.Invoke(cubeClone);
        }
    }

    private Vector3 RandomizePosition(Vector3 position)
    {
        return new Vector3(
            Disperce(position.x), Disperce(position.y), Disperce(position.z)
            );
    }

    private float Disperce(float number)
    {
        return number + Random.Range(-_dispersion, _dispersion);
    }
}
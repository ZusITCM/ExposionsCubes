using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly List<Cube> _activeCubes = new();

    [Header("Настройка спавнера")]

    [Tooltip("Взрыватель")]
    [SerializeField] private Exploder _exploder;

    [Tooltip("Минимальное количество клонов")]
    [SerializeField, Range(1, 6)] private int _countClonesMin;

    [Tooltip("Максимальное количество клонов")]
    [SerializeField, Range(1, 6)] private int _countClonesMax;

    [Tooltip("Количество начальных кубов")]
    [SerializeField, Range(1, 6)] private int _startCubeCount;

    [Tooltip("Префаб куба")]
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private Cube _cube;

    private int _countClones;

    private void Start()
    {
        _cube.Clicked += CubeSplit;
    }

    private void AddCube(Cube cube)
    {
        cube.Clicked += CubeSplit;
        cube.Destroyed += CubeDestroyed;

        _activeCubes.Add(cube);
    }

    private void CubeSplit(Cube cube)
    {
        cube.Clicked -= CubeSplit;
        cube.Destroyed -= CubeDestroyed;

        Vector3 changeScale = cube.transform.localScale / cube.ScaleDevider;
        float changeSplitChance = cube.SplitChance / cube.ScaleDevider;

        _activeCubes.Remove(cube);

        Spawn(cube.transform.position, changeScale, changeSplitChance);
    }

    private void CubeDestroyed(Cube cube)
    {
        cube.Clicked -= CubeSplit;
        cube.Destroyed -= CubeDestroyed;

        _activeCubes.Remove(cube);
    }

    private void Spawn(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        _countClones = Random.Range(_countClonesMin, _countClonesMax + 1);

        for (int i = 0; i < _countClones; i++)
        {
            Cube cubeClone = Instantiate(_cubePrefab, position, Random.rotation);

            cubeClone.transform.localScale = newScale;
            cubeClone.CubeColor.material.color = new Color(Random.value, Random.value, Random.value);

            cubeClone.Init(newSplitChance);

            if (cubeClone.CubeRigidbody != null)
                _exploder.Explode(cubeClone.CubeRigidbody, position);

            AddCube(cubeClone);
        }
    }
}

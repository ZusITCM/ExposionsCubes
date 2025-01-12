using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("Настройка спавнера")]

    [Tooltip("Минимальное количество клонов")]
    [SerializeField, Range(1, 6)] private int _countClonesMin;

    [Tooltip("Максимальное количество клонов")]
    [SerializeField, Range(1, 6)] private int _countClonesMax;

    [Tooltip("Префаб куба")]
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private Cube _cube;

    private readonly List<Cube> _activeCubes = new();

    private int _countClones;

    private void Start()
    {
        _cube.Splited += SplitCube;
    }

    private void AddCube(Cube cube)
    {
        cube.Splited += SplitCube;
        cube.Destroyed += OnCubeDestroyed;

        _activeCubes.Add(cube);
    }

    private void SplitCube(Cube cube)
    {
        OnCubeDestroyed(cube);

        Vector3 changeScale = cube.transform.localScale / cube.ScaleDevider;
        float changeSplitChance = cube.SplitChance / cube.ScaleDevider;

        Spawn(cube.transform.position, changeScale, changeSplitChance);
    }

    private void OnCubeDestroyed(Cube cube)
    {
        cube.Splited -= SplitCube;
        cube.Destroyed -= OnCubeDestroyed;

        _activeCubes.Remove(cube);
    }

    private void Spawn(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        _countClones = Random.Range(_countClonesMin, _countClonesMax + 1);

        for (int i = 0; i < _countClones; i++)
        {
            Cube cubeClone = Instantiate(_cubePrefab, position, Random.rotation);

            cubeClone.Init(newSplitChance, newScale);

            AddCube(cubeClone);
        }

        _cube.FillListCubes(_activeCubes);
    }
}

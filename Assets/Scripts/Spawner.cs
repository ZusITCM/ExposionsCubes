using System.Collections.Generic;
using UnityEngine;

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

    private List<ICubeObserver> _observers = new List<ICubeObserver>();

    private int _countClones;

    private void Start()
    {
        _cube.Splited += SplitCube;
    }

    public void RegisterObserver(ICubeObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(ICubeObserver observer)
    {
        _observers.Remove(observer);
    }

    private void AddCube(Cube cube)
    {
        cube.Splited += SplitCube;
    }

    private void SplitCube(Cube cube)
    {
        OnCubeDestroyed(cube);

        Vector3 changeScale = cube.transform.localScale / cube.ScaleDevider;
        float changeSplitChance = cube.SplitChance / cube.ScaleDevider;

        _countClones = Random.Range(_countClonesMin, _countClonesMax + 1);

        for (int i = 0; i < _countClones; i++)
            Spawn(cube.transform.position, changeScale, changeSplitChance);
    }

    private void OnCubeDestroyed(Cube cube)
    {
        cube.Splited -= SplitCube;
    }

    private void Spawn(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        Cube cubeClone;

        cubeClone = Instantiate(_cubePrefab, position, Random.rotation);

        cubeClone.Init(newSplitChance, newScale);

        foreach (var observer in _observers)
            observer.OnCubeSpawned(cubeClone);
    }
}

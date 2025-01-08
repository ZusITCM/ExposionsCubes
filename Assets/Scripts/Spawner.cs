using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly List<Cube> _activeCubes = new();

    [Header("��������� ��������")]

    [Tooltip("����������")]
    [SerializeField] private Exploder _exploder;

    [Tooltip("����������� ���������� ������")]
    [SerializeField, Range(1, 6)] private int _countClonesMin;

    [Tooltip("������������ ���������� ������")]
    [SerializeField, Range(1, 6)] private int _countClonesMax;

    [Tooltip("���������� ��������� �����")]
    [SerializeField, Range(1, 6)] private int _startCubeCount;

    [Tooltip("������ ����")]
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private Cube _cube;

    private int _countClones;

    private void Start()
    {
        _cube.Clicked += SplitCube;
    }

    private void AddCube(Cube cube)
    {
        cube.Clicked += SplitCube;
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
        cube.Clicked -= SplitCube;
        cube.Destroyed -= OnCubeDestroyed;

        _activeCubes.Remove(cube);
    }

    private void Spawn(Vector3 position, Vector3 newScale, float newSplitChance)
    {
        _countClones = Random.Range(_countClonesMin, _countClonesMax + 1);

        for (int i = 0; i < _countClones; i++)
        {
            Cube cubeClone = Instantiate(_cubePrefab, position, Random.rotation);

            cubeClone.transform.localScale = newScale;
            cubeClone.Color.material.color = new Color(Random.value, Random.value, Random.value);

            cubeClone.Init(newSplitChance);

            if (cubeClone.Rigidbody != null)
                _exploder.Explode(cubeClone.Rigidbody, position);

            AddCube(cubeClone);
        }
    }
}

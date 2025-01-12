using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public event UnityAction<Cube> Splited;
    public event UnityAction<Cube> Destroyed;

    [Tooltip("Делитель размера")]
    [field: SerializeField, Range(1, 10)] public int ScaleDevider { get; private set; } = 2;

    [Tooltip("Делитель шанса спавна")]
    [field: SerializeField, Range(1, 10)] public int ChanceDevider { get; private set; } = 2;

    [Tooltip("Шанс разделения")]
    [field: SerializeField, Range(0f, 1f)] public float SplitChance { get; private set; } = 1f;

    [SerializeField] private Exploder _exploder;

    public Rigidbody Rigidbody { get; private set; }

    public Renderer Color { get; private set; }

    private List<Cube> _cubes = new List<Cube>();

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Color = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (Random.value <= SplitChance)
        {
            SplitObject();
            AddExplosionForce(_cubes);
        }
        else
        {
            Destroy();
        }

        Destroy(gameObject);
    }

    public void FillListCubes(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
            _cubes.Add(cube);
    }

    private void ClearListCubes()
    {
        _cubes.Clear();
    }

    private void AddExplosionForce(List<Cube> cubeClones)
    {
        foreach (Cube cube in cubeClones)
            _exploder.Explode(cube);

        ClearListCubes();
    }

    public void Init(float splitChance, Vector3 scale)
    {
        SplitChance = splitChance;
        transform.localScale = scale;
        Color.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void SplitObject()
    {
        Splited?.Invoke(this);
    }

    private void Destroy()
    {
        Destroyed?.Invoke(this);
    }
}
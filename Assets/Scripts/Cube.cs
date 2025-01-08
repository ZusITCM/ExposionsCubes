using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public event UnityAction<Cube> Clicked;
    public event UnityAction<Cube> Destroyed;

    public Rigidbody CubeRigidbody { get; private set; }

    public Renderer CubeColor {  get; private set; }

    [Tooltip("Делитель размера")]
    [field: SerializeField, Range(1, 10)] public int ScaleDevider { get; private set; } = 2;

    [Tooltip("Делитель шанса спавна")]
    [field: SerializeField, Range(1, 10)] public int ChanceDevider { get; private set; } = 2;

    [Tooltip("Шанс разделения")]
    [field: SerializeField, Range(0f, 1f)] public float SplitChance { get; private set; } = 1f;

    private void Awake()
    {
        CubeRigidbody = GetComponent<Rigidbody>();
        CubeColor = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (Random.value <= SplitChance)
            SplitCube();
        else
            DestroyCube();
    }

    public void Init(float splitChance)
    {
        SplitChance = splitChance; 
    }

    private void SplitCube()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }

    private void DestroyCube()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
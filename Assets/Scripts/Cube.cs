using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public event UnityAction<Cube> Splited;

    public Rigidbody Rigidbody { get; private set; }

    public Renderer Renderer { get; private set; }

    [Tooltip("Делитель размера")]
    [field: SerializeField, Range(1, 10)] public int ScaleDevider { get; private set; } = 2;

    [Tooltip("Делитель шанса спавна")]
    [field: SerializeField, Range(1, 10)] public int ChanceDevider { get; private set; } = 2;

    [Tooltip("Шанс разделения")]
    [field: SerializeField, Range(0f, 1f)] public float SplitChance { get; private set; } = 1f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (Random.value <= SplitChance)
            SplitObject();

        Destroy(gameObject);
    }

    public void Init(float splitChance, Vector3 scale)
    {
        SplitChance = splitChance;
        transform.localScale = scale;
        Renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void SplitObject()
    {
        Splited?.Invoke(this);
    }
}
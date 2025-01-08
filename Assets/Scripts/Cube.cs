using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public event UnityAction<Cube> Clicked;
    public event UnityAction<Cube> Destroyed;

    public Rigidbody Rigidbody { get; private set; }

    public Renderer Color {  get; private set; }

    [Tooltip("�������� �������")]
    [field: SerializeField, Range(1, 10)] public int ScaleDevider { get; private set; } = 2;

    [Tooltip("�������� ����� ������")]
    [field: SerializeField, Range(1, 10)] public int ChanceDevider { get; private set; } = 2;

    [Tooltip("���� ����������")]
    [field: SerializeField, Range(0f, 1f)] public float SplitChance { get; private set; } = 1f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Color = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (Random.value <= SplitChance)
            Split();
        else
            Destroy();

        Destroy(gameObject);
    }

    public void Init(float splitChance)
    {
        SplitChance = splitChance; 
    }

    private void Split()
    {
        Clicked?.Invoke(this);
    }

    private void Destroy()
    {
        Destroyed?.Invoke(this);
    }
}
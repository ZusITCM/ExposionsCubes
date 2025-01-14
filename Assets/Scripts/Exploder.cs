using UnityEngine;

public class Exploder : MonoBehaviour, ICubeObserver
{
    [Header("��������� ������� �����")]

    [Tooltip("���� �������")]
    [SerializeField, Range(10f, 100f)] private float _explosionForce;

    [Tooltip("������ �������")]
    [SerializeField, Range(1f, 10f)] private float _explosionRadius;

    private void OnEnable()
    {
        Spawner spawner = FindObjectOfType<Spawner>();

        if (spawner != null)
            spawner.RegisterObserver(this);
    }

    private void OnDisable()
    {
        Spawner spawner = FindObjectOfType<Spawner>();

        if (spawner != null)
            spawner.UnregisterObserver(this);
    }

    public void OnCubeSpawned(Cube cube)
    {
        Explode(cube);
    }

    private void Explode(Cube cube)
    {
        if (cube.Rigidbody != null)
            cube.Rigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
    }
}

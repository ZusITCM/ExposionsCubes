using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Header("Настройки разлета кубов")]

    [Tooltip("Сила разлета")]
    [SerializeField, Range(10f, 100f)] private float _explosionForce;

    [Tooltip("Радиус разлета")]
    [SerializeField, Range(1f, 10f)] private float _explosionRadius;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
    }

    private void OnEnable()
    {
        _spawner.Spawned += Explode;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= Explode;
    }

    public void Explode(Cube cube)
    {
        cube.Rigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
    }
}

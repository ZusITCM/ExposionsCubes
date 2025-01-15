using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Tooltip("Explosion force")]
    [SerializeField, Range(1f, 2000f)] private float _explosionForce = 100f;
    [Tooltip("Explosion radius")]
    [SerializeField, Range(1f, 30f)] private float _explosionRadius = 15f;

    [Tooltip("Spawner object")]
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.Spawned += Explode;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= Explode;
    }

    private void Explode(Cube cube)
    {
        cube.Rigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
    }
}
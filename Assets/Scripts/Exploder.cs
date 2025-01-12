using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Header("Настройки разлета кубов")]

    [Tooltip("Сила разлета")]
    [SerializeField, Range(10f, 100f)] private float _explosionForce;

    [Tooltip("Радиус разлета")]
    [SerializeField, Range(1f, 10f)] private float _explosionRadius;

    public void Explode(Cube cube)
    {
        cube.Rigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Header("��������� ������� �����")]

    [Tooltip("���� �������")]
    [SerializeField, Range(10f, 100f)] private float _explosionForce;

    [Tooltip("������ �������")]
    [SerializeField, Range(1f, 10f)] private float _explosionRadius;

    public void Explode(Rigidbody cube, Vector3 position)
    {
        cube.AddExplosionForce(_explosionForce, position, _explosionRadius);
    }
}

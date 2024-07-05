using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform shoting;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //_rigidbody.velocity = Vector3.forward * speed * Time.deltaTime;
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            HealthController healthController = other.gameObject.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.takeDamage(1.0f);
            }
        }

        Destroy(gameObject);

    }
    public void SetTarget(Transform shooter)
    {
        shoting = shooter;

    }
}

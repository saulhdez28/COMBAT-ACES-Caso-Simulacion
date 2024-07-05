using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    float speed;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //_rigidbody.velocity = Vector3.forward * speed * Time.deltaTime;
        _rigidbody.position += transform.forward.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((targetMask & (1 << other.gameObject.layer)) != 0) 
        {
            if (other.gameObject.CompareTag("Enemy") && other.gameObject != null)
            {
                Destroy(other.gameObject);
            }
        }

        Destroy(gameObject);

    }

}

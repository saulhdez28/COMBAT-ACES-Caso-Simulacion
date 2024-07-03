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
        _rigidbody.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((targetMask & (1 << other.gameObject.layer)) != 0) 
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);

    }

}

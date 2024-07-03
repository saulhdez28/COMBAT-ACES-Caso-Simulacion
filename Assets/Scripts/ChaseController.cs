using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChaseController : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;

    Transform _target;
    Rigidbody _rigidbody;

    [SerializeField]
    float speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetTarget (Transform target)
    {
        _target = target;
        transform.LookAt(target.position);

    }

    private void FixedUpdate()
    {
        
        _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _target.position, speed * Time.fixedDeltaTime);
        transform.LookAt(_target.position);
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

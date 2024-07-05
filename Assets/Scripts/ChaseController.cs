using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;

    Transform _target;
    Rigidbody _rigidbody;

    [SerializeField]
    float speed;

    [SerializeField]
    float stopDistance;

    public bool moving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        Follow_Unfollow();
    }

    private void Follow_Unfollow()
    {
        float distance = Vector3.Distance(_rigidbody.position, _target.position);

        if (_target != null && distance > stopDistance)
        {
            moving = true;
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _target.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            moving = false;
        }

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

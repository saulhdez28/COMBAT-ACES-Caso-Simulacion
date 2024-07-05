using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;

    Transform _target;

    [SerializeField]
    float speed;

    [SerializeField]
    float stopDistance;

    [SerializeField]
    float tooCloseDistance;

    [SerializeField]
    float rotationSpeed;


    public bool moving;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Awake()
    {
        Rigidbody _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Follow_Unfollow();
    }

    private void Follow_Unfollow()
    {
        if (_target == null)
            return;

        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance > stopDistance)
        {
            moving = true;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.fixedDeltaTime);
        }
        else if (distance < tooCloseDistance)
        {
            moving = true;
            Vector3 direction = (transform.position - _target.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.fixedDeltaTime);
        }
        else
        {
            moving = false;
        }

        RotateTowardsTarget();
    }
    private void RotateTowardsTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if ((targetMask & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject);
        }

    }
}

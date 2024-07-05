using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float health;

    public void takeDamage()
    {
        health -= 1;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}

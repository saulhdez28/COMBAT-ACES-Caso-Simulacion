using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField]
    Transform[] firepoints;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletEnemyPrefab;

    [SerializeField]
    float fireDelay;

    [SerializeField]
    float lifeTimeout;

    float _currentTime;

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime < 0.0F)
        {
            _currentTime = 0.0F;
        }

        if (Input.GetButtonDown("Fire1") && gameObject.CompareTag("Player"))
        {

            if (_currentTime > 0.0F) 
            {
                return;
            }
            foreach (Transform firepoint in firepoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                Destroy(bullet.gameObject, lifeTimeout);
            }

            _currentTime = fireDelay;
            
        }
        if (gameObject.CompareTag("Boss") && gameObject.GetComponent<ChaseController>().moving == false)
        {
            if (_currentTime > 0.0F)
            {
                return;
            }
            foreach (Transform firepoint in firepoints)
            {
                GameObject Enemybullet = Instantiate(bulletEnemyPrefab, firepoint.position, firepoint.rotation);
                Enemybullet.GetComponent<BulletController>().SetTarget(gameObject.transform);
                Destroy(Enemybullet.gameObject, lifeTimeout);
            }

            _currentTime = fireDelay;
        }
    }
}

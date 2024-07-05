using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawningPrefabs;


    [SerializeField]
    GameObject[] spawnBossPrefabs;


    [SerializeField]
    int maxSpawningAmount;

    [SerializeField]
    float spawningRange;

    [SerializeField]
    float spawningSafeRange;

    [SerializeField]
    Transform target;

    private GameObject[] enemiesNonBossList;
    public bool BossInGame;


    private void Start()
    {
        Spawn();
        BossInGame = true;
    }


    private void Update()
    {
        enemiesNonBossList = GameObject.FindGameObjectsWithTag("Enemy");
        Boss();
    }
    private void Spawn()
    {       
        for (int spawningIndex = 0; spawningIndex < maxSpawningAmount;  spawningIndex++)
        {
            Vector3 spawningPoint = Vector3.zero;
            while (Vector3.Distance(spawningPoint, Vector3.zero) < spawningSafeRange)
            {
                spawningPoint = GetSpawningPoint();
            }

            GameObject spawningPrefab = spawningPrefabs[Random.Range(0, spawningPrefabs.Length)];
            GameObject spawningObject = Instantiate(spawningPrefab, spawningPoint, Quaternion.identity);
            ChaseController chaseController = spawningObject.GetComponent<ChaseController>();
            chaseController.SetTarget(target);
            spawningObject.transform.parent = transform;
        }
    }

    private void Boss()
    {
        if(enemiesNonBossList.Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length < 1 && BossInGame)
        {
            Vector3 spawnPoint = Vector3.zero;
            while (Vector3.Distance(spawnPoint, Vector3.zero) < spawningSafeRange)
            {
                spawnPoint = GetSpawningPoint();

            }

            GameObject spawnBossPrefab = spawnBossPrefabs[Random.Range(0, spawnBossPrefabs.Length)];
            GameObject spawnBossObject = Instantiate(spawnBossPrefab, spawnPoint, Quaternion.identity);
            ChaseController chaseController = spawnBossObject.GetComponent<ChaseController>();
            chaseController.SetTarget(target);
            spawnBossObject.transform.parent = transform;
            BossInGame = false;

        }
    }

    private Vector3 GetSpawningPoint()
    {

        float x = Random.Range(-1.0F, 1.0F);
        float y = Random.Range(-1.0F, 1.0F);
        float z = Random.Range(-1.0F, 1.0F);

        Vector3 spawninPoint = new Vector3(x, y, z);

        if(spawninPoint.magnitude > 1.0F) {
            spawninPoint.Normalize();}

        spawninPoint *= spawningRange;

        return spawninPoint;
    }
}

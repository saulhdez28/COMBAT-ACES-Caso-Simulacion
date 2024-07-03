using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawningPrefabs;

    [SerializeField]
    int maxSpawningAmount;

    [SerializeField]
    float spawningRange;

    [SerializeField]
    float spawningSafeRange;

    [SerializeField]
    Transform target;

    private void Start()
    {
        Spawn();
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

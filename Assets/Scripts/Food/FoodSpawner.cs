using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawners;
    [SerializeField] private float randXYPosRange = 10;
    [SerializeField] GameObject whalishFood;

    public float minTimeBetweenSpawns = 0.3f;
    public float maxTimeBetweenSpawns = 1.0f;

    public void Start()
    {
        spawnFood();
    }

    public void spawnFood()
     {
        StartCoroutine(SpawnAfterTime());
     }

     IEnumerator SpawnAfterTime()
     {
        yield return new WaitForSeconds(maxTimeBetweenSpawns);
        maxTimeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        int randomIndex = Random.Range(0, Spawners.Count);

        Vector3 spawnPosition = new Vector3(Spawners[randomIndex].transform.position.x + Random.Range(-randXYPosRange, randXYPosRange),
                                            Spawners[randomIndex].transform.position.y + Random.Range(-randXYPosRange, randXYPosRange),
                                            Spawners[randomIndex].transform.position.z);

        GameObject spawnedFood = Instantiate(whalishFood, transform, false);
        spawnedFood.transform.position = spawnPosition;
        StartCoroutine(SpawnAfterTime());
    }
}

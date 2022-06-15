using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawners;
    [SerializeField] private float randXYPosRange = 10;
    [SerializeField] GameObject[] whalishFood;

    public int foodsToSpawnBuff = 10;
    public int foodsToSpawnDebuff = 7;
    private int foodsSpawned = 1;
    int foodIndex = 0;

    public float minTimeBetweenSpawns = 0.3f;
    public float maxTimeBetweenSpawns = 1.0f;

    public void Start()
    {
        StartCoroutine(SpawnAfterTime());
    }

    public void Update()
    {
        if (foodsSpawned % foodsToSpawnBuff == 0)
        {
            foodIndex = 1;
        }
        else if (foodsSpawned % foodsToSpawnDebuff == 0)
        {
            foodIndex = 2;
        }
        else
            foodIndex = 0;
    }

    IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(maxTimeBetweenSpawns);
        maxTimeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        int randomIndex = Random.Range(0, Spawners.Count);

        Vector3 spawnPosition = new Vector3(Spawners[randomIndex].transform.position.x + Random.Range(-randXYPosRange, randXYPosRange),
                                            Spawners[randomIndex].transform.position.y + Random.Range(-randXYPosRange, randXYPosRange),
                                            Spawners[randomIndex].transform.position.z);


        GameObject spawnedFood = Instantiate(whalishFood[foodIndex], transform, false);
        spawnedFood.transform.position = spawnPosition;
        if (foodIndex == 1) Debug.Log("Buff Spawned!");
        if (foodIndex == 2) Debug.Log("Debuff Spawned!");
        Debug.Log(foodsSpawned);
        foodsSpawned += 1;

        StartCoroutine(SpawnAfterTime());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableSpawner : MonoBehaviour
{
    [SerializeField] public float spawnRate; // Set time between spawns
    [SerializeField] public int junkRate;
    [SerializeField] GameObject whalishFood; // Object which powers up the whale
    [SerializeField] GameObject oceanJunk; // Object which powers down the whale
    [SerializeField] GameObject whale;
    [SerializeField] private float randXYPosRange = 10;
    [SerializeField] bool isWhalePosBased = false;

    public float time = 0;
    private int foodCounter = 0;

    [SerializeField] List<GameObject> Spawners;

    public void Start()
    {
        time = 0;
        InvokeRepeating("SpawnEatable", 1, Random.Range(0, spawnRate));
    }

    public void FixedUpdate()
    {
        time += Time.deltaTime;

    }
    void SpawnEatable()
    {
        Vector3 spawnPosition;
        int randomIndex = Random.Range(0, Spawners.Count);

        if (isWhalePosBased == true)
        {
            spawnPosition = new Vector3(Spawners[randomIndex].transform.position.x + Random.Range(-10, 10) + whale.transform.position.x,
                                        Spawners[randomIndex].transform.position.y + Random.Range(-10, 10) + whale.transform.position.y,
                                        Spawners[randomIndex].transform.position.z);
        }
        else
        {
            spawnPosition = new Vector3(Spawners[randomIndex].transform.position.x + Random.Range(-randXYPosRange, randXYPosRange),
                                        Spawners[randomIndex].transform.position.y + Random.Range(-randXYPosRange, randXYPosRange),
                                        Spawners[randomIndex].transform.position.z);
        }

        GameObject spawnEatable;
        if (foodCounter % junkRate != 0)
        {
            foodCounter++;
            spawnEatable = Instantiate(whalishFood, transform, false);
            //Debug.Log(foodCounter + " Food spawned");
        }
        else
        {
            foodCounter++;
            spawnEatable = Instantiate(oceanJunk, transform, false);
            //Debug.Log(foodCounter + " " + junkRate + " junk spawned");
        }

        spawnEatable.transform.position = spawnPosition;

        //Debug.Log("Food spawned!");
    }

}

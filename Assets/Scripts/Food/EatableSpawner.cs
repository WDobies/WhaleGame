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
    [SerializeField] bool isWhalePosBased = false;

    private float time = 0;
    private bool isHealthy = false; // Determines if the object to eat is food or junk
    private int foodCounter = 0;

    [SerializeField] List<GameObject> Spawners;

    public void Start()
    {
        InvokeRepeating("SpawnEatable", 1, Random.Range(1, spawnRate));
    }

    public void Update()
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
            spawnPosition = new Vector3(Spawners[randomIndex].transform.position.x + Random.Range(-10, 10),
                                                Spawners[randomIndex].transform.position.y + Random.Range(-30, 30),
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

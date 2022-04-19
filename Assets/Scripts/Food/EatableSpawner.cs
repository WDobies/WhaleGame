using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableSpawner : MonoBehaviour
{
    [SerializeField] public float spawnRate; // Set time between spawns
    [SerializeField] GameObject whalishFood; // Object which powers up the whale
    [SerializeField] GameObject oceanJunk; // Object which powers down the whale
    [SerializeField] GameObject whale;

    private float time = 0;
    private bool isHealthy = false; // Determines if the object to eat is food or junk

    [SerializeField] List<GameObject> Spawners;

    public void Start()
    {
        InvokeRepeating("SpawnEatable", 1, Random.Range(1, spawnRate));
    }

    public void Update()
    {
        time += Time.deltaTime;
        if(time % 0.5 >= 0)
        {
            isHealthy = !isHealthy;
            //Debug.Log(isHealthy);
        }

    }
    void SpawnEatable()
    {
        int randomIndex = Random.Range(0, Spawners.Count);

        Vector3 spawnPosition = new Vector3(Spawners[randomIndex].transform.position.x + Random.Range(-10, 10) + whale.transform.position.x,
                                            Spawners[randomIndex].transform.position.y + Random.Range(-10, 10) + whale.transform.position.y,
                                            Spawners[randomIndex].transform.position.z);

        GameObject spawnEatable;
        if (isHealthy)
            spawnEatable = Instantiate(whalishFood, transform, false);
        else
            spawnEatable = Instantiate(oceanJunk, transform, false);

        spawnEatable.transform.position = spawnPosition;

        //Debug.Log("Food spawned!");
    }

}

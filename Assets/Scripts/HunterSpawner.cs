using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpawner : MonoBehaviour
{
    private List<GameObject> spawnedHuntersList;

    public GameObject hunter;

    // Start is called before the first frame update
    void Start()
    {
        spawnedHuntersList = new List<GameObject>();
        InvokeRepeating("SpawnHunter", 1, Random.Range(15, 25));
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedHuntersList.Count == 0)
        {
            float spawnRange = 24.0f;
            float value = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(value, 0, 0);
            GameObject spawnHunter = Instantiate(hunter, transform, false);
            spawnHunter.transform.localPosition = spawnPosition;
            spawnedHuntersList.Add(spawnHunter);
        }
    }

    void SpawnHunter()
    {
        float spawnRange = 24.0f;
        float value = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(value, 0, 0);
        GameObject spawnHunter = Instantiate(hunter, transform, false);
        spawnHunter.transform.localPosition = spawnPosition;
        spawnedHuntersList.Add(spawnHunter);
    }
}

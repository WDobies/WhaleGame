using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpawner : MonoBehaviour
{
    private List<GameObject> spawnedHuntersList;

    public GameObject hunter;

    //hunters settings
    [HideInInspector]
    private int maxHunterNumber = 3;
    [HideInInspector]
    private float hunterSpawnFrequency = 15.0f;
    [HideInInspector]
    private float firstHunterSpawn = 3.0f;

    //harpoon settings

    //other properties
    private int currentHunterNumber = 0;

    private void Awake()
    {
        maxHunterNumber = GameManager.instance.maxHunterNumber;
        hunterSpawnFrequency = GameManager.instance.hunterSpawnFrequency;
        firstHunterSpawn = GameManager.instance.firstHunterSpawn;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnedHuntersList = new List<GameObject>();
        InvokeRepeating("SpawnHunter", firstHunterSpawn, Random.Range(hunterSpawnFrequency, hunterSpawnFrequency+10));
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedHuntersList.Count == maxHunterNumber)
        {
            CancelInvoke("SpawnHunter");
        }
    }

    void SpawnHunter()
    {
        float spawnRange = 1.0f;
        float value = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(value, 0, 0);
        GameObject spawnHunter = Instantiate(hunter, transform, false);
        spawnHunter.transform.localPosition = spawnPosition;
        spawnedHuntersList.Add(spawnHunter);
    }
}

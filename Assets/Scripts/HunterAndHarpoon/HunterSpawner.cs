using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSpawner : MonoBehaviour
{
    private List<GameObject> spawnedHuntersList;

    public GameObject hunter;

    //difficulty level properties
    [HideInInspector]
    public float difficultyMultiplier = 0.0f;

    //hunters settings
    [HideInInspector]
    public int maxHunterNumber = 3;
    [HideInInspector]
    public float hunterSpawnFrequency = 15.0f;
    [HideInInspector]
    public float firstHunterSpawn = 3.0f;
    [HideInInspector]
    public float throwingFrequency = 0.0f;
    [HideInInspector]
    public float harpoonSpeed = 50.0f;

    //harpoon settings

    //other properties
    private int currentHunterNumber = 0;
  
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

        foreach ( GameObject spawnedHunter in spawnedHuntersList)
        {
            spawnedHunter.GetComponent<Hunter>().throwingFrequency = throwingFrequency/difficultyMultiplier;
        }
        //if(spawnedHuntersList.Count == 0)
        //{
        //    float spawnRange = 24.0f;
        //    float value = Random.Range(-spawnRange, spawnRange);
        //    Vector3 spawnPosition = new Vector3(value, 0, 0);
        //    GameObject spawnHunter = Instantiate(hunter, transform, false);
        //    spawnHunter.transform.localPosition = spawnPosition;
        //    spawnedHuntersList.Add(spawnHunter);
        //}
    }

    void SpawnHunter()
    {
        float spawnRange = 24.0f;
        float value = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(value, 0, 0);
        GameObject spawnHunter = Instantiate(hunter, transform, false);
        spawnHunter.transform.localPosition = spawnPosition;
        spawnHunter.GetComponent<Hunter>().harpoonSpeed = harpoonSpeed;
        spawnedHuntersList.Add(spawnHunter);
    }
}

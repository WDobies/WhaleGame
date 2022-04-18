using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> harpoons;
    public GameObject whale;

    private Transform whaleTransform;

    //difficulty level properties
    private int hunterNumber = 3;
    private float hunterSpawnFrequency = 15.0f;
    private float firstHunterSpawn = 3.0f;
    //other properties
    private int currentHunterNumber = 0;

    void Start()
    {
        InvokeRepeating("Spawn", firstHunterSpawn, hunterSpawnFrequency);
    }

    void Spawn()
    {
        if (harpoons.Count > 0)
        {
            whaleTransform = whale.transform;
            harpoons[0].SetActive(true);
            harpoons.RemoveAt(0);
        }
    }

    private void Update()
    {
        
    }
}

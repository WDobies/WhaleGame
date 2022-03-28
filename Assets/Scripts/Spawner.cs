using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> harpoons;
    public GameObject whale;

    private Transform whaleTransform;

    void Start()
    {
        InvokeRepeating("Spawn", 1, 3);
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
}

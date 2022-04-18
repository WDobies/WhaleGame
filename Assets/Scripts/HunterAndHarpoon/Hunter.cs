using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    //difficulty level properties
    [HideInInspector]
    public float throwingFrequency = 7.0f;

    [HideInInspector]
    public float harpoonSpeed = 50.0f;

    //other properties
    public GameObject harpoon;
    public float nextActionTime = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnHarpoon", 1, Random.Range(throwingFrequency, throwingFrequency*2));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + Random.Range(throwingFrequency, throwingFrequency * 2);
            SpawnHarpoon();
        }
    }

    void SpawnHarpoon()
    {
        harpoon.GetComponent<Harpoon>().movementSpeed = harpoonSpeed;
        Instantiate(harpoon, transform.position, Quaternion.identity);
    }
}

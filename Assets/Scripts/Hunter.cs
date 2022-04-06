using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public GameObject harpoon;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnHarpoon", 1, Random.Range(2,5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnHarpoon()
    {
        
        Instantiate(harpoon, transform.position, Quaternion.identity);
    }
}

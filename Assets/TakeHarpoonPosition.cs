using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHarpoonPosition : MonoBehaviour
{
    public GameObject harpoon;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = harpoon.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = harpoon.transform.position;
    }
}

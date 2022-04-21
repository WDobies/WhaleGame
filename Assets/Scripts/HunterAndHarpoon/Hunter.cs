using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    // Chasing variables
    GameObject whale;
    bool isChasing = false;
    Vector3 hunterPos;
    Vector3 whalePos;
    [SerializeField] float chasingSpeed;

    //difficulty level properties
    [HideInInspector]
    public float throwingFrequency = 7.0f;

    [HideInInspector]
    public float harpoonSpeed = 50.0f;

    [HideInInspector]
    public float harpoonRange = 30.0f;

    //other properties
    public GameObject harpoon;
    public float nextActionTime = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnHarpoon", 1, Random.Range(throwingFrequency, throwingFrequency*2));
        whale = GameObject.FindGameObjectWithTag("Player");
        hunterPos = transform.position;
        whalePos = whale.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        whalePos = whale.transform.position;
        //hunterPos = transform.position;
        //Debug.Log(Mathf.Abs(hunterPos.x - whalePos.x));
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + Random.Range(throwingFrequency, throwingFrequency * 2);
            SpawnHarpoon();
        }
        if (Mathf.Abs(hunterPos.x - whalePos.x) > 20)
        {
            Chase();
        }
    }

    void SpawnHarpoon()
    {
        harpoon.GetComponent<Harpoon>().movementSpeed = harpoonSpeed;
        harpoon.GetComponent<Harpoon>().whalePosition = whalePos;
        harpoon.GetComponent<Harpoon>().spawnRange = harpoonRange;
        Instantiate(harpoon, transform.position, Quaternion.identity);
    }

    void Chase()
    {
        Debug.Log("I'm chasing the whale! My pos along X axis is: " + transform.position.x);

        if (transform.position.x < whalePos.x)
            transform.position += new Vector3(1 * chasingSpeed * Time.deltaTime, 0, 0);
        else
            transform.position -= new Vector3(1 * chasingSpeed * Time.deltaTime, 0, 0);
    }
}

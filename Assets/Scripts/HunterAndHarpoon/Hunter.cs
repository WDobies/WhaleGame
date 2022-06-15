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
    private float throwingFrequency = 7.0f;

    //other properties
    public GameObject harpoon;
    public float nextActionTime = 1.0f;
    public bool justSpawned = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnHarpoon", 1, Random.Range(throwingFrequency, throwingFrequency*2));
        whale = GameObject.FindGameObjectWithTag("Player");
        hunterPos = transform.position;
        whalePos = whale.transform.position;
        Invoke("ChangeSpawnedStatus", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        throwingFrequency = GameManager.instance.throwingFrequency / GameManager.instance.difficultyMultiplierBase;

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
        Instantiate(harpoon, transform.position, Quaternion.identity, transform);
    }

    void Chase()
    {
        //Debug.Log("I'm chasing the whale! My pos along X axis is: " + transform.position.x);
        //Debug.Log(DetectHunter(transform.TransformDirection(Vector3.right)));
       
        if (transform.position.x < whalePos.x && !DetectHunter(Vector3.right, GameManager.instance.hunterDetectionDistance))
            transform.position += new Vector3(1 * chasingSpeed * Time.deltaTime, 0, 0);
        else if(transform.position.x > whalePos.x && !DetectHunter(Vector3.left, GameManager.instance.hunterDetectionDistance))
            transform.position -= new Vector3(1 * chasingSpeed * Time.deltaTime, 0, 0);
    }

    public bool DetectHunter( Vector3 direction, float detectionDistance )
    {
        // Bit shift the index of the layer (7) to get a bit mask
        int layerMask = 1 << 7;

        // This cast rays only against colliders in layer 7.

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance, layerMask))
        {
            //Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        //else if(Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
        //{
            //Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
        //    return true;
        //}
        return false;
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.layer == 7 && justSpawned)
        {
            float value = Random.Range(30.0f, 20.0f);
            Vector3 spawnPosition = new Vector3(value, 0, 0);
            transform.localPosition = spawnPosition;
        }
    }

    private void ChangeSpawnedStatus()
    {
        justSpawned = false;
    }
}

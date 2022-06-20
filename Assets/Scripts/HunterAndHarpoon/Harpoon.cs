using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harpoon : MonoBehaviour
{
    // difficulty level properties
    [HideInInspector]
    private float movementSpeed = 50.0f;

    [HideInInspector]
    private float spawnRange = 24.0f;

    // other properties
    public GameObject warning;

    [HideInInspector]
    public Vector3 whalePosition;

    private Movement whale;
    //private Vector3 whaleTransform;

    Vector3 targetPosition;
    bool stop = false;
    bool canUpdate = false;
    [SerializeField] ParticleSystem Bubbles = null;
    ParticleSystem instantiatedBubbles;
    bool bubbleSpawned = false;

    [SerializeField] ParticleSystem Blood = null;
    private bool isMovementFinished = false;
    private GameObject spawnedWarning;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = GameManager.instance.harpoonSpeed;
        spawnRange = GameManager.instance.harpoonRange / GameManager.instance.difficultyMultiplierBase;

        float value = Random.Range(whalePosition.x - (spawnRange), whalePosition.x + (spawnRange));
        targetPosition = new Vector3(value, -18, 0);

        transform.LookAt(targetPosition);
        Vector3 raySpawn = transform.position;
        //raySpawn.y = raySpawn.y - 5;
        spawnedWarning = Instantiate(warning, raySpawn, transform.rotation, transform);
        Invoke("StartUpdating", 2);

        //currentHealth = stats.health;

        //healthBar.GetComponent<StatSlider>().SetMaxValue(stats)
        //healthBar.SetMaxValue(stats.health);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementFinished == false)
            transform.LookAt(targetPosition);

        if (!stop && canUpdate)
        {
            transform.parent = null;
            if (spawnedWarning != null)
            {
                Destroy(spawnedWarning);
            }


            Vector3 pos = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            transform.position = pos;

            // Particles
            if (bubbleSpawned == false)
            {
                SpawnBubbles();
                if (instantiatedBubbles.isPlaying == false)
                    instantiatedBubbles.Play();

                bubbleSpawned = true;
            }
            else
                instantiatedBubbles.transform.position = transform.position;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ground")
        {
            Destroy(gameObject, 5);
            stop = true;

            // Particles
            if (instantiatedBubbles.isEmitting == true)
                instantiatedBubbles.Stop();
            isMovementFinished = true;
        }
        if (other.gameObject.name == "Whale" && !stop && canUpdate)
        {
            other.gameObject.GetComponent<PlayerStats>().harpoonsAttached.Add(gameObject);
            whale = other.gameObject.GetComponent<Movement>();

            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
            if (stats.health > 20)
            {
                stats.health -= other.gameObject.GetComponent<PlayerStats>().damagePerHarpoon;
            }
            else
            {
                stats.health -= other.gameObject.GetComponent<PlayerStats>().damagePerHarpoon;
                Debug.Log("Whale died.");
            }

            transform.parent = other.transform;

            transform.GetComponent<Rigidbody>().detectCollisions = false;
            stop = true;

            // Particles
            if (instantiatedBubbles.isEmitting == true)
                instantiatedBubbles.Stop();

            if (Blood.isEmitting == false)
                Blood.Play();

            isMovementFinished = true;
        }
    }

    void SpawnBubbles()
    {
        instantiatedBubbles = Instantiate(Bubbles, transform.position, Quaternion.identity);
    }

    void StartUpdating()
    {
        canUpdate = true;
    }
}

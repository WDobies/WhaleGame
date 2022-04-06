using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    private Movement whale;
    //private Vector3 whaleTransform;
    float spawnRange = 24.0f;
    Vector3 targetPosition;
    bool stop = false;
    
    [SerializeField] ParticleSystem Bubbles = null;
    ParticleSystem instantiatedBubbles;
    bool bubbleSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        float value = Random.Range(-spawnRange, spawnRange);
        targetPosition = new Vector3(value, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            transform.LookAt(targetPosition);

            Vector3 pos = Vector2.MoveTowards(transform.position, targetPosition, 55 * Time.deltaTime);
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
        }
        if (other.gameObject.name == "Whale" && !stop)
        {
            whale = other.gameObject.GetComponent<Movement>();
            whale.timesHit++;

            this.transform.parent = other.transform;
            //Physics.IgnoreCollision(other, this.transform.parent.GetComponent<BoxCollider>());
            this.transform.GetComponent<Rigidbody>().detectCollisions = false;
            stop = true;

            // Particles
            if (instantiatedBubbles.isEmitting == true)
                instantiatedBubbles.Stop();
        }
    }

    void SpawnBubbles()
    {
        instantiatedBubbles =  Instantiate(Bubbles, transform.position, Quaternion.identity);
    }
}

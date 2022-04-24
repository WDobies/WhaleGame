using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] bool isHealthy = true;
    [SerializeField] float timeToStay = 3;
    [SerializeField] float foodVelocity = 10;
    bool leftToRight;

    private float time;

    private void Start()
    {
        if (transform.position.x >= GameObject.FindGameObjectWithTag("Player").transform.position.x) leftToRight = false;
        else leftToRight = true;
        

    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeToStay)
        {
            Destroy(gameObject);
        }
        if (leftToRight == true)
        {
            transform.position += new Vector3(foodVelocity * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(foodVelocity * Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    void PickUp(Collider player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if(isHealthy)
        {
            player.transform.localScale *= 1.02f;
            stats.energy += player.GetComponent<PlayerStats>().energyGainedFromFood;
            Score.instance.AddPoint();
        }
        else
        {
            player.transform.localScale /= 1.02f;
            stats.energy -= player.GetComponent<PlayerStats>().energyGainedFromFood;
            Score.instance.SubtractPoint();
        }

        Destroy(gameObject);
    }
}

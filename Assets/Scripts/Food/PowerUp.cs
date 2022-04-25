using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] bool isHealthy = true;
    [SerializeField] float timeToStay = 3;
    [SerializeField] float foodVelocity = 10;
    [SerializeField] float maxYMovement = 5;
    [SerializeField] int upAndDownTime = 10;
    [SerializeField] bool isMovingUpAndDown = true;
    bool leftToRight;
    bool isMovingUp = true;

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

        int tempTime = (int)time / upAndDownTime;
        if (tempTime % 2 == 0) isMovingUp = false;
        else isMovingUp = true;

        if (isMovingUpAndDown == true)
        {
            if (isMovingUp == true && transform.position.y < maxYMovement)
            {
                transform.position += new Vector3(0, foodVelocity * Time.deltaTime, 0);
            }
            else if (isMovingUp == false && transform.position.y > -maxYMovement)
            {
                transform.position -= new Vector3(0, foodVelocity * Time.deltaTime, 0);
            }
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

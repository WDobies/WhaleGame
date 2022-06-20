using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eatable : MonoBehaviour
{
    [SerializeField] bool isBuff = false;
    [SerializeField] bool isFood = false;
    [SerializeField] float timeToStay = 3;
    [SerializeField] float debuffSpeedMultiplier = 0.6f;

    [SerializeField] float foodVelocity = 10;
    [SerializeField] float maxYMovement = 5;
    [SerializeField] int upAndDownTime = 10;
    [SerializeField] bool isMovingUpAndDown = true;
    bool leftToRight;
    bool isMovingUp = true;
    private Vector3 spawnerPosOffset;

    private float time;

    private void Start()
    {
        if (transform.position.x >= GameObject.FindGameObjectWithTag("Player").transform.position.x) leftToRight = false;
        else leftToRight = true;
        spawnerPosOffset = transform.position;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeToStay)
        {
            Destroy(gameObject);
        }

        int tempTime = (int)time;
        if (tempTime % upAndDownTime == 0) isMovingUp = false;
        else isMovingUp = true;

        if (isMovingUpAndDown == true)
        {
            if (isMovingUp == true && transform.position.y < spawnerPosOffset.y + maxYMovement)
            {
                transform.position += new Vector3(0, foodVelocity * Time.deltaTime, 0);
            }
            else if (isMovingUp == false && transform.position.y > spawnerPosOffset.y - maxYMovement)
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
        if (isBuff && !isFood)
        {
            if (player.GetComponent<PlayerStats>().health < player.GetComponent<PlayerStats>().startHealth)
            {
                player.GetComponent<PlayerStats>().health += player.GetComponent<PlayerStats>().startHealth / 3.0f;
                if (player.GetComponent<PlayerStats>().health >= player.GetComponent<PlayerStats>().startHealth / 2.0f)
                {
                    player.GetComponent<PlayerStats>().hp2.SetActive(true);
                }
                if (player.GetComponent<PlayerStats>().health >= player.GetComponent<PlayerStats>().startHealth)
                {
                    player.GetComponent<PlayerStats>().hp1.SetActive(true);
                }
            }

            //player.GetComponent<PlayerStats>().harpoonsAttached.RemoveAt(player.GetComponent<PlayerStats>().harpoonsAttached.Count - 1);
            //Destroy(player.GetComponent<PlayerStats>().harpoonsAttached[player.GetComponent<PlayerStats>().harpoonsAttached.Count - 1].gameObject);
        }
        else if (!isBuff && !isFood)
        {
            player.GetComponent<Movement>().currentDebuffTime = 0.0f;
            player.GetComponent<Movement>().isDebuffed = true;
            player.GetComponent<Movement>().changeMaxSpeed(debuffSpeedMultiplier);
            Debug.Log("maxSpeed: " + player.GetComponent<Movement>().maxSpeed);
        }
        else
        {
            stats.energy += player.GetComponent<PlayerStats>().energyGainedFromFood;
            Score.instance.AddPoint();
        }

        Destroy(gameObject);
    }
}

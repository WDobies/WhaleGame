using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] bool isHealthy = true;
    [SerializeField] float timeToStay = 3;

    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeToStay)
        {
            Debug.Log("Eatable destroyed!");
            Destroy(gameObject);
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

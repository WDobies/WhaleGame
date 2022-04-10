using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    void PickUp(Collider player)
    {
        player.transform.localScale *= 1.02f;
        PlayerStats stats = player.GetComponent<PlayerStats>();

        stats.energy += player.GetComponent<PlayerStats>().energyGainedFromFood;

        Destroy(gameObject);
    }
}

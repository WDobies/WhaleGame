using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSuddenDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
            stats.health = 0;
        }
    }
}

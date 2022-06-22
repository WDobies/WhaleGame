using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float startHealth;
    [SerializeField] public float health = 60;
    [SerializeField] public float energy = 100f;
    [SerializeField] public float damagePerHarpoon = 20f;
    [SerializeField] public float energyGainedFromFood = 30f;
    [SerializeField] public float energyLostPerTouch = 5f;

    [SerializeField] public List<GameObject> harpoonsAttached = new List<GameObject>();

    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;

    private void Start()
    {
        startHealth = health;
    }

    public void FixedUpdate()
    {
        //Debug.Log(health);
        if(health <= 0)
        {
            GameManager.instance.UpdateGameState(GameState.Lose);
        }
        if(health < startHealth && hp1.active)
        {
            hp1.SetActive(false);
        }
        else if (health < startHealth/2.0f && hp2.active)
        {
            hp2.SetActive(false);
        }
        else if (health < startHealth / 3.0f && hp3.active)
        {
            hp3.SetActive(false);
        }
    }
}

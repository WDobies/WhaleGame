using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float health = 100f;
    [SerializeField] public float energy = 100f;
    [SerializeField] public float damagePerHarpoon = 20f;
    [SerializeField] public float energyGainedFromFood = 30f;
    [SerializeField] public float energyLostPerTouch = 5f;
    [SerializeField] public Slider healthBar;
    [SerializeField] public Slider energyBar;

    public void Start()
    {
        healthBar.maxValue = health;
        energyBar.maxValue = energy;
    }

    public void Update()
    {
        healthBar.value = health;
        energyBar.value = energy;
    }
}

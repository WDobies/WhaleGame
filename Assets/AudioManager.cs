using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource foodSound;
    public AudioSource hitSound;
    public AudioSource splash;
    public AudioSource bonus;
    public AudioSource debuff;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void playHitSound()
    {
        hitSound.Play();
    }

    public void playFoodSound()
    {
        foodSound.Play();
    }

    public void Splash()
    {
        if(!splash.isPlaying)
            splash.Play();
    }
    public void Bonus()
    {
        bonus.Play();
    }
    public void Debuff()
    {
        debuff.Play();
    }
}

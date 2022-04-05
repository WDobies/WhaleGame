using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovement : MonoBehaviour
{
    public GameObject parent;
    [SerializeField] ParticleSystem Bubbles = null;

    private void OnTriggerEnter(Collider other)
    {
        if (Bubbles.isEmitting == true)
            Bubbles.Stop();
        parent.SetActive(false);
    }
}

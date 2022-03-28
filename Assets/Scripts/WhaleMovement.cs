using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovement : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {

        parent.SetActive(false);
    }
}

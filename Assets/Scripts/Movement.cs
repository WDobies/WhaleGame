using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Update()
    {
        Physics.gravity = new Vector3(0, -4.5f, 0);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.position.x > Screen.width/2 && touch.phase == TouchPhase.Began)
            {
                GetComponent<Rigidbody>().AddForce(-300, 200.5f, 0);
                Debug.Log("right");
            }
            if (touch.position.x < Screen.width /2 && touch.phase == TouchPhase.Began)
            {
                GetComponent<Rigidbody>().AddForce(300, 200.5f, 0);
                Debug.Log("left");
            }
            
        }
        transform.forward += -GetComponent<Rigidbody>().velocity.normalized * 0.05f;
    }
}

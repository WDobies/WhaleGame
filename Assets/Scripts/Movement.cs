using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool doubleScreen = false;
    private int width;
    private int height;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Physics.gravity = new Vector3(0, -4.5f, 0);
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (doubleScreen)
            {
                upMovement(touch);
            }
            else
            {
                upMovement(touch);

                if (touch.position.x > Screen.width / 2 && touch.phase == TouchPhase.Began && touch.position.y < Screen.height / 2)
                {
                    rb.AddForce(-150, -300.5f, 0);
                }
                if (touch.position.x < Screen.width / 2 && touch.phase == TouchPhase.Began && touch.position.y < Screen.height / 2)
                {
                    rb.AddForce(150, -300.5f, 0);
                }
            }    
        }

        transform.forward += -rb.velocity.normalized * 0.05f;
    }

    void upMovement(Touch touch)
    {
        if (touch.position.x > Screen.width / 2 && touch.phase == TouchPhase.Began)
        {
            rb.AddForce(-300, 200.5f, 0);
        }
        if (touch.position.x < Screen.width / 2 && touch.phase == TouchPhase.Began)
        {
            rb.AddForce(300, 200.5f, 0);
        }
    }
}

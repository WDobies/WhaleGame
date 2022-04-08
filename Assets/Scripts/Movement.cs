using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool doubleScreen = false;
    public int timesHit = 0;
    private int width;
    private int height;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Physics.gravity = new Vector3(0, -4.5f-(timesHit*4), 0);
        
        if (Input.touchCount > 0)
        {
            PlayerStats stats = GetComponent<PlayerStats>();

            Touch touch = Input.GetTouch(0);

            if (doubleScreen)
            {
                stats.energy -= GetComponent<PlayerStats>().energyLostPerTouch;
                upMovement(touch);
            }
            else
            {
                upMovement(touch);

                if (touch.position.x > Screen.width / 2 && touch.phase == TouchPhase.Began && touch.position.y < Screen.height / 2)
                {
                    stats.energy -= GetComponent<PlayerStats>().energyLostPerTouch;
                    rb.AddForce(-150, -300.5f, 0);
                }
                if (touch.position.x < Screen.width / 2 && touch.phase == TouchPhase.Began && touch.position.y < Screen.height / 2)
                {
                    stats.energy -= GetComponent<PlayerStats>().energyLostPerTouch;
                    rb.AddForce(150, -300.5f, 0);
                }
            }    
        }

        transform.forward += -rb.velocity.normalized * 0.05f;

    }

    void upMovement(Touch touch)
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (touch.position.x > Screen.width / 2 && touch.phase == TouchPhase.Began)
        {
            stats.energy -= GetComponent<PlayerStats>().energyLostPerTouch;
            rb.AddForce(-300, 200.5f, 0);
        }
        if (touch.position.x < Screen.width / 2 && touch.phase == TouchPhase.Began)
        {
            stats.energy -= GetComponent<PlayerStats>().energyLostPerTouch;
            rb.AddForce(300, 200.5f, 0);
        }
    }

}

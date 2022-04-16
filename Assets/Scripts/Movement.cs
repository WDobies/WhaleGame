using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool doubleScreen = false;
    public int timesHit = 0;
    public float gravity;

    private int width;
    private int height;
    [SerializeField] private float sideForce; //300
    [SerializeField] private float upDownForce; //400
    private Rigidbody rb;
    private bool isColiding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero; //helps avoid moving on Z axis
        width = Screen.width / 2;
        height = Screen.height / 2;
    }
    void Update()
    {
        //Physics.gravity = new Vector3(0, -4.5f-(timesHit*4), 0);
        Physics.gravity = new Vector3(0, gravity, 0);

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

                if (touch.position.x > width && touch.phase == TouchPhase.Began && touch.position.y < height)
                {
                    rb.AddForce(-sideForce, -upDownForce, 0);
                }
                if (touch.position.x < width && touch.phase == TouchPhase.Began && touch.position.y < height)
                {
                    rb.AddForce(sideForce, -upDownForce, 0);
                }
            }    
        }

        
        if(!isColiding)
            transform.forward += -rb.velocity.normalized * 0.05f;
        //transform.forward += new Vector3(0, Time.deltaTime * 10);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isColiding = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isColiding = false;
    }

    private void upMovement(Touch touch)
    {
        if (touch.position.x > width && touch.phase == TouchPhase.Began && touch.position.y > height)
        {
            rb.AddForce(-sideForce, upDownForce, 0);
        }
        if (touch.position.x < width && touch.phase == TouchPhase.Began && touch.position.y > height)
        {
            rb.AddForce(sideForce, upDownForce, 0);
        }
    }

}

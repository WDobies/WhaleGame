using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float gravity;
    public float airGravity;
    public float waterGravity;

    [SerializeField] private float sideForce; //300
    [SerializeField] private float upDownForce; //400
    [SerializeField] private float maxSpeed;
    [SerializeField] private float closeToMaxVelocityForceDivider = 10;
    [SerializeField] private float oceanEdge; //36
    [SerializeField] Camera mainCamera;
    private int width;
    private int height;
    private bool isColiding = false;
    private Rigidbody rb;

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

        float speed = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2));

        if (transform.position.y < oceanEdge)
        {
            mainCamera.GetComponent<CameraFollow>().isUnderwater = true;
            gravity = waterGravity;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (speed < maxSpeed)
                {
                    if (touch.position.x > width && touch.phase == TouchPhase.Began && touch.position.y > height)
                    {
                        rb.AddForce(-sideForce, upDownForce, 0);
                    }

                    if (touch.position.x < width && touch.phase == TouchPhase.Began && touch.position.y > height)
                    {
                        rb.AddForce(sideForce, upDownForce, 0);
                    }

                    if (touch.position.x > width && touch.phase == TouchPhase.Began && touch.position.y < height)
                    {
                        rb.AddForce(-sideForce, -upDownForce, 0);
                    }

                    if (touch.position.x < width && touch.phase == TouchPhase.Began && touch.position.y < height)
                    {
                        rb.AddForce(sideForce, -upDownForce, 0);
                    }
                }

                else if (speed >= maxSpeed)
                {
                    Debug.Log("You have reached top speed: " + maxSpeed + "Present speed: " + speed);
                    if (touch.position.x > width && touch.phase == TouchPhase.Began && touch.position.y > height)
                    {
                        rb.AddForce(-sideForce / closeToMaxVelocityForceDivider, upDownForce / closeToMaxVelocityForceDivider, 0);
                    }

                    if (touch.position.x < width && touch.phase == TouchPhase.Began && touch.position.y > height)
                    {
                        rb.AddForce(sideForce / closeToMaxVelocityForceDivider, upDownForce / closeToMaxVelocityForceDivider, 0);
                    }

                    if (touch.position.x > width && touch.phase == TouchPhase.Began && touch.position.y < height)
                    {
                        rb.AddForce(-sideForce / closeToMaxVelocityForceDivider, -upDownForce / closeToMaxVelocityForceDivider, 0);
                    }

                    if (touch.position.x < width && touch.phase == TouchPhase.Began && touch.position.y < height)
                    {
                        rb.AddForce(sideForce / closeToMaxVelocityForceDivider, -upDownForce / closeToMaxVelocityForceDivider, 0);
                    }
                }
            }
        }
        else
        {
            gravity = airGravity;
            mainCamera.GetComponent<CameraFollow>().isUnderwater = false;
        }


        if (!isColiding)
            transform.forward += -rb.velocity.normalized * 0.2f;
    }

    public void changeSpeed(float mult)
    {
        upDownForce *= mult;
        sideForce *= mult;
    }

    public void restoreDefaultSpeed()
    {
        upDownForce = 400;
        sideForce = 300;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColiding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isColiding = false;
    }
}

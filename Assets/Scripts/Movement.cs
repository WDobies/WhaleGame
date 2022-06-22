using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float gravity;
    public float airGravity;
    public float waterGravity;

    [SerializeField] private float sideForce; // -300
    [SerializeField] private float upDownForce; //400
    [SerializeField] public float maxSpeed;
    private float defaultMaxSpeed;
    [SerializeField] private float closeToMaxVelocityForceDivider = 10;
    [SerializeField] private float oceanEdge; //36
    [SerializeField] Camera mainCamera;
    private int width;
    private int height;
    private bool isColiding = false;
    private Rigidbody rb;

    [SerializeField] public float debuffTime = 2.0f;
    [SerializeField] public float currentDebuffTime = 0.0f;
    [SerializeField] public bool isDebuffed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero; //helps avoid moving on Z axis
        width = Screen.width / 2;
        height = Screen.height / 2;

        defaultMaxSpeed = maxSpeed;
    }
    void Update()
    {
        if (isDebuffed)
        {
            currentDebuffTime += Time.deltaTime;
            if (currentDebuffTime > debuffTime)
            {
                restoreDefaultMaxSpeed();
            }
        }
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
                    //Debug.Log("You have reached top speed: " + maxSpeed + "Present speed: " + speed);
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
            if(Time.timeScale == 1) { AudioManager.instance.Splash(); }           
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

    public void restoreDefaultMaxSpeed()
    {
        maxSpeed = defaultMaxSpeed;
        currentDebuffTime = 0.0f;
        isDebuffed = false;
        Debug.Log("maxSpeed: " + maxSpeed);
    }

    public void changeMaxSpeed(float maxSpeedMultiplier)
    {
        maxSpeed *= maxSpeedMultiplier;
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

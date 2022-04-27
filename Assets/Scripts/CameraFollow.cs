using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool isUnderwater = true;
    private bool switchToWaterCamera;
    public Transform target;
    public float speed;
    public Vector3 offset;
    private Vector3 tempOffset;

    private void Start()
    {
        tempOffset = target.position + offset; // used to make comeback to waterCamera smooth
    }

    private void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, speed);
        float smoothWaterPosY = Mathf.Lerp(transform.position.y, tempOffset.y, speed); // used to make comeback to waterCamera smooth
        if (isUnderwater == true)
        {
            transform.position = new Vector3(smoothPos.x, smoothWaterPosY - 0.4f, offset.z); 
        }
        else
        {
            transform.position = new Vector3(smoothPos.x, smoothPos.y, offset.z);
        }

        // var currentTrans = transform.forward;
        // transform.LookAt(target);
        //var val = Mathf.Clamp(transform.forward.y, -0.05f, 0.05f);
        //Debug.Log(transform.forward);
        // if(transform.forward.y < 0.1 && transform.forward.y > - 0.1)    
        //transform.forward = new Vector3(currentTrans.x, val, currentTrans.z);
        //transform.forward = new Vector3(0, val, 0);
        //transform.forward -= new Vector3(0, Time.deltaTime, 0);//Time.deltaTime;
        ///Debug.Log(target.rotation.x - transform.rotation.x);
    }
}

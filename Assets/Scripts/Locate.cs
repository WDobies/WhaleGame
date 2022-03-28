using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locate : MonoBehaviour
{
    public GameObject whale;
    private Vector3 whaleTransform;
    int pos;

    private void Awake()
    {
        whaleTransform = whale.transform.position + new Vector3(0, -10, 0);
        pos = Random.Range(-25, 25);
        transform.position = new Vector3(pos,25,0);
    }
    void Update()
    {
        transform.LookAt(whaleTransform);
        Vector3 pos = Vector2.MoveTowards(transform.position, whaleTransform , 55 * Time.deltaTime);
        transform.position = pos;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpriteController : MonoBehaviour
{
    public GameObject pointAxis;
    Vector3 forward;

    private void Start()
    {
       pointAxis = GameObject.FindGameObjectWithTag("PointAxis");
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
        forward = transform.TransformDirection(Vector3.forward);
        transform.forward = pointAxis.transform.position - transform.position;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBox : MonoBehaviour
{
    public float xSpeed = 0.0f;
    public float ySpeed = 0.0f;
    public float zSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    xSpeed = Random.Range(0, Random.Range(0, 8) * 50.5f);
    ySpeed = Random.Range(0, Random.Range(0, 26) * 50.5f);
    zSpeed = Random.Range(0, Random.Range(0, 2) * 50.5f);
    transform.Rotate(
            xSpeed * Time.deltaTime,
            ySpeed * Time.deltaTime,
            zSpeed * Time.deltaTime
       );
    }
}

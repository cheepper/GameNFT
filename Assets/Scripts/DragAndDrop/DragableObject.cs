using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    public bool detected;
    public LayerMask m_LayerMask;
    private Renderer cubeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = transform.GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 2.1f, Quaternion.identity, m_LayerMask);
        int i = 0;
        
        while (i < hits.Length)
        {
            if (i >= 1)
            {
                cubeRenderer.material.SetColor("_Color", Color.red);
                //Debug.Log("Hit : " + hits[i].name + i);
                detected = false;
            }
            else
            {
                cubeRenderer.material.SetColor("_Color", Color.white);
                detected = true;
            }
            i++;
        }
    }
}

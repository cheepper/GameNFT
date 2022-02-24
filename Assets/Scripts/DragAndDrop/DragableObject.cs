using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    public bool detected;
    public LayerMask m_LayerMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 4, Quaternion.identity, m_LayerMask);
        int i = 0;
        var cubeRenderer = transform.GetComponent<Renderer>();
        while (i < hits.Length)
        {
            if (i >= 1)
            {
                cubeRenderer.material.SetColor("_Color", Color.red);
                Debug.Log("Hit : " + hits[i].name + i);
            }
            else
            {
                cubeRenderer.material.SetColor("_Color", Color.white);
            }
            i++;
        }
    }
}

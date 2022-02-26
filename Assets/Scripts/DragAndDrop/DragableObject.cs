using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    public bool detected;
    public float half = 3;
    public LayerMask m_LayerMask;
    private Renderer cubeRenderer;
    [SerializeField]
    private Transform[] cubeTransformChildren;
    //[SerializeField]
    public Material opaqueMaterial;
    public Material transparentMaterial;
    public List<Renderer> cubeRendererChildren = new List<Renderer>();
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = transform.GetComponent<Renderer>();
        cubeTransformChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in cubeTransformChildren) 
        {
            cubeRendererChildren.Add(child.GetComponent<Renderer>());
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / half, transform.rotation, m_LayerMask);
        int i = 0;
        
        while (i < hits.Length)
        {
            if (i >= 1)
            {
                InvalidDetected();
                //Debug.Log("Hit : " + hits[i].name + i);
                detected = false;
            }
            else
            {
                ValidDetected();
                detected = true;
            }
            i++;
        }
    }
    public void ValidDetected() 
    {
        Color white = Color.white;
        white.a = 0.5f;
        cubeRenderer.material.SetColor("_Color", white);
        foreach (Renderer child in cubeRendererChildren)
        {
            child.material.SetColor("_Color", white);
        }
    }

    public void InvalidDetected() 
    {
        Color red = Color.red;
        red.a = 0.5f;
        cubeRenderer.material.SetColor("_Color", red);
        foreach (Renderer child in cubeRendererChildren)
        {
            child.material.SetColor("_Color", red);
        }
    }
    public void UpdateMaterial(Material mat)
    {
        //if (transparent)
        //{
        //    pendingObj.GetComponent<Renderer>().material = transparentMat;
        //}
        //else
        //{
        cubeRenderer.material = mat;
        foreach (Renderer child in cubeRendererChildren)
        {
            child.material = mat;
        }
        //}
    }
}

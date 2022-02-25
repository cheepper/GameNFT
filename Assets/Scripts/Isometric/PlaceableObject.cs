using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set; }
    private Vector3[] Vertices;
    // Start is called before the first frame update
    void Start()
    {
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Place()
    {
        ObjectDragging drag = gameObject.GetComponent<ObjectDragging>();
        Destroy(drag);

        Placed = true;
    }

    private void GetColliderVertexPositionsLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;

        Debug.Log("Box collider: " + Vertices);
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            Vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x), 
                                Mathf.Abs((vertices[0] - vertices[3]).y),
                                1);

        Debug.Log("placeable object: " + Size);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }
}

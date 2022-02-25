using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject pendingObj;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField]
    private LayerMask layerMask;

    public float gridSize;
    public bool gridOn = true;

    [SerializeField]
    private Toggle gridToggle;
    private float angle = 90;

    //public bool canRotate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingObj != null)
        {
            if (gridOn)
            {
                pendingObj.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    (pendingObj.transform.localScale.y / 2),
                    RoundToNearestGrid(pos.z)
                    );
                //Debug.Log("Pending Obj Position: " + pendingObj.transform.position);
            }
            else 
            {
                pendingObj.transform.position = pos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (CanBePlaced(pendingObj))
                {
                    PlaceObject();
                }
                else
                {
                    Debug.Log("cannot place here");
                }
                
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                RotateObject();
            }
        }
    }
    
    void PlaceObject()
    {
        pendingObj = null;
    }

    public void RotateObject() {
        pendingObj.transform.Rotate(Vector3.up, angle);
    }
    
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation);
    }

    private bool CanBePlaced(GameObject obj) {

        return obj.transform.GetComponent<DragableObject>().detected;
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        //Debug.Log("xDiff: " + xDiff);
        //Debug.Log("Pos: " + pos);
        return pos;
    }
}

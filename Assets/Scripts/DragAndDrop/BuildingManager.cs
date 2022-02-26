using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public Transform player;
    private Player playerScript;
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
    private bool isEvenBlock;
    private bool toggleBoolRotate;
    private float toggleBoolPos;
    private float rotationSpeed = 20;

    //public bool canRotate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingObj != null)
        {
            // Stop moving if started placing blocks
            playerScript.stopMoving();

            if (gridOn)
            {
                pendingObj.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x) + toggleBoolPos,
                    (pendingObj.transform.localScale.y / 2),
                    EvenBlockAdjustment(RoundToNearestGrid(pos.z)) + toggleBoolPos
                    //RoundToNearestGrid(pos.z)
                    );
                //pendingObj.transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
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
                /*
                angle += 90;
                if (angle >= 180)
                {
                    angle = 0;
                }
                Debug.Log("R: " + angle);
                */
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                CancelPlaceObject();
            }

            //SmoothRotateBlocks();
        }
    }
    
    private void PlaceObject()
    {
        DragableObject script = pendingObj.GetComponent<DragableObject>();
        script.UpdateMaterial(script.opaqueMaterial);

        pendingObj.AddComponent<UnityEngine.AI.NavMeshObstacle>();
        UnityEngine.AI.NavMeshObstacle obstacle = pendingObj.GetComponent<UnityEngine.AI.NavMeshObstacle>();
        obstacle.size = new Vector3(1.5f,1.5f,1.5f);
        obstacle.carving = true;
        pendingObj = null;
        //angle = 0;
    }

    private void CancelPlaceObject()
    {
        Destroy(pendingObj);
        pendingObj = null;
    }

    void SmoothRotateBlocks()
    {
        pendingObj.transform.rotation = Quaternion.Lerp(
        pendingObj.transform.rotation,
        Quaternion.Euler(pendingObj.transform.rotation.x, Mathf.Round(angle), 0),
        rotationSpeed * Time.deltaTime
        );
    }

    public void RotateObject() 
    {
        pendingObj.transform.Rotate(Vector3.up, angle);
        //Debug.Log(pendingObj.transform.rotation);
        if (isEvenBlock)
        {
            if (toggleBoolRotate)
            {
                toggleBoolRotate = false;
                toggleBoolPos = 0;
            }
            else
            {
                toggleBoolRotate = true;
                toggleBoolPos = -2f;
            }
            //Debug.Log("toggle rotate: " + toggleBoolRotate);
        }
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
        toggleBoolRotate = false;
        toggleBoolPos = 0;
        if (index % 2 == 1)
        {
            isEvenBlock = true;
        }
        else
        {
            isEvenBlock = false;
        }
        //Material mat = pendingObj.GetComponent<Renderer>().material;
        //mat.SetFloat
        //StandardShaderUtils.ChangeRenderMode(mat,StandardShaderUtils.BlendMode.Transparent);
        //StandardShaderUtils.ToFadeMode(mat);
        //Debug.Log("index: " + index);
        //Debug.Log("isEvenBlock: " + isEvenBlock);
    }

    private float EvenBlockAdjustment(float size) {
        if (isEvenBlock)
        {
            return size - 2f;
        }
        else
        {
            return size;
        }
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

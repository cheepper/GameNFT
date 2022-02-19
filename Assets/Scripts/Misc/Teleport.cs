using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform portDestination;
    public Transform player;
    //public Transform globalLight;
    //public Transform playerSpotlight;
    public bool isUsingDoor = false;
    public bool isEnteringBuilding;
    public bool isEnteringDungeon;
    public Canvas canvas;
    private Transition transition;
    private UnityEngine.AI.NavMeshAgent MeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        MeshAgent = player.GetComponent <UnityEngine.AI.NavMeshAgent> ();
        transition = canvas.gameObject.GetComponent<Transition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsingDoor) {
            StartCoroutine(transition.fadeAndOut(() =>
            {
                
                Invoke("warpDestination", 0.0f);
                Debug.Log("on complete");
            }
            ));
            isUsingDoor = false;
            Debug.Log("isUsingDoor");
        }
    }

    bool warpDestination() {
        if (isEnteringBuilding)
        {
            //globalLight.gameObject.SetActive(false);
            //playerSpotlight.gameObject.SetActive(false);
        }
        else if (isEnteringDungeon)
        {
            //globalLight.gameObject.SetActive(false);
            //playerSpotlight.gameObject.SetActive(true);
        }
        else 
        {
            //globalLight.gameObject.SetActive(true);
            //playerSpotlight.gameObject.SetActive(false);

        }
        return MeshAgent.Warp(portDestination.transform.position);
    }
}

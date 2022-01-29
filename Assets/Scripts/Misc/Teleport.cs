using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform portDestination;
    public Transform player;
    //public Transform globalLight;
    //public Transform playerSpotlight;
    public bool useTransition = false;
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
        if (useTransition) {
            //canvas.gameObject.SetActive(true);
            transition.container.gameObject.SetActive(false);
            StartCoroutine(transition.waitDoorTransition());
            Invoke("WarpDestination", 0.5f);
            useTransition = false;
            Debug.Log("useTransition");
        }
    }

    public void WarpDestination() {
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
        MeshAgent.Warp(portDestination.transform.position);
    }
}

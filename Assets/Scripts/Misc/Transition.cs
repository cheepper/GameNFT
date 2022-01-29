using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Transform fadesIn;
    public Transform fadesOut;
    private Animator FadesInAnimator;
    private Animator FadesOutAnimator;
    public Transform container;
    // Start is called before the first frame update
    void Start()
    {
        FadesInAnimator = fadesIn.gameObject.GetComponent<Animator>();
        FadesOutAnimator = fadesOut.gameObject.GetComponent<Animator>();
        StartCoroutine(waitSceneLoad());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitSceneLoad()
    {
        yield return new WaitForSeconds(1);
        fadesIn.gameObject.SetActive(false);
        fadesOut.gameObject.SetActive(false);
        container.gameObject.SetActive(false);
        //transform.root.gameObject.SetActive(false);
        Debug.Log("End wait scene load");
    }

    public IEnumerator waitDoorTransition()
    {
        fadesIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        fadesIn.gameObject.SetActive(false);
        fadesOut.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        fadesOut.gameObject.SetActive(false);
        container.gameObject.SetActive(false);
        //transform.root.gameObject.SetActive(false);
        Debug.Log("teleport transition");
    }
}

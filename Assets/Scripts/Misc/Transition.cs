using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Transform curtainFadesIn;
    public Transform curtainFadesOut;
    private Animator FadesInAnimator;
    private Animator FadesOutAnimator;
    public Transform container;
    // Start is called before the first frame update
    void Start()
    {
        FadesInAnimator = curtainFadesIn.gameObject.GetComponent<Animator>();
        FadesOutAnimator = curtainFadesOut.gameObject.GetComponent<Animator>();
        StartCoroutine(waitSceneLoad());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitSceneLoad()
    {
        yield return new WaitForSeconds(FadesOutAnimator.GetCurrentAnimatorStateInfo(0).length);
        curtainFadesIn.gameObject.SetActive(false);
        curtainFadesOut.gameObject.SetActive(false);
        container.gameObject.SetActive(false);
        //transform.root.gameObject.SetActive(false);
        Debug.Log("End wait scene load");
    }

    /*
    void waitDoorTransition()
    {
        container.gameObject.SetActive(false);
        //transform.root.gameObject.SetActive(false);
        Debug.Log("teleport transition");
    }
    */

    public IEnumerator fadeAndOut(System.Action OnComplete) {
        curtainFadesIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(FadesInAnimator.GetCurrentAnimatorStateInfo(0).length);
        curtainFadesIn.gameObject.SetActive(false);
        OnComplete();
        curtainFadesOut.gameObject.SetActive(true);
        yield return new WaitForSeconds(FadesOutAnimator.GetCurrentAnimatorStateInfo(0).length);
        curtainFadesOut.gameObject.SetActive(false);
    }
}

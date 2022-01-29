using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    public Transform pointAxis;
    Vector3 forward, oppositeForward, right, oppositeRight;
    public GameObject front, back;
    //If not right is left, if not front back.
    public bool isRight;
    public int indexChild;
    private Transform spriteCharacter;

    private void Start()
    {
        //this just assume that body is the first child, you can change it.
        spriteCharacter = transform.GetChild(indexChild);
    }

    void Update() {
    }

    private void LateUpdate()
    {
        forward = transform.TransformDirection(Vector3.forward);
        right = transform.TransformDirection(Vector3.right);

        oppositeForward = pointAxis.position - transform.position;
        oppositeRight = pointAxis.position - transform.position;
        if (Vector3.Dot(oppositeForward, forward) < 0)
        {
            //front.SetActive(false);
            //back.SetActive(true);
            // Facing back
            front.transform.localPosition = new Vector3(0,-300,0);
            back.transform.localPosition = new Vector3(0,0,0);
        }
        else
        {
            // front.SetActive(true);
            // back.SetActive(false);
            // Facing front
            front.transform.localPosition = new Vector3(0,0,0);
            back.transform.localPosition = new Vector3(0,-300,0);
        }
        if (Vector3.Dot(oppositeRight, right) < 0)
        {
            isRight = false;
            front.transform.localScale = Vector3.one;
            back.transform.localScale = Vector3.one;
        }
        else
        {
            isRight = true;
            //this place mirror the sprite to make it appear as different angle
            front.transform.localScale = new Vector3(-1f, 1f, 1f);
            back.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //Debug.Log("forward: " + Vector3.Dot(oppositeForward, forward));
        //Debug.Log("right: " + Vector3.Dot(oppositeRight, right));

        spriteCharacter.forward = pointAxis.position - transform.position;
    }
}


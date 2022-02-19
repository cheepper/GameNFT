using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [Header("Destroy loot at")]
    public float duration = 5;
    private Vector3 velocity = Vector3.up;
    private Rigidbody rb;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        velocity *= Random.Range(50f, 70f);
        velocity += new Vector3(Random.Range(-17.5f, 17.5f), 0, Random.Range(-17.5f, 17.5f));

        rb = this.GetComponent<Rigidbody>();
        //rb.useGravity = false;
        //rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.position += velocity * 1.5f * Time.deltaTime;

        if (velocity.y < -50f)
        {
            velocity.y = -50f;
        }
        else 
        {
            velocity -= Vector3.up * 200 * Time.deltaTime;
        }

        if (Mathf.Abs(rb.position.y - startPosition.y) < 0.25f && velocity.y < 0f) {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = velocity;
            this.enabled = false;
        }

        Destroy(transform.gameObject, duration);
    }
}

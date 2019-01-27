using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BouncingPlayer : MonoBehaviour {

    public float force = 10.0f;
    Rigidbody rb;
    bool grounded = false;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.Space) & grounded)
        {
            rb.AddForce(Vector3.up * force * 10.0f);
            grounded = false;
        }
        rb.AddForce(new Vector3(horiz * force, 0.0f, vert * force));

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(AudioSource))]
public class BouncingPlayer : MonoBehaviour
{

    public float force = 10.0f;
    Rigidbody rb;
    AudioSource bounce;
    bool grounded = false;

    float timer = 0.0f;
    public float bounce_timer = 0.1f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        bounce = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {

        if (timer > bounce_timer)
        {
            bounce.pitch = Random.Range(0.95f, 1.05f);
            bounce.volume = collision.relativeVelocity.magnitude / 5.0f;
            bounce.Play();
            timer = 0.0f;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.Space) & grounded)
        {
            rb.AddForce(Vector3.up * force * 10.0f);
            grounded = false;
        }
        rb.AddForce(new Vector3(horiz * force, 0.0f, vert * force));

        timer += Time.deltaTime;

    }

    
}

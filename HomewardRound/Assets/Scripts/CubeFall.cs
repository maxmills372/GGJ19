using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Rigidbody))]
public class CubeFall : MonoBehaviour
{
    Rigidbody rb;
    float timer = 0f;
    float random_time = 0f;

    // Use this for initialization
    void Awake ()
    {
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        random_time = Random.Range(0.1f, 1.0f);

        this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        //Physics.IgnoreLayerCollision(10, 10);
    } 
	
   
	// Update is called once per frame
	void Update ()
    {   
        timer += Time.deltaTime;
        if (timer > random_time)
        {
            rb.isKinematic = false;
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, new Color(3.0f, 3.0f, 3.0f), timer));

        }
        else
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * 5.0f);
        }

        


    }
}

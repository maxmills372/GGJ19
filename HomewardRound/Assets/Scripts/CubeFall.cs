using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Rigidbody))]
public class CubeFall : MonoBehaviour
{
    float random_start_time = 1.0f; // 1 // 0.25f
    float random_end_time = 1.8f; // 2 // 1.25f


    Rigidbody rb;
    float timer = 0f;
    float random_time = 0f;
    GameObject lvl_controller;

    float emm_timer = 0.0f;

    // Use this for initialization
    void Awake ()
    {
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
       // rb.mass = 15.0f;

        random_time = Random.Range(random_start_time, random_end_time);
        lvl_controller = GameObject.FindGameObjectWithTag("LevelController");
        this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        //Physics.IgnoreLayerCollision(10, 10);
    } 
	
   
	// Update is called once per frame
	void Update ()
    {   
        timer += Time.deltaTime;

        if (timer > random_time / 3.0f)
        {
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, new Color(3.0f, 1.0f, 0.0f), emm_timer));
            emm_timer += Time.deltaTime;
        }
        if (timer > random_time)
        {
            rb.isKinematic = false;
        }
        else
        {
            transform.position += (new Vector3(0.0f, 0.0f, -1.0f) * Time.deltaTime * lvl_controller.GetComponent<LevelController>().m_MoveSpeed);
        }

        


    }
}

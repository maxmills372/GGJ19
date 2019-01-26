using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParent : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        Invoke("Dead", 5.0f);
    }

    void Dead()
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update ()
    {
		
	}
}

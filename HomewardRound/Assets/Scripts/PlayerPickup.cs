using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    //TO BE COMBINED WITH PLAYER CONTROLLER

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "pickup")
        {
            col.gameObject.GetComponent<PickUp>().Activate();
        }
        else if (col.gameObject.tag == "obstacle")
        {

        }
    }
}

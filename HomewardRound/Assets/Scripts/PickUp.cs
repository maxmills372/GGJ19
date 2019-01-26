using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum pick_up { COLLECT, SPEED, MAGNET, MULTIPLIER };
    public pick_up type;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Activate()
    {
        if (type == pick_up.SPEED)
        {
            // Decrease speed of level movement for 5 seconds
        }
        if (type == pick_up.MAGNET)
        {
            // Enable magnet collider
            // Collect animals
        }
        if (type == pick_up.MULTIPLIER)
        {
            // Increase score multiplier
        }
        if (type == pick_up.COLLECT)
        {
            // Collect
        }
    }
}

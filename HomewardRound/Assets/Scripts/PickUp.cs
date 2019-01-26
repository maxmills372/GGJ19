using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum pick_up { COLLECT, SPEED, MAGNET, MULTIPLIER };
    public pick_up type;
    public GameObject score;
    float speed = 1.0f;

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
        }
        if (type == pick_up.MULTIPLIER)
        {
            // Increase score multiplier
            score.GetComponent<Score>().Enable_Multiplier();
        }
        if (type == pick_up.COLLECT)
        {
            // Collect
            score.GetComponent<Score>().Update_Score();
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type == pick_up.COLLECT)
        {
            if (other.gameObject.tag == "Player")
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, other.gameObject.transform.position, step);
            }
        }
    }
}

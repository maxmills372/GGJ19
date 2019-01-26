using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum pick_up { COLLECT, SPEED, MAGNET, MULTIPLIER };
    public pick_up type;
    public GameObject score;
    public GameObject player;
    public GameObject lvl_cont;
    float speed = 7.0f;

    bool moving = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("Score");
        lvl_cont = GameObject.FindGameObjectWithTag("LevelController");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, step);   
        }

        if(player.GetComponent<PlayerController>().magnet == false)
        {
            moving = false;
        }
    }

    public void Activate()
    {
        if (type == pick_up.SPEED)
        {
            // Decrease speed of level movement for 5 seconds
            lvl_cont.GetComponent<LevelController>().ChangeSpeed();

        }
        if (type == pick_up.MAGNET)
        {
            // Enable magnet collider
            player.GetComponent<PlayerController>().EnableMagnet();
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
            if (other.gameObject.tag == "Player" && other.GetComponent<PlayerController>().magnet == true)
            {
                moving = true; 
            }
        }
    }
}

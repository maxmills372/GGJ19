using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float x_speed = 5.0f;
    public float y_speed = 5.0f;
    public float z_speed = 5.0f;
    public float speed_mult = 1.0f;

    float x_acceleration = 0.0f;
    float y_acceleration = 0.0f;
    float z_acceleration = 0.0f;
    float x_acc_mult = 1.0f;
    public float y_acc_mult = 4.0f;

    public float x_force;
    public float y_force;
    public float z_force;

    public float x_stop;
    float y_stop;
    float z_stop;

    public int score = 0;
    public int score_mult = 1;

    bool grounded = true;

    bool dead = false;

    public bool magnet = false;
    float count = 0.0f;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        y_stop = transform.position.y;
        z_stop = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }
	
    void xMove()
    {
        bool button_pressed = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            button_pressed = true;
            // Move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                x_acceleration -= Time.deltaTime * x_force * 2.0f;
                if (x_acceleration < -1.0f)
                {
                    x_acceleration = -1.0f;
                }    
            }

            // Move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                x_acceleration += Time.deltaTime * x_force * 2.0f;
                if (x_acceleration > x_force)
                {
                    x_acceleration = x_force;
                }                
            }

        }
        else
        {
            button_pressed = false;
        }

        if(!button_pressed)
        {
            if (x_acceleration < 0.05 && x_acceleration > -0.05)
            {
                x_acceleration = 0.0f;
            }
        }

        // Going right
        if (x_acceleration > 0)
        {
            x_acceleration -= Time.deltaTime * x_acc_mult;
            
        }
        // Going left
        else if (x_acceleration < 0)
        {
            x_acceleration += Time.deltaTime * x_acc_mult;
        }

        if(transform.position.x > x_stop || transform.position.x < -x_stop)
        {
            x_acceleration *= -1.0f;
            
        }

    }

    void yMove()
    {
        // If in air
        if (transform.position.y > y_stop)
        {
            x_acc_mult = 0.5f;

            if (y_acceleration > 0.0f)
            {
                y_acceleration -= Time.deltaTime * y_acc_mult;
            }
            // Accelerates more while going down
            else
            {
                y_acceleration -= Time.deltaTime * y_acc_mult * 2.0f;
            }
        }
        // On the ground, set to default y position
        else
        {
            x_acc_mult = 1.0f;
            y_acceleration = -1.0f * (y_acceleration / 2.5f);

            if(y_acceleration < 0.05 && y_acceleration > -0.05)
            {
                y_acceleration = 0.0f;
            }
            grounded = true;

            transform.position = new Vector3(transform.position.x, y_stop, transform.position.z);
        }

        // Jumps
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            y_acceleration = y_force;
            grounded = false;
        }
    }

    void zMove()
    {
        // Has been hit
        if (transform.position.z < z_stop)
        {
            z_acceleration += Time.deltaTime;
        }
        // Sets to default z position
        else
        {
            z_acceleration = 0.0f;

            transform.position = new Vector3(transform.position.x, transform.position.y, z_stop);
        }

        if(transform.position.z < -9.5)
        {
            dead = true;
            Destroy(rb);
            

            //rb.isKinematic = false;
        }
    }

    public void HitObstacle()
    { 
        z_acceleration = -z_force;
    }

    void Move()
    {
        xMove();
        yMove();
        zMove();

        // Gets hit
        if (Input.GetKeyDown(KeyCode.F))
        {
            HitObstacle();
        }

        // Move player
        transform.position += new Vector3(
            x_speed * speed_mult * x_acceleration * Time.deltaTime, 
            y_speed *              y_acceleration * Time.deltaTime, 
            z_speed *              z_acceleration * Time.deltaTime);
        
    }

	// Update is called once per frame
	void Update ()
    {
        if (!dead)
        {
            Move();
        }

        if (magnet == true)
        {
            count += Time.deltaTime;
            if (count >= 5.0f)
            {
                magnet = false;
                count = 0.0f;
            }
        }

        if(GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
	}

    public void EnableMagnet()
    {
        magnet = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "pickup")
        {
            col.gameObject.GetComponent<PickUp>().Activate();
        }
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            z_acceleration = -z_force;
            transform.position -= new Vector3(0.0f, 0.0f, 0.001f);
            collision.gameObject.SendMessage("BreakIt");
        }
    }
}

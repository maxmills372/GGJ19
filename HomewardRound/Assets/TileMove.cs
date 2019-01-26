using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    public float m_MoveSpeed = 5f;
    GameObject controller;

    public GameObject cubeParent;

    // Use this for initialization
    void Start ()
    {
		
	}
	
    public void Init(GameObject g)
    {
        controller = g;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * m_MoveSpeed);

        if (transform.position.z < -5f)
        {
            Instantiate(cubeParent, transform.position, Quaternion.identity, null);

            Destroy(this.gameObject);
        }

    }
}

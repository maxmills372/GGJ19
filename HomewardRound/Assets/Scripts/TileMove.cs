using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    //public float m_MoveSpeed = 3f;
    GameObject controller;

    public GameObject cubeParent;

    Manager manager_;
    GameObject lvl_controller;
    GameObject m_plane_fall;

    public bool is_tile = false;

    // Use this for initialization
    void Start ()
    {
        
        lvl_controller = GameObject.FindGameObjectWithTag("LevelController");
        m_plane_fall = GameObject.FindGameObjectWithTag("FallPlane");
    }
	
    public void Init(GameObject g)
    {
        controller = g;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.position += (new Vector3(0.0f, 0.0f, -1.0f) * Time.deltaTime * lvl_controller.GetComponent<LevelController>().m_MoveSpeed);

        if (transform.position.z < m_plane_fall.transform.position.z)
        {
            if (is_tile)
            {
                Instantiate(cubeParent, transform.position, Quaternion.identity, null);

                Destroy(this.gameObject);
            }
        }

    }
}

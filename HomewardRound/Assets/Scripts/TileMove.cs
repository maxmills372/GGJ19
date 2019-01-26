using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour //Singleton<Manager>
{
    public float m_MoveSpeed = 3.0f;
}

public class TileMove : MonoBehaviour
{
    //public float m_MoveSpeed = 3f;
    GameObject controller;

    public GameObject cubeParent;

    Manager manager_;

    // Use this for initialization
    void Start ()
    {
        manager_ = Manager.instance;
        Debug.Log(manager_.m_MoveSpeed);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelMove : MonoBehaviour {

    public float movement_speed;
    public GameObject level_prefab;
    public GameObject level_start;
    float m_ZSpawnPos = -300.0f;
    float m_ZDeletePos = 300.0f;
    GameObject current_level_plane;
    float half_length;
    public float m_planeZScale = 30f;
    public Transform spawn_pos;
    public Transform delete_pos;


    GameObject[] level_platforms;
    GameObject temp;

    // Use this for initialization
    void Start()
    {
        m_ZSpawnPos = spawn_pos.position.z;
        m_ZDeletePos = delete_pos.position.z;

        level_platforms = new GameObject[2];
        level_platforms[0] = level_start;
        level_platforms[1] = Instantiate(level_prefab, new Vector3(0f, 0f, m_ZSpawnPos), Quaternion.identity) as GameObject;

       
        half_length = (m_planeZScale * 10.0f)/ 2.0f;

    }

    // Update is called once per frame
    void Update () {

        level_platforms[0].transform.Translate(Vector3.forward * movement_speed * Time.deltaTime);
        level_platforms[1].transform.Translate(Vector3.forward * movement_speed * Time.deltaTime);

        if (/*!level_platforms[0].GetComponentInChildren<IsVisible>().is_visible)*/ level_platforms[0].transform.position.z + half_length > m_ZDeletePos)
        {
            temp = level_platforms[0];
            level_platforms[0] = level_platforms[1];
            level_platforms[1] = Instantiate(level_prefab, new Vector3(0f, 0f, temp.GetComponent<Transform>().position.z - (half_length * 4.0f)), Quaternion.identity) as GameObject;

            //Destroy(level_platforms[0]);
            Destroy(temp);
           

            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public GameObject tile_prefab;
    public int m_noOfTiles = 10;
    Manager manager_;
    public float m_MoveSpeed = 5.0f;
    public float normal_speed = 5.0f;
    public float slow_speed = 2.5f;

    public float timer = 0.0f;

    float length;

    float freq = 0.0f;

    public GameObject[] obstcales;
    public GameObject[] pick_ups;
    public GameObject[] animals;
    public GameObject home;

    public float win_time = 5.0f;
    float win_timer = 0.0f;

    public float obstacle_spawn_rate;
    public float pick_up_spawn_rate;
    public float animal_spawn_rate;

    bool house_spawned = false;

    // Use this for initialization
    void Start ()
    {

        length = tile_prefab.transform.localScale.z;

        for (int i = 0; i < m_noOfTiles; i++)
        {
            GameObject g = Instantiate(tile_prefab, new Vector3(0f, 0f, i * length), Quaternion.identity);
            
            g.GetComponent<TileMove>().Init(this.gameObject);

        }
    }
	
    public void TileDead()
    {
        // Creates new tile at the start of the tiles
        GameObject g = Instantiate(tile_prefab, new Vector3(0f, 0f, (m_noOfTiles * length) - length), Quaternion.identity);
       
        g.GetComponent<TileMove>().Init(this.gameObject);
    }
    
    IEnumerator SpeedWait()
    {
        yield return new WaitForSeconds(3.0f);

        m_MoveSpeed = normal_speed;

        yield return null;
    }

    public void ChangeSpeed()
    {
        m_MoveSpeed = slow_speed;
        StartCoroutine(SpeedWait());
    }

    void SpawnObj(float rate, GameObject[] objs, float height)
    {
        if ((int)Random.Range(0, rate) == 0)
        {
            Instantiate(objs[Random.Range(0, objs.Length)], new Vector3(Random.Range(-5, 6), height, m_noOfTiles - length), Quaternion.identity);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        freq = 0.9f / m_MoveSpeed;
        // Makes a new Tile at given frequency
        if (timer > freq)
        {
            TileDead();
            timer = 0.0f;
        }

        timer += Time.deltaTime;

        if (!house_spawned)
        {
            if (win_timer > win_time)
            {
                Instantiate(home, new Vector3(0f, 0f, (m_noOfTiles * length) - length), Quaternion.identity);
                house_spawned = true;
            }
            else
            {
                SpawnObj(obstacle_spawn_rate, obstcales, 1.0f);
                SpawnObj(animal_spawn_rate, animals, 2.0f);
                SpawnObj(pick_up_spawn_rate, pick_ups, 1.0f);
            }
        }
        win_timer += Time.deltaTime;
    }
}

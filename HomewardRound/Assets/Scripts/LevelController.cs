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
                if ((int)Random.Range(0, obstacle_spawn_rate) == 0)
                {
                    Instantiate(obstcales[Random.Range(0, obstcales.Length)], new Vector3(Random.Range(-5, 6), 1.0f, m_noOfTiles), Quaternion.identity);
                }
                else if ((int)Random.Range(0, pick_up_spawn_rate) == 0)
                {
                    Instantiate(pick_ups[Random.Range(0, pick_ups.Length)], new Vector3(Random.Range(-5, 6), 1.0f, m_noOfTiles), Quaternion.identity);
                }
                else if ((int)Random.Range(0, animal_spawn_rate) == 0)
                {
                    Instantiate(animals[Random.Range(0, animals.Length)], new Vector3(Random.Range(-5, 6), 1.0f, m_noOfTiles), Quaternion.identity);
                }
            }
        }
        win_timer += Time.deltaTime;
    }
}

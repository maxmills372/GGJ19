using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject tile_prefab;
    public int m_noOfTiles = 10;

    public float timer = 0.0f;

    float length;

    float freq = 0.0f;

    // Use this for initialization
    void Start () {

        length = tile_prefab.transform.localScale.z;
        move_speed
        freq = 0.9f / move_speed;

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
    
	// Update is called once per frame
	void Update ()
    {
        // Makes a new Tile at given frequency
        if(timer > 0.3f)
        {
            TileDead();
            timer = 0.0f;
        }
        timer += Time.deltaTime;

      
    }
}

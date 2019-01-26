using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject tile_prefab;
    public int m_noOfTiles = 10;
    Manager manager_;
     public float m_MoveSpeed;

    public float timer = 0.0f;

    float length;

    float freq = 0.0f;

    // Use this for initialization
    void Start () {

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

      
    }
}

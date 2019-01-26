using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject tile_prefab;
    public int m_noOfTiles = 10;

    public float timer = 0.0f;

    float length;

    // Use this for initialization
    void Start () {

        length = tile_prefab.transform.localScale.z ;
        for (int i = 0; i < m_noOfTiles; i++)
        {
            GameObject g = Instantiate(tile_prefab, new Vector3(0f, 0f, i * length), Quaternion.identity);
            g.GetComponent<TileMove>().Init(this.gameObject);

        }
        //Invoke("TileDead", timer);
    }
	
    public void TileDead()
    {
        GameObject g = Instantiate(tile_prefab, new Vector3(0f, 0f, (m_noOfTiles * length) - length), Quaternion.identity);
        g.GetComponent<TileMove>().Init(this.gameObject);
        //Invoke("TileDead", timer);
    }



	// Update is called once per frame
	void Update ()
    {
        
        if(timer > 0.18f)
        {
            TileDead();
            timer = 0.0f;
        }
        timer += Time.deltaTime;

        //if (transform.position.z > -4.0f)
        //{

        //    //Instantiate(this.gameObject, new Vector3(0f, 0f, temp.GetComponent<Transform>().position.z - (half_length * 4.0f)), Quaternion.identity) as GameObject;

        //    //Destroy(level_platforms[0]);
        //    //Destroy(this.gameObject);



        //}
    }
}

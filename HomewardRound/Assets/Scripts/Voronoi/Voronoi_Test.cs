﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Voronoi_Test : MonoBehaviour 
{
	public int resolution = 50;
	public int region_amount = 5;
	public bool m_VoronoiBreak = false;
	public bool create_grid = false;
	public bool voronoi_break = false;
	public bool combine_meshes = false;
	public bool give_rigidbodies = false;
	public bool reset_object = false;

	public bool find_objects = false;
	public GameObject region_clone;
	public GameObject region_parent;
	public Rigidbody rigidbody_clone;
    public Slider slider1, slider2;

    public Object_Pool object_pooler;

	GameObject[,,] grid_;
	int grid_size;

	Vector3 start_pos, object_scale;

	float closest_dist;
	int temp_int = 0;
	float temp_dist = 0.0f;
	Color temp_color;
	bool created_ = false;
	bool broken_up = false; 
	bool combined = false;

    

	[System.Serializable]
	public struct region
	{
		public GameObject control_point;
		public List<GameObject> members;
		public GameObject parent;
		public float temp_dist;
		public Color color;
	}
	region[] regions;


	// Use this for initialization
	void Awake () 
	{
		object_pooler = Object_Pool.instance;

		if(find_objects)
		{
			region_clone = transform.parent.Find("Region_Clone").gameObject;
			region_parent= transform.parent.Find("Region_Parent").gameObject;
			rigidbody_clone = transform.parent.Find("Rigidbody_Clone").gameObject.GetComponent<Rigidbody>();
		}
        temp_color = GetComponent<Renderer>().material.color;

       
    }

    
    void BreakIt()
    {
        float st_ = Time.realtimeSinceStartup;

        //m_VoronoiBreak = false;
        //StartCoroutine("BreakObject");
        CreateGrid();
        VoronoiBreak();
        CombineMeshes();
        AddRigidbodies();
        GetComponent<AudioSource>().Play();

        float et_ = Time.realtimeSinceStartup;
        print("Finished voronoi break: " + (et_ - st_));
    }

    // Update is called once per frame
    void Update ()
	{
        //resolution = (int)slider1.value;
        //region_amount = (int)slider2.value;

        /*if (Input.GetMouseButtonDown(0))
        {
            m_VoronoiBreak = true;
            //BreakIt();
            print("test");
        }
        else
        {
            m_VoronoiBreak = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            reset_object = true;
            print("reset");
        }
        else
        {
            reset_object = false;
        }*/

        if (m_VoronoiBreak)
		{
            BreakIt();
		}

		if(create_grid)
		{
			CreateGrid();
		}
		// If broken and grid is created
		if(m_VoronoiBreak && created_)
		{
			VoronoiBreak();
		}
		if(combine_meshes && broken_up)
		{
			CombineMeshes();
		}
		if(give_rigidbodies && combined)
		{
			AddRigidbodies();
		}
		if(reset_object)
		{			
			object_pooler.pool_dictionary["Cube"].Clear();
			object_pooler.AddToPool("Cube",resolution * resolution*resolution);
			Reset_Object();
		}

        if(voronoi_break)
        {
            //voronoi_break = false;
        }
	}

	// Waits until functions are finished -- not needed as does this already, i just like the code
	IEnumerator BreakObject()
	{

		yield return new WaitUntil(() => CreateGrid());
		yield return new WaitUntil(() => VoronoiBreak());
		yield return new WaitUntil(() => CombineMeshes());

		AddRigidbodies();

		yield return null;
	}

	public bool CreateGrid()
	{
		Reset_Object();

		// Total grid size (for a cube)
		grid_size = resolution * resolution * resolution;

		// Create 3D grid
		grid_ = new GameObject[resolution, resolution, resolution];

		// Bottom left of object
		start_pos = transform.position - new Vector3(transform.localScale.x/2.0f, transform.localScale.y/2.0f, transform.localScale.z/2.0f);

		// Calculate the scale of each object
		object_scale = new Vector3(this.transform.localScale.x/resolution,this.transform.localScale.y/resolution, this.transform.localScale.z/resolution);

		// Create grid of Gameobjects
		GameObject temp_object;
		for(int i = 0; i<resolution; i++)
		{
			for(int j = 0; j<resolution; j++)
			{
				for(int k = 0; k<resolution; k++)
				{
					// Spawn a gameobject at start position using the objects from the object pool
					temp_object = //Instantiate(test_cube, start_pos, Quaternion.identity, grid_parent.transform) as GameObject;
								  object_pooler.SpawnFromPool("Cube", start_pos, Quaternion.identity, region_parent.transform);

					// Set new position and scale
					temp_object.transform.localScale = object_scale;// new Vector3(this.transform.localScale.x/resolution,test_cube.transform.localScale.y,this.transform.localScale.z/resolution);
					temp_object.transform.position = start_pos 
						+ new Vector3(object_scale.x/2.0f, object_scale.y/2.0f, object_scale.z/2.0f)
						+ new Vector3((this.transform.localScale.x/resolution)*i, (this.transform.localScale.y/resolution)*j, (this.transform.localScale.z/resolution)*k);

					// Assign cube
					grid_[i,j,k] = temp_object;	
				}
			}
		}

		// Create array of regions
		regions = new region[region_amount];

		// Create random region positions
		for(int r = 0; r<region_amount; r++)
		{
			// Create new region at random point
			GameObject region_sphere = Instantiate(region_clone,transform.position,Quaternion.identity,region_parent.transform) as GameObject;
			region_sphere.transform.position = grid_[Random.Range(0,resolution),Random.Range(0,resolution),Random.Range(0,resolution)].transform.position;

			// Assign control point
			regions[r].control_point = region_sphere;

            // Assign random colour
            // new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
			region_sphere.GetComponent<Renderer>().material.color = temp_color;
			regions[r].color = temp_color;		

			// Assign parent
			regions[r].parent = region_sphere;

			regions[r].members = new List<GameObject>();

            //regions[r].control_point.GetComponent<MeshRenderer>().enabled = false;

        }

		// Hide actual object
		this.GetComponent<MeshRenderer>().enabled = false;

		create_grid = false;
		created_ = true;
		return true;
	}

	public bool VoronoiBreak()
	{
        // Hide actual object
        //this.GetComponent<MeshRenderer>().enabled = false;

        // Find closest region for every object
        for (int i = 0; i<resolution; i++)
		{
			for(int j = 0; j<resolution; j++)
			{
				for(int k = 0; k<resolution; k++)
				{
					closest_dist = 1000000.0f;
					// For every region
					for(int r = 0; r<region_amount; r++)
					{
						// Distance between object and region control point
						temp_dist = Euclidean_Dist_3D(grid_[i,j,k].transform.position, regions[r].control_point.transform.position);

                        // If closer than current closest
                        if (temp_dist < closest_dist)
						{	
							// Update closest and store region array identifier
							closest_dist = temp_dist;
							temp_int = r;
						}					
					}

					// Add closest to that region
					regions[temp_int].members.Add(grid_[i,j,k]);
				}
			}
		}

		// For every region member 
		for(int r = 0; r<region_amount; r++)
		{
			foreach(GameObject m in regions[r].members)
			{
				// Assign a parent and color of region
				m.transform.parent = regions[r].control_point.transform;
				m.GetComponent<Renderer>().material.color = regions[r].color;

               // m.GetComponent<MeshRenderer>().enabled = true;

            }
            //regions[r].control_point.SendMessage("CombineChildMeshes");
            //regions[r].control_point.AddComponent<Rigidbody>();
        }

        // Stop breaking
        m_VoronoiBreak = false;
		broken_up = true;
		created_ = false;

		return true;

	}
	// Sends meesage to all regions to copmbine all child meshes into one
	bool CombineMeshes()
	{	
		// For every region member 
		for(int r = 0; r<region_amount; r++)
		{
			regions[r].control_point.SendMessage("CombineChildMeshes");
		}

		combine_meshes = false;
		combined = true;

		return true;
	}

	// Gives every combined mesh a rigidbody with the components of the RB clone
	void AddRigidbodies()
	{
		give_rigidbodies = false;

		// Disables this objects collider
		this.GetComponent<Collider>().enabled = false;

		// For every region member 
		for(int r = 0; r<region_amount; r++)
		{
			// Add a rigidbody with components from the clone
			regions[r].control_point.AddComponent<Rigidbody>();
			regions[r].control_point.GetComponent<Rigidbody>().mass = rigidbody_clone.mass;
			regions[r].control_point.GetComponent<Rigidbody>().drag = rigidbody_clone.drag;
			regions[r].control_point.GetComponent<Rigidbody>().angularDrag = rigidbody_clone.angularDrag;
			regions[r].control_point.GetComponent<Rigidbody>().useGravity = rigidbody_clone.useGravity;
			regions[r].control_point.GetComponent<Rigidbody>().isKinematic = rigidbody_clone.isKinematic;

						
		}
	}

	void Reset_Object()
	{
		if(created_)
		{
			for(int i = 0; i<resolution; i++)
			{
				for(int j = 0; j<resolution; j++)
				{
					for(int k = 0; k<resolution; k++)
					{
						//Destroy(grid_[i,j,k]);
						grid_[i,j,k].SetActive(false);
						grid_[i,j,k].transform.parent = object_pooler.transform;

					}
				}
			}

			for(int r = 0; r<region_amount; r++)
			{ 

				Destroy(regions[r].control_point);
				//regions[r].control_point.SetActive(false);

			}
		}

        m_VoronoiBreak = false;
		created_ = false;
		broken_up = false;

		reset_object = false;
		this.GetComponent<MeshRenderer>().enabled = true;
		this.GetComponent<Collider>().enabled = true;
	}

	// Distance in the XZ Plane
	float Euclidean_Dist(Vector3 a, Vector3 b)
	{
		return Mathf.Sqrt(Mathf.Pow((a.x - b.x),2.0f) + Mathf.Pow((a.z - b.z),2.0f)); 
	}

	// Distance in the XYZ Plane
	float Euclidean_Dist_3D(Vector3 a, Vector3 b)
	{
		return Mathf.Sqrt(Mathf.Pow((a.x - b.x),2.0f) + Mathf.Pow((a.y - b.y),2.0f) + Mathf.Pow((a.z - b.z),2.0f)); 
	}

    

    

}

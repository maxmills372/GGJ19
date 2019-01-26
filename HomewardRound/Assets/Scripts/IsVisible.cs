using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour {

    public bool is_visible = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        is_visible = this.GetComponent<Renderer>().isVisible;
	}
}

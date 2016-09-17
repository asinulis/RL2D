using UnityEngine;
using System.Collections;

public class UpdateZCoordinate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		adjustPosition ();
	}
	
	// Update is called once per frame
	void OnCollisionStay2D (Collision2D other) {
		adjustPosition ();
	}

	void adjustPosition(){
		//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}
}
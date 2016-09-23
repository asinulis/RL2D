using System;
using UnityEngine;

public class Environment : RLObject {

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("Environment has been hit by " + other.transform.name);
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Environment was triggered by " + other.name);
		//if(!other.transform.parent.GetComponent<Unit>()) leaveShellCasingOnGround ();
	}
}
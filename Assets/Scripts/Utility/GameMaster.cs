using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public static void DeactivateObject(GameObject obj){
		// test description
		obj.SetActive (false);
	}

	public static void ActivateObject(GameObject obj){
		obj.SetActive (true);
	}

	public static void DeactivateCollider(Collider2D obj){
		obj.enabled = false;
	}
}

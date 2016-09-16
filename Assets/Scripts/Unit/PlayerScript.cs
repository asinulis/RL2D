using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	UnitStats stats;

	// Use this for initialization
	void Start () {
		stats = new UnitStats (gameObject, new List<AbstractEffect>{new SprintEffect()});
		GetComponent<PlayerMovement>().setPlayerStats (stats);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			stats.triggerEffect ();
		} else {
			stats.untriggerEffect ();
		}
	
	}
}

using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	
	void FixedUpdate () {
		Application.targetFrameRate = 25;
		GameObject player = GameObject.Find ("Player");
		if (player != null) {
			Camera.main.transform.position = player.transform.position - new Vector3 (0, 0, 10);

			if (Input.GetKey (KeyCode.KeypadPlus)) {
				transform.eulerAngles += new Vector3 (0, 1, 0);
			}
		}
	}
}

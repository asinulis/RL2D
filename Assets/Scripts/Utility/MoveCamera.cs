using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Application.targetFrameRate = 25;
//		GameObject player = GameObject.Find ("Player");
//		Camera.main.transform.position = player.transform.position - new Vector3 (0, 0, 10);

		if (Input.GetKey (KeyCode.KeypadPlus)) {
			transform.eulerAngles += new Vector3 (0, 1, 0);
		}
	}
}

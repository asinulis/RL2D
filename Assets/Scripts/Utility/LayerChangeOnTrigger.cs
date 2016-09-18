using UnityEngine;
using System.Collections;

public class LayerChangeOnTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage ("enterTriggerState");
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage("leftTriggerState");
		}
	}
}

using UnityEngine;
using System.Collections;

public class LayerChangeOnTrigger : MonoBehaviour {

	public BoxCollider2D box;

	void Start()
	{
		box = GetComponent<BoxCollider2D> ();
	}
	void Update()
	{
		
	}
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

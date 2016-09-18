using UnityEngine;
using System.Collections;

public class LayerChangeOnTrigger : MonoBehaviour {
	private RLObject obj;

	void Start(){
		obj = GetComponentInParent<RLObject> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!other.isTrigger){
			RLObject otherObject = other.gameObject.GetComponentInParent<RLObject> ();
			if(otherObject != null)
				obj.addBehind (otherObject);
	//		if (other.gameObject.tag == "Player") {
	//			other.gameObject.SendMessage ("enterTriggerState");
	//		}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(!other.isTrigger){
			RLObject otherObject = other.gameObject.GetComponentInParent<RLObject> ();
			if(otherObject != null)
				obj.removeBehind (otherObject);
	//		if (other.gameObject.tag == "Player") {
	//			other.gameObject.SendMessage("leftTriggerState");
	//		}
		}
	}
}

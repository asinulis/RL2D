using UnityEngine;
using System.Collections;

public class LayerChangeOnTrigger : MonoBehaviour {
	private RLObject parentUnit;

	void Start(){
		parentUnit = GetComponentInParent<RLObject> ();
		if (parentUnit == null) {
			Debug.Log ("No RLObject attached to " + this.name + ". Removing script.");
			Destroy (this);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!other.isTrigger){
			RLObject otherObject = other.gameObject.GetComponentInParent<RLObject> ();
			if(otherObject != null)
				parentUnit.addBehind (otherObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(!other.isTrigger){
			RLObject otherObject = other.gameObject.GetComponentInParent<RLObject> ();
			if(otherObject != null)
				parentUnit.removeBehind (otherObject);
		}
	}
}

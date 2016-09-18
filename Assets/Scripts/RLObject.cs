using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class RLObject : MonoBehaviour {
	public List<RLObject> objectsBehind = new List<RLObject> ();
	public List<RLObject> objectsInFront = new List<RLObject> ();
	private SpriteRenderer renderer;
	public List<RLObject> waitList = new List<RLObject> ();

	// Use this for initialization
	void Start () {
		initialize ();
	}

	protected virtual void initialize(){
		renderer = GetComponent<SpriteRenderer> ();
	}

	public void addBehind(RLObject other){
		if (objectsInFront.Contains (other) && !waitList.Contains(other)) {
			waitList.Add (other);
		}
		if (!objectsBehind.Contains (other) && !objectsInFront.Contains (other)) {
			objectsBehind.Add (other);
			if (other.getLayerNumber () >= this.getLayerNumber ()) {
				setLayerNumber (other.getLayerNumber () + 1);
			}
			other.addInFront (this);
		}
	}

	public void removeBehind(RLObject other){
		if (waitList.Contains(other)){
			waitList.Remove (other);
		}
		if (objectsBehind.Contains (other)) {
			objectsBehind.Remove (other);
			updateLayerNumber ();
			other.removeInFront (this);
		}
	}

	internal void updateLayerNumber(){
		setLayerNumber (calculateLayer ());
	}

	private int calculateLayer() {
		if (objectsBehind.Count == 0) {
			return 0;
		}
		int max = 0;
		foreach (RLObject obj in objectsBehind) {
			if (obj.getLayerNumber () > max) {
				max = obj.getLayerNumber ();
			}
		}
		return max + 1;
	}

	private void setLayerNumber(int layer){
		renderer.sortingOrder = layer;
		foreach (RLObject obj in objectsInFront) {
			obj.updateLayerNumber ();
		}
	}

	internal int getLayerNumber(){
		return renderer.sortingOrder;
	}

	internal void addInFront(RLObject other){
		if (!objectsBehind.Contains(other) && !objectsInFront.Contains (other)) {
			objectsInFront.Add (other);
		}
	}

	internal void removeInFront(RLObject other) {
		if (objectsInFront.Contains (other)) {
			objectsInFront.Remove (other);
		}
		if (waitList.Contains (other)) {
			addBehind (other);
			waitList.Remove (other);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RLObject : MonoBehaviour {
	public List<RLObject> objectsBehind = new List<RLObject> ();
	public List<RLObject> objectsBefore = new List<RLObject> ();
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
//		initializeObject ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	protected abstract void initializeObject ();

	public void addBehind(RLObject other){
		if (!objectsBehind.Contains (other)) {
			objectsBehind.Add (other);
			if (other.getLayerNumber () >= this.getLayerNumber ()) {
				int oldLayer = this.getLayerNumber ();
				setLayerNumber (other.getLayerNumber () + 1);
				updateLayersBefore (oldLayer);
			}
			other.addBefore (this);
		}
	}

	public void removeBehind(RLObject other){
		if (objectsBehind.Contains (other)) {
			objectsBehind.Remove (other);
			if (other.getLayerNumber () == this.getLayerNumber () - 1) {
				int oldLayer = this.getLayerNumber ();
				setLayerNumber (calculateLayer ());
				updateLayersBefore (oldLayer);
			}
			other.removeBefore (this);
		}
	}

	private void updateLayersBefore(int oldLayer){
		foreach (RLObject obj in objectsBefore) {
			updateLayerNumber (oldLayer);
		}
	}

	internal void updateLayerNumber(int oldLayer){
		if (oldLayer == this.getLayerNumber () - 1) {
			setLayerNumber (calculateLayer ());
		}
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
	}

	internal int getLayerNumber(){
		return renderer.sortingOrder;
	}

	internal void addBefore(RLObject before){
		if (!objectsBefore.Contains (before)) {
			objectsBefore.Add (before);
		}
	}

	internal void removeBefore(RLObject before) {
		if (objectsBefore.Contains (before)) {
			objectsBefore.Remove (before);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Abstract class, encapsulating the derived classes Unit (i.e. Player, Enemy, Companion/Orbital), NPC (e.g. shopkeeper), hitables (e.g. bombs, traps) and environment.
 * Only has an associated SpriteRenderer. Handles the dynamic layer ordering.
*/

public abstract class RLObject : MonoBehaviour 
{
	protected List<RLObject> objectsBehind = new List<RLObject> ();
	protected List<RLObject> objectsInFront = new List<RLObject> ();
	protected SpriteRenderer sprRenderer;
	protected List<RLObject> waitList = new List<RLObject> ();

	public virtual void Start () {
		try{
			sprRenderer = GetComponent<SpriteRenderer> ();
		}
		catch (System.NullReferenceException) {
			throw new InitializationException ("Sprite Renderer in RLObject " + gameObject.name + " could not be initialized.");
		}
	}

	public void Update()
	{
		float miny = Mathf.Infinity;
		SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> (false);
		foreach (SpriteRenderer sprR in spr) {
			miny = Mathf.Min (miny, (int)Camera.main.WorldToScreenPoint (sprR.bounds.min).y);
		}
<<<<<<< HEAD
		foreach (SpriteRenderer sprR in spr) {
			sprR.sortingOrder = (int)miny * -1;
		}
	}

	public virtual void OnTriggerEnter2D(Collider2D other)
	{/*
		if(!other.isTrigger){
			RLObject otherObject = other.gameObject.GetComponentInParent<RLObject> ();
			if(otherObject != null)
				addBehind (otherObject);
=======
		if (!objectsBehind.Contains (other) && !hasInFront(other)) {
			objectsBehind.Add (other);
			if (other.getLayerNumber () >= this.getLayerNumber ()) {
				setLayerNumber (other.getLayerNumber () + 1);
			}
			other.addInFront (this);
		}
	}

	internal bool hasInFront(RLObject obj){
		if (objectsInFront.Contains (obj)) {
			return true;
		}
		foreach (RLObject obj1 in objectsInFront){
			if (obj1.hasInFront (obj)) {
				return true;
			}
		}
		return false;
	}

	public void removeBehind(RLObject other){
		if (waitList.Contains(other)){
			waitList.Remove (other);
>>>>>>> origin/master
		}
		*/
	}

	public virtual void OnTriggerExit2D(Collider2D other)
	{
		/*
		if(!other.isTrigger){
			RLObject otherObject = other.gameObject.GetComponentInParent<RLObject> ();
			if(otherObject != null)
				removeFromLists (otherObject);
		}
		*/
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
		SpriteRenderer[] allSprites = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sprite in allSprites) {
			sprite.sortingOrder = layer;
			foreach (RLObject obj in objectsInFront) {
				obj.updateLayerNumber ();
			}
		}
	}

	internal int getLayerNumber(){
		if (sprRenderer == null)
			return 0;
		return sprRenderer.sortingOrder;
	}


	public void addBehind(RLObject other){
		if(GameMaster.logMessages) GameMaster.LogMsg(" Adding " + other.gameObject.name + " behind " + this.gameObject.name, "addBehind");
		if (objectsInFront.Contains (other) && !waitList.Contains(other)) {
			waitList.Add (other);
		}
		if (!objectsBehind.Contains (other) && !hasInFront(other)) {
			objectsBehind.Add (other);
			if (other.getLayerNumber () >= this.getLayerNumber ()) {
				setLayerNumber (other.getLayerNumber () + 1);
			}
			other.addInFront (this);
		}
	}

	internal bool hasInFront(RLObject obj){
		if (objectsInFront.Contains (obj))
			return true;
		foreach (RLObject obj1 in objectsInFront) {
			if (obj1.hasInFront (obj))
				return true;
		}
		return false;

	}

	public void removeFromLists(RLObject other){
		if (objectsInFront.Contains (other)) {
			objectsInFront.Remove (other);
		}
		else if(objectsBehind.Contains (other)) {
			if(GameMaster.logMessages) GameMaster.LogMsg("Removing " + other.gameObject.name + " from behind " + this.gameObject.name, "removeBehind");
			objectsBehind.Remove (other);
			updateLayerNumber ();
			other.removeFromLists (this);
		}
		else if (waitList.Contains(other)){
			waitList.Remove (other);
		}
	}

	internal void addInFront(RLObject other){
		if (!objectsBehind.Contains(other) && !objectsInFront.Contains (other)) {
			objectsInFront.Add (other);
		}
	}
}
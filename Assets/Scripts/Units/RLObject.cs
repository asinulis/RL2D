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
		foreach (SpriteRenderer sprR in spr) {
			sprR.sortingOrder = (int)miny * -1;
		}
	}
}
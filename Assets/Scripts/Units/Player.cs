using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * A correctly set up player should have the following characteristics:
 * Rigidbody 2D, Animator, BoxCollider2D (feet), Player Script
 * 
 * The children are structured as follows:
 * Player
 * |- Gun
 * 	  |- Barrel
 * |- Head
 * |- Trigger
 * 
 * The gun should hold the Weapon script and a sprite renderer, the barrel can be an empty object. Every trigger needs to have the 
 * LayerChangeOnTrigger script attached to it. The head just needs a sprite renderer.
*/

public class Player : Unit {
	public KeyMap keymap { get; private set; }
	public enum Attribute {FLYING, INVISIBLE, ON_FIRE, };							// TODO: add attributes
	public float nextShot { get; private set; }
	public Dictionary<Attribute, bool> dict = new Dictionary<Attribute, bool>();	// TODO: set up the dictionary and its usage
	public GameObject head { get; private set; }
	public GameObject body { get ; private set; }

    public override void Start()
	{
		try{
		base.Start ();
			stats.damage = 300;
		dict.Add (Attribute.FLYING, false);
		dict.Add (Attribute.ON_FIRE, false);
		dict.Add (Attribute.INVISIBLE, false);
		keymap = new KeyMap ();
		nextShot = Time.time;
		head = transform.FindChild("Head").gameObject;
		body = transform.FindChild("Body").gameObject;
		sprRenderer = body.GetComponent<SpriteRenderer>();
		if(animator == null) animator = body.GetComponent<Animator>();
		}
		catch (System.NullReferenceException){
			if (gameObject.GetComponent<Player> () == null)
				throw new InitializationException ("Player: Can't find player script.");
			else if (head == null)
				throw new InitializationException ("Player: Can't find child named Head.");
			else if (body == null)
				throw new InitializationException ("Player: Can't find child named Body.");
		}
    }

	public void moveBy(Vector3 vec) {
		trans.Translate (vec);
	}

	public void setShotTime(float time){
		nextShot = time;
	}
}
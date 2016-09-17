using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerBehaviour : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb;
	Transform transform;
	UnitStats stats;
	Weapon mainWeapon;
	public Text UIText;
	int noOfTriggers = 0;
	float nextShot;

    void Start()
    {
		animator = GetComponent<Animator>();
		stats = new UnitStats (this.gameObject, new List<AbstractEffect>{new SprintEffect()});
		transform = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
		mainWeapon = new Weapon (this.gameObject, stats.damage);
		nextShot = Time.time;
		checkForComponents ();
		setPlayerStats (stats);
		displayPlayerStats ();
    }

    void FixedUpdate()
    {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			stats.triggerEffect ();
		} else {
			stats.untriggerEffect ();
		}

		if (Input.GetButtonDown ("Fire1") && Time.time > nextShot) {
			nextShot = Time.time + (1 / stats.attack_rate);
			mainWeapon.Shoot ();
		}

		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		Vector3 moveBy = new Vector3 (Math.Sign (horizontal), Math.Sign (vertical), 0f); // Math.Sign(vertical));
		moveBy *= Time.deltaTime * stats.speed;
		transform.Translate (moveBy);

		if (vertical == 0 && horizontal == 0) {
			animator.speed = 0;
		} else {
			animator.speed = stats.speed / UnitStats.BASE_SPEED;
			if (vertical > 0) {
				animator.Play ("PlayerMovementNorth");
			} else if (vertical < 0) {
				animator.Play ("PlayerMovementSouth");
			} else if (horizontal > 0) {
				animator.Play ("PlayerMovementEast");
			} else if (horizontal < 0) {
				animator.Play ("PlayerMovementWest");
			}
		}
		//print ("Moving by " + moveBy.x.ToString () + "/" + moveBy.y.ToString () + "/" + moveBy.z.ToString ()+". DeltaTime: " + Time.deltaTime.ToString());
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		//transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
		if (other.gameObject.tag == "Enemy") {
			stats.hp -= 10;
			displayPlayerStats ();
		}
	}

	public void setPlayerStats(UnitStats stats){
		this.stats = stats;
	}

	public void displayPlayerStats(){
		UIText.text = "HP: " + stats.hp.ToString () + "\nRunes: " + "0/0/0/0";
	}

	public void leftTriggerState(){
		noOfTriggers--;
		if (noOfTriggers == 0) {
			//Debug.Log ("You have left the trigger zone, changing back layer.");
			GetComponent<SpriteRenderer> ().sortingOrder = 1;
		}
	}

	public void enterTriggerState(){
		noOfTriggers++;
		//Debug.Log ("You have walked into the trigger, changing layer temporarily. Layers: " + noOfTriggers.ToString());
		GetComponent<SpriteRenderer> ().sortingOrder = -10;
	}

	void checkForComponents()
	{
		if (animator == null) {
			Debug.LogError ("Missing Animator component during PlayerBehaviour initialization");
		}
		if (stats == null) {
			Debug.LogError ("Missing UnitStats component during PlayerBehaviour initialization");
		}
		if (UIText == null) {
			Debug.LogError ("Missing UIText component during PlayerBehaviour initialization");
		}
		if (transform == null) {
			Debug.LogError ("Missing Transform component during PlayerBehaviour initialization");
		}
		if (rb == null) {
			Debug.LogError ("Missing RigidBody2D component during PlayerBehaviour initialization");
		}
		if (mainWeapon == null) {
			Debug.LogError ("Missing Weapon component during PlayerBehaviour initialization");
		}
	}
}
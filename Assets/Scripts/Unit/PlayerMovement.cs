using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerMovement : MonoBehaviour {

    Animator animator;
    //Rigidbody2D rb;
	Transform transform;
	UnitStats stats;
	public Text UIText;

    void Start()
    {
		animator = GetComponent<Animator>();
		stats = new UnitStats (this.gameObject, new List<AbstractEffect>{new SprintEffect()});
		setPlayerStats (stats);
        //rb = GetComponent<Rigidbody2D>();
		transform = GetComponent<Transform> ();
		displayPlayerStats ();
    }

    void FixedUpdate()
    {
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			stats.triggerEffect ();
		} else {
			stats.untriggerEffect ();
		}

		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		Vector3 moveBy = new Vector3 (Math.Sign (horizontal), Math.Sign (vertical), Math.Sign(vertical));
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
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
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
}
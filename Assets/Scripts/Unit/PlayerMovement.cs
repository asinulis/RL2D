using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {

    Animator animator;
	Camera camera;
    Rigidbody2D rb;
	Transform transform;
	UnitStats stats;
	public Text UIText;

    void Start()
    {
		animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
		transform = GetComponent<Transform> ();
		camera = Camera.main;
		displayPlayerStats ();
    }

    void FixedUpdate()
    {
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		Vector3 moveBy = new Vector3 (Math.Sign (horizontal), Math.Sign (vertical), Math.Sign(vertical));
		moveBy *= Time.deltaTime * stats.Speed;
		transform.Translate (moveBy);

		if (vertical == 0 && horizontal == 0) {
			animator.speed = 0;
		} else {
			animator.speed = stats.Speed / UnitStats.BASE_SPEED;
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
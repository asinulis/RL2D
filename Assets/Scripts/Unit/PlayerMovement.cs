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

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
		transform = GetComponent<Transform> ();
		camera = Camera.main;
		UIText.text = "HP: " + stats.hp.ToString ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		transform.Translate (stats.Speed * (Math.Sign(vertical) * Vector2.up + Math.Sign(horizontal) * Vector2.right));

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
		Vector3 pos = new Vector3 (transform.position.x, transform.position.y, transform.position.y);//Mathf.Min(transform.position.y,0));
		transform.position = pos;
		pos.z = -10;
		camera.transform.position = pos;
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		print ("Collision");
		if (other.gameObject.tag == "Enemy") {
			stats.hp -= 10;
			displayPlayerStats ();
		}
	}

	public void setPlayerStats(UnitStats stats){
		this.stats = stats;
	}

	public void displayPlayerStats(){
		UIText.text = "HP: " + stats.hp.ToString ();
	}
}
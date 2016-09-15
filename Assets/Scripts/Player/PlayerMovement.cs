using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb;
	Transform transform;
	PlayerStats stats;
    const int DIRECTION_SOUTH = 1;
    const int DIRECTION_EAST = 2;
    const int DIRECTION_WEST = 3;
    const int DIRECTION_NORTH = 0;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
		transform = GetComponent<Transform> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (transform.position.y > 0.75) {
			GetComponent<SpriteRenderer> ().sortingOrder = 0;
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = 2;
		}
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		transform.Translate (stats.Speed * (Math.Sign(vertical) * Vector2.up + Math.Sign(horizontal) * Vector2.right));

		if (vertical == 0 && horizontal == 0) {
			animator.speed = 0;
		} else {
			animator.speed = stats.Speed / PlayerStats.BASE_SPEED;
		}
        if (vertical > 0) {
            animator.SetInteger("Direction", DIRECTION_NORTH);
		} else if (vertical < 0) {
			animator.SetInteger("Direction", DIRECTION_SOUTH);
		} else if (horizontal > 0) {
			animator.SetInteger("Direction", DIRECTION_EAST);
        } else if (horizontal < 0) {
			animator.SetInteger ("Direction", DIRECTION_WEST);
		}
    }

	public void setPlayerStats(PlayerStats stats){
		this.stats = stats;
	}
}
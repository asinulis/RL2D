using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {

    Animator animator;
	Camera camera;
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
		camera = Camera.main;
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
			animator.speed = stats.Speed / PlayerStats.BASE_SPEED;
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
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, Mathf.Min(transform.position.y,0));
		transform.position = pos;
		pos.z = -10;
		camera.transform.position = pos;
    }

	public void setPlayerStats(PlayerStats stats){
		this.stats = stats;
	}
}
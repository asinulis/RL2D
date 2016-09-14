using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb;
	Transform transform;
	PlayerStats stats;
	float speed;
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
    void Update()
    {
		if (transform.position.y > 0.75) {
			GetComponent<SpriteRenderer> ().sortingOrder = 0;
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = 2;
		}
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
        if (vertical > 0) {
            animator.SetInteger("Direction", DIRECTION_NORTH);
			rb.MovePosition(rb.position + stats.Speed*Vector2.up);
			animator.speed = stats.Speed / PlayerStats.BASE_SPEED;
        } else if (vertical < 0) {
			animator.SetInteger("Direction", DIRECTION_SOUTH);
			rb.MovePosition(rb.position + stats.Speed*Vector2.down);
			animator.speed = stats.Speed / PlayerStats.BASE_SPEED;
		} else if (horizontal > 0) {
			animator.SetInteger("Direction", DIRECTION_EAST);
			rb.MovePosition(rb.position + stats.Speed*Vector2.right);
			animator.speed = stats.Speed / PlayerStats.BASE_SPEED;
        } else if (horizontal < 0) {
			animator.SetInteger ("Direction", DIRECTION_WEST);
			rb.MovePosition (rb.position + stats.Speed*Vector2.left);
			animator.speed = stats.Speed / PlayerStats.BASE_SPEED;
		} else {
			animator.speed = 0;
		}
    }

	public void setPlayerStats(PlayerStats stats){
		this.stats = stats;
	}
}
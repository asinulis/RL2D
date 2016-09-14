using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb;
	private Transform transform;
	private float speed;
    private const int DIRECTION_SOUTH = 1;
    private const int DIRECTION_EAST = 2;
    private const int DIRECTION_WEST = 3;
    private const int DIRECTION_NORTH = 0;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
		transform = GetComponent<Transform> ();
		speed = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
		if (gameObject.transform.position.y > 0.75) {
			GetComponent<SpriteRenderer> ().sortingOrder = 0;
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = 2;
		}
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
        if (vertical > 0) {
            animator.SetInteger("Direction", DIRECTION_NORTH);
			rb.MovePosition(rb.position + speed*Vector2.up);
			animator.speed = 1;
        } else if (vertical < 0) {
			animator.SetInteger("Direction", DIRECTION_SOUTH);
			rb.MovePosition(rb.position + speed*Vector2.down);
			animator.speed = 1;
		} else if (horizontal > 0) {
			animator.SetInteger("Direction", DIRECTION_EAST);
			rb.MovePosition(rb.position + speed*Vector2.right);
			animator.speed = 1;
        } else if (horizontal < 0) {
			animator.SetInteger ("Direction", DIRECTION_WEST);
			rb.MovePosition (rb.position + speed*Vector2.left);
			animator.speed = 1;
		} else {
			animator.speed = 0;
		}
    }
}
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb;
    private int DIRECTION_SOUTH = 1;
    private int DIRECTION_EAST = 2;
    private int DIRECTION_WEST = 3;
    private int DIRECTION_NORTH = 0;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0)
        {
            animator.SetInteger("Direction", DIRECTION_NORTH);
            Vector2 up = new Vector2(0f,0.01f);
			rb.MovePosition(rb.GetRelativePoint(new Vector2(0,0))+up);
        }
        if (vertical < 0)
        {
            animator.SetInteger("Direction", DIRECTION_SOUTH);
			Vector2 down = new Vector2(0f,-0.01f);
			rb.MovePosition(rb.GetRelativePoint(new Vector2(0,0))+down);
		}
        if (horizontal > 0)
        {
            animator.SetInteger("Direction", DIRECTION_EAST);
			Vector2 right = new Vector2(0.01f,0f);
			rb.MovePosition(rb.GetRelativePoint(new Vector2(0,0))+right);
        }
        if (horizontal < 0)
        {
            animator.SetInteger("Direction", DIRECTION_WEST);
			Vector2 left = new Vector2(-0.01f,0f);
			rb.MovePosition(rb.GetRelativePoint(new Vector2(0,0))+left);
        }
    }
}
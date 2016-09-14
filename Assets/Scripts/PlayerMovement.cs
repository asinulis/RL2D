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
            Vector2 up = new Vector2(0,1);
            rb.AddForce(up);
        }
        else if (vertical < 0)
        {
            animator.SetInteger("Direction", DIRECTION_SOUTH);
            Vector2 up = new Vector2(0, -1);
            rb.AddForce(up);
        }
        else if (horizontal > 0)
        {
            animator.SetInteger("Direction", DIRECTION_EAST);
            Vector2 up = new Vector2(1, 0);
            rb.AddForce(up);
        }
        else if (horizontal < 0)
        {
            animator.SetInteger("Direction", DIRECTION_WEST);
            Vector2 up = new Vector2(-1, 0);
            rb.AddForce(up);
        }
    }
}
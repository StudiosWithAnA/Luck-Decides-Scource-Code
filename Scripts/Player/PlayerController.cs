using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask wutIsGround;

    private int Jumps;
    public int maxJumps;

    private Rigidbody2D rb;

    public int MessUp = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jumps = maxJumps;
    }

    private void Update()
    {
        if(isGrounded)
        {
            Jumps = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            Jumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && Jumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed * MessUp, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, wutIsGround);
    }

    public void KnockBack(float knock, Vector2 dir, float dis)
    {
        rb.MovePosition(rb.position + dir * dis);
    }
}

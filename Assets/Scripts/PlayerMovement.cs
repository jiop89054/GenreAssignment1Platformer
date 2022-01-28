using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //gets rigidbody
    public Rigidbody2D rb;
    //basic movement stuff
    public float speed = 10.0f;
    public Vector2 movement;
    public Vector2 velocity;
    public float maxspeed;
    public float jumpForce;
    public Vector2 speedCompensation;
    //stuff for detecting the ground
    public bool isGrounded;
    public Transform groundCheck;
    public Transform groundCheck2;
    public float checkRadius;
    public LayerMask whatIsGround;
    //stuff for double jumps
    public int extraJumps;
    public int extraJumpsValue;
    //stuff for wallsliding
    bool isTouchingFront;
    public Transform frontCheck;
    public Transform frontCheck2;
    public bool wallSliding;
    public float wallSlidingSpeed;
    //WallJUMPING stuff
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    //Makes the animations work
    Animator animator;
    //Dash system
    public bool isDashing;
    public float maxDashes;
    public float DashesRemaining;
    public float DashPower;
    public float DashTime;
    public bool dashRight = true;
    public bool isDashingRight;
    public bool isDashingLeft;
    // Use this for initialization
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontalInput, 0);
        //the point of this is just to measure speed for testing
        velocity = rb.velocity;

        //Activates animations
        //running
        animator.SetBool("Run", horizontalInput != 0);
        //jumping and falling
        if(rb.velocity.y < 0.01 && rb.velocity.y > -0.01)
        {
            animator.SetInteger("AirState", 0);
        }
        if(rb.velocity.y > 0.01)
        {
            animator.SetInteger("AirState", 1);
        }
        if(rb.velocity.y < -0.01 && !wallJumping)
        {
            animator.SetInteger("AirState", -1);
        }
        //wallsliding
        if (wallSliding)
        {
            animator.SetBool("IsWallSliding", true);
        }   else
        {
            animator.SetBool("IsWallSliding", false);
        }
        //Dash Animation
        animator.SetBool("Dashing", isDashing);

        //If statement that flips character
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(1, 1);
            dashRight = true;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
            dashRight = false;
        }
        //segment for jumping and double jumping
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            DashesRemaining = maxDashes;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && wallSliding == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //for dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && DashesRemaining > 0)
        {
            if(dashRight == true)
            {
                isDashingRight = true;
            }
            else if(dashRight == false)
            {
                isDashingLeft = true;
            }
            Invoke("DeactivateDash", DashTime);
            DashesRemaining--;
        }
        if(isDashingRight == true)
        {
            rb.velocity = new Vector2(DashPower, 0);
        }
        if(isDashingLeft == true)
        {
            rb.velocity = new Vector2(-DashPower, 0);
        }
        isDashing = isDashingRight || isDashingLeft;
        //this is for wall sliding
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(frontCheck2.position, checkRadius, whatIsGround);
        if (isTouchingFront == true && isGrounded == false && horizontalInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }
        if (wallSliding)
        {
            extraJumps = extraJumpsValue;
            DashesRemaining = maxDashes;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        //walljumping
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);

        }
        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -horizontalInput, yWallForce);
        }
    }

    void FixedUpdate()
    {
        //simply runs the moveCharacter function
        moveCharacter(movement);
        //this exists to create a max speed for our player
        if (rb.velocity.x > maxspeed && isDashingRight == false)
        {
            rb.velocity = new Vector2(maxspeed, rb.velocity.y);
        }
        if (rb.velocity.x < -maxspeed && isDashingLeft == false)
        {
            rb.velocity = new Vector2(-maxspeed, rb.velocity.y);
        }
        //this stops the player from slipping around all the time
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (rb.velocity.x > .25)
            {
                speedCompensation = new Vector2(speed / 2, 0);
                rb.AddForce(-speedCompensation);
            }
            if (rb.velocity.x < -.25)
            {
                speedCompensation = new Vector2(speed / 2, 0);
                rb.AddForce(speedCompensation);
            }
            if (rb.velocity.x > -.25 && rb.velocity.x < .25)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        //sets isGrounded for all the jumping related things
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck2.position, checkRadius, whatIsGround);
    }
    void moveCharacter(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    void DeactivateDash()
    {
        isDashingRight = false;
        isDashingLeft = false;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNewMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [Header("Movement")]
    public float speed = 8f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    private float horizontal;
    private bool isFacingRight = true;

    [Header("Jump")]
    public float jumpingPower = 16f;
    public float doubleJumpingPower = 14f;
    private bool doubleJump;

    [Header("Better Jump")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Coyote Time")]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Jump Buffer")]
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Jump")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer;
    public Vector2 wallJumpingPower = new Vector2(10f, 18f);
    public float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private bool isWallJumping;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;
    private bool canDash = true;
    private bool isDashing;
    private float dashTime;

    [Header("Multiple Jumps")]
    public int extraJumps = 1; // How many additional jumps after the first
    private int jumpsLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Reset double jump when grounded
        if (IsGrounded() && rb.velocity.y <= 0.1f)
        {
            jumpsLeft = extraJumps;
        }

        // Timers
        coyoteTimeCounter = IsGrounded() ? coyoteTime : coyoteTimeCounter - Time.deltaTime;
        jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpBufferCounter = 0f;
                coyoteTimeCounter = 0f;
            }
            else if (jumpsLeft > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpingPower);
                jumpsLeft--;
                jumpBufferCounter = 0f;
            }
        }
        // Wall jumping
        if (IsTouchingWall() && !IsGrounded())
        {
            wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        // Better jump physics
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        Flip();

        // Animations
        anim.SetBool("isJumping", !IsGrounded());
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    private void FixedUpdate()
    {
        if (isDashing || isWallJumping) return;

        float targetSpeed = horizontal * speed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        float movement = speedDiff * accelRate;

        rb.AddForce(new Vector2(movement, 0f));
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;

            // Wall jump condition
            if (IsTouchingWall() && !IsGrounded())
            {
                isWallJumping = true;
                float wallDir = isFacingRight ? -1 : 1; // push away from wall
                rb.velocity = new Vector2(wallDir * wallJumpingPower.x, wallJumpingPower.y);

                jumpsLeft = extraJumps; // Reset extra jumps
                coyoteTimeCounter = 0f;

                Invoke(nameof(StopWallJumping), 0.3f);
            }
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && !isDashing)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;
        dashTime = dashDuration;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2((isFacingRight ? 1 : -1) * dashSpeed, 0f);

        while (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private bool IsTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, wallCheckDistance, wallLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }
}
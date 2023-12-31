using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBetterTest : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Animator anim;

    Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float jumpDelay;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip jumpLandClip;
    AudioSource playerSource;
    float timeLastGrounded;
    bool hasCheckedGrounded;
    bool hasJumped;

    Vector3 startPosition;

    GameManager gameManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        startPosition = transform.position;
        playerSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(gameManager.gameState == GameManager.GameState.PLAY_MODE)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal == 0)
            {
                anim.SetBool("IsRunning", false);
            }
            else
            {
                anim.SetBool("IsRunning", true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || Time.time - timeLastGrounded < jumpDelay) && !hasJumped)
            {
                playerSource.clip = jumpClip;
                playerSource.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                hasJumped = true;
            }

            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            WallSlide();
            WallJump();

            if (!isWallJumping)
            {
                Flip();
            }

            if(!IsGrounded() && !hasCheckedGrounded)
            {
                timeLastGrounded = Time.time;
                hasCheckedGrounded = true;
            }
            else if (IsGrounded() && hasCheckedGrounded)
            {
                playerSource.clip = jumpLandClip;
                playerSource.Play();
                hasCheckedGrounded = false;
                hasJumped = false;
            }

        }
        
    }

    private void FixedUpdate()
    {
        if (gameManager.gameState == GameManager.GameState.PLAY_MODE)
        {
            if (!isWallJumping)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void ResetStartPosition()
    {
        transform.position = startPosition;
    }
}

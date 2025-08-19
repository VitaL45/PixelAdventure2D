using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private bool jumping;
    private float myGravityScale = 7f;

    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpPower = 8f;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; //How much time the player can hang in the air before jumping
    private float coyoteCounter; //How much time has passes since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player
        if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0.01)
        {
            transform.localScale = Vector3.one;
        }

        //Set animator parameters
        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded());
         
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);

        if (onWall())
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
        else 
        {
            rb.gravityScale = myGravityScale;
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on ground
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter is bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                else
                {
                    if(jumpCounter > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }

            }
            //Reset coyote counter to 0 to avoid double jumps
            coyoteCounter = 0;
        }
    }
    private void WallJump()
    {
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.01f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return true;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpSpeed = 8f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //Flip player
        if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0.01)
        {
            transform.localScale = Vector3.one;
        }

        //Jump 
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        //Set animator parameters
        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        anim.SetTrigger("jump");
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}

using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movementDistance;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
                movingLeft = false;
        }
        else 
        {
            if (transform.position.x < rightEdge)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
                movingLeft = true;
        }
    }

    private void moveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void moveRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}

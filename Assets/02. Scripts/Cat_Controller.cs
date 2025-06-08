using Unity.VisualScripting;
using UnityEngine;

public class Cat_Controller : MonoBehaviour
{
    private float h;
    private int jumpCount = 0;
    public bool isGround = false;

    Animator catAnim;
    Rigidbody2D catRb;

    public float moveSpeed;

    public float jumpPower = 5f;


    void Start()
    {
        catRb = GetComponent<Rigidbody2D>();
        catAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");

        Vector3 moveTo = new Vector3(h, 0, 0);

        transform.position += moveTo * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            catAnim.SetTrigger("Jump");

            catRb.AddForceY(jumpPower, ForceMode2D.Impulse);            
            jumpCount++;
            isGround = false;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                jumpCount = 0;
                isGround = true;
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGround = false;
            }
        }
    }
}

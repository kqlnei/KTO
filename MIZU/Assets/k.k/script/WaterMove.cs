using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 5f; // ÉWÉÉÉìÉvÇÃçÇÇ≥
    private Rigidbody rb;
    private bool isGrounded = true;
    private ChararCh chararCh; // Reference to the ChararCh script

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chararCh = GetComponentInParent<ChararCh>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float move = 0;

        if (chararCh.Mode) // Player 1
        {
            if (Input.GetKey(KeyCode.D))
            {
                move = moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                move = -moveSpeed * Time.deltaTime;
            }
            transform.Translate(move, 0, 0);
        }
        else // Player 2
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                move = moveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                move = -moveSpeed * Time.deltaTime;
            }
            transform.Translate(move, 0, 0);
        }
    }

    void Jump()
    {
        if (chararCh.Mode) // Player 1
        {
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, CalculateJumpSpeed(jumpHeight), rb.velocity.z);
                isGrounded = false;
            }
        }
        else // Player 2
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, CalculateJumpSpeed(jumpHeight), rb.velocity.z);
                isGrounded = false;
            }
        }
    }

    private float CalculateJumpSpeed(float jumpHeight)
    {
        return Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

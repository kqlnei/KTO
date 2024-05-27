using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private ChararCh chararCh; // Reference to the ChararCh script
    private RiseGas riseGas;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Get the ChararCh component from the parent object
        chararCh = GetComponentInParent<ChararCh>();
        riseGas = GetComponentInParent<RiseGas>();

    }

    // Update is called once per frame
    void Update()
    {
        
            Move();
        
        
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform feetPos; //where do we generate a physics detector to find what is below us?
    [SerializeField] private LayerMask groundLayer; // what layer of objects counts as the ground?
    [SerializeField] private float jumpTime = 0.3f;

    private bool onGround;
    private bool isJumping;
    private float jumpTimer;


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics2D.OverlapCircle(feetPos.position, 0.25f, groundLayer) == true) // creates a overlap circle at (variable.position, radius, layer)
        {
            onGround = true;
        }

        else
        {
            onGround = false;
        }


        if (Input.GetButton("Jump") && onGround == true)
        {
            playerRB.velocity = Vector2.up * jumpForce;
            
            isJumping = true;
        }

        if (isJumping == true && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                playerRB.velocity = Vector2.up * jumpForce;
                
                jumpTimer += Time.deltaTime;
            }

            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;  
            jumpTimer = 0;
        }
    }
}

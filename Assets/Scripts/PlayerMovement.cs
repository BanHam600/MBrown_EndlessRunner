using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpTime = 0.3f;

    [SerializeField] private Animator animator;
    
    [SerializeField] private Transform feetPos; //where do we generate a physics detector to find what is below us?
    [SerializeField] private LayerMask groundLayer; // what layer of objects counts as the ground?
    

    private bool onGround;
    private bool isJumping;
    private float jumpTimer;
    
    private bool isFalling;
    private float fallTime;
    private bool wasGrounded;


    void OnDrawGizmosSelected()
    {
        if (feetPos != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(feetPos.position, 0.25f);
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("vSpeed", playerRB.velocity.y);

        onGround = Physics2D.OverlapCircle(feetPos.position, 0.25f, groundLayer);

        animator.SetBool("isGrounded", onGround);

        Debug.Log("[GroundCheck] onGround ={onGround}");
       


        if (onGround && Input.GetButtonDown("Jump"))
        {
            playerRB.velocity = Vector2.up * jumpForce;
            
            isJumping = true;

            jumpTimer = 0;

            Debug.Log(" Jump button DOWN");


        }

        if (isJumping == true && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                playerRB.velocity = Vector2.up * jumpForce;
                
                jumpTimer += Time.deltaTime;

                Debug.Log(" Applying jump impulse");
            }

            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;  
        }

        if (Input.GetKeyDown("s"))
        {
            GameManager.Instance.PauseObstacles();
        }

        if (Input.GetKeyDown("g"))
        {
            GameManager.Instance.ResumeObstacles();
        }

        // fell without jumping
        if(wasGrounded && !onGround &&  !isJumping)
        {
            fallTime = 0;
        }

        //if in the air and falling, start timer
        if(!onGround && playerRB.velocity.y < 0f)
        {
            fallTime += Time.deltaTime;

            if(fallTime >= 5f)
            {
                GameManager.Instance.GameOver();
            }
        }

        //landed
        if(!wasGrounded && onGround)
        {
            fallTime = 0f;
            Debug.Log($"You fell for  {fallTime:f2} seconds.");
        }

        wasGrounded = onGround;
    }
}

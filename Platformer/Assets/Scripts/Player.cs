using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player: MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Animator animator;
    

    public float moveSpeed = 100;
    public float jumpHeight = 0;
    private bool isJumping = false;

    public void Jump()
     {
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
        myRigidBody.velocity =  new Vector3(0, jumpHeight, 0);
    }
    public void Move(float horizontalInput)
    {
        //get the Input from Horizontal axis
        Debug.Log("move");
        
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //animator.SetBool("left", true);
        }
        else if(horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput * moveSpeed));
        //update the position
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 slideDirection = Vector3.Reflect(myRigidBody.velocity, collision.contacts[0].normal);
            ApplySlideEffect(slideDirection);
        }
    }
    public void ApplySlideEffect(Vector3 slideDirection)
    {
        transform.position += slideDirection * moveSpeed * Time.deltaTime;
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isJumpingInput = Input.GetKeyDown("space");
        if (horizontalInput != 0)
        {
            Move(horizontalInput);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        if (isJumpingInput && !isJumping)
        {
            Jump();
        }
    }
}

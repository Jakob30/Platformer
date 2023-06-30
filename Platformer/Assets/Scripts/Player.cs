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
    private float actMoveSpeed;
    public void Jump()
     {
        isJumping = true;
        animator.SetBool("isOnGround", isJumping);
        myRigidBody.velocity =  new Vector3(0, jumpHeight, 0);
    }
    public void Move(float horizontalInput)
    {
        //get the Input from Horizontal axis
        Debug.Log("move");
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        if (horizontalInput < 0)
        {
            MirrorAlongXAxis();
        }
        animator.SetFloat("Speed", horizontalInput * moveSpeed * Time.deltaTime);
        actMoveSpeed = Mathf.Abs(horizontalInput * moveSpeed * Time.deltaTime);
        //update the position
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
            if (actMoveSpeed > 0)
                animator.SetBool("is!JumpingandRunning", true);
            else
                animator.SetBool("is!JumpingandRunning", false);
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
        if (isJumpingInput && !isJumping)
        {
            Jump();
        }
    }
    
    public void MirrorAlongXAxis()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}

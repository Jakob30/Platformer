using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Animator animator;

    public float raycast_distance = 0.01f;
    public float moveSpeed = 100;
    public float jumpHeight = 0;
    public float slide_force = 10;
  

    private bool isJumping = false;
    private bool wall_collide = false;

    private enum dir { left, right, nodir};
    dir wall = dir.nodir;
    public void Jump()
    {
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
        myRigidBody.velocity = new Vector3(0, jumpHeight, 0);
        Debug.Log("jump");
    }
    public void Move(float horizontalInput)
    {
        float movement_horizontal = horizontalInput * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(movement_horizontal, 0, 0));
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput * moveSpeed));
        
        //update the position
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.contacts[0].normal.normalized;
        Debug.Log(collisionNormal);
        float upDot = Vector2.Dot(collisionNormal, Vector2.up);
        float downDot = Vector2.Dot(collisionNormal, Vector2.down);
        float rightDot = Vector2.Dot(collisionNormal, Vector2.right);
        float leftDot = Vector2.Dot(collisionNormal, Vector2.left);

        if (upDot > 0.9f)
        {
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
            Debug.Log("Floor");
        }
    }


    public void ApplySlideEffect()
    {
        myRigidBody.velocity = new Vector2(0, -slide_force * myRigidBody.gravityScale);
        Debug.Log("Slide");
        isJumping = false;
        //myRigidBody.AddForce(Vector2.down * slide_force, ForceMode2D.Force);
        
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isJumpingInput = Input.GetKeyDown("space");
        dir dir_current = dir.nodir;


        if (horizontalInput < 0)
        {
            dir_current = dir.left;
            transform.localScale = new Vector3(-1, 1, 1);
            //animator.SetBool("left", true);
        }
        else if (horizontalInput > 0)
        {
            dir_current = dir.right; 
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (isJumpingInput && !isJumping)
        {
            Jump();
        }

        if (horizontalInput != 0)
        {
            if (check_collide() && !isJumpingInput)
            {
                if (dir_current == dir.right && is_colliding_with_wall(Vector2.right))
                {
                    ApplySlideEffect();
                    horizontalInput = 0;
                }
                else if (dir_current == dir.left && is_colliding_with_wall(Vector2.left))
                {
                    horizontalInput = 0;
                    ApplySlideEffect();
                }
            }

            Move(horizontalInput);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }


        //if (wall_collide)
        //{
           
        //}
    }
    private bool check_collide()
    {
        return is_colliding_with_wall(Vector2.left) || is_colliding_with_wall(Vector2.right);
    }
    private bool is_colliding_with_wall(Vector2 direction)
    {

        int character_layer = gameObject.layer;
        int layer_mask = ~(1 << character_layer);

        RaycastHit2D[] hit = new RaycastHit2D[1];

        Physics2D.RaycastNonAlloc(transform.position, direction, hit, raycast_distance, layer_mask);

        // Check if any of the raycasts hit a wall collider
        return hit[0].collider != null;
    }

}

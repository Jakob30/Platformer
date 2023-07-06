using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Animator animator;

    public float raycast_distance = 0.01f;
    public float moveSpeed = 100;
    public float jumpHeight = 0;
    public float slideSpeed = 10;

    private bool isJumping = false;
    private bool wall_collide = false;

    private enum dir { left, right, nodir};
    dir wall = dir.nodir;
    public void Jump()
    {
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
        myRigidBody.velocity = new Vector3(0, jumpHeight, 0);
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
        else if (rightDot > 0.9f || leftDot > 0.9f)
        {
            if (rightDot > 0.9f) { wall = dir.left; }
            else { wall = dir.right; }
            wall_collide = true;
            ApplySlideEffect();
            Debug.Log("Slide");
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Vector2 collisionNormal = collision.contacts[0].normal.normalized;
    //    float rightDot = Vector2.Dot(collisionNormal, Vector2.right);
    //    float leftDot = Vector2.Dot(collisionNormal, Vector2.left);
    //    if (rightDot > 0.9f || leftDot > 0.9f)
    //    {
    //        wall_collide = false;
    //    }
    //}

    public void ApplySlideEffect()
    {
        transform.position += new Vector3(0, slideSpeed * Time.deltaTime, 0);
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isJumpingInput = Input.GetKeyDown("space");
        dir dir_current = dir.nodir;
        Debug.Log(wall_collide);
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

        if(horizontalInput != 0)
        {
            if (wall_collide)
            {
                horizontalInput = 0;
                if (dir_current != wall)
                {
                    wall_collide = false;
                }
            }
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

        //if (wall_collide)
        //{
           
        //}
    }
    //private bool CheckWallCollision()
    //{

    //    int character_layer = gameObject.layer;
    //    int layer_mask = ~(1<<character_layer);

    //    RaycastHit2D[] hit_right = new RaycastHit2D[1];
    //    RaycastHit2D[] hit_left = new RaycastHit2D[1];
    //    // Cast rays to detect collision with walls
    //    // You can modify this code depending on your specific collider setup
    //     Physics2D.RaycastNonAlloc(transform.position, Vector2.left, hit_left, raycast_distance, layer_mask);
    //     Physics2D.RaycastNonAlloc(transform.position, Vector2.right, hit_right, raycast_distance, layer_mask);

    //    // Check if any of the raycasts hit a wall collider
    //    if (hit_left[0].collider != null || hit_right[0].collider != null)
    //    {
    //        return true; // Colliding with a wall
    //    }

    //    return false; // Not colliding with a wall
    //}
    
}

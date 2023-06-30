using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player: MonoBehaviour
{
    public GameObject pl1;
    public Rigidbody2D myRigidBody;
    public Animator animator;
    public Sprite monochrome_tilemap_packed_240;
    private float x = 0;
    private float y = 0;
    public float jumpHeight = 0;
    private bool isOnGround = true;
    public float moveSpeed = 100;
    public float gravity = 1;
    private Vector2 movementDirection = new Vector3(0,0,0);

    public void CreatePlayer(float Spawnx, float Spawny)
    {
        pl1 = GameObject.FindGameObjectWithTag("Player");
        x = Spawnx; y = Spawny;
        gameObject.GetComponent<SpriteRenderer>().sprite = monochrome_tilemap_packed_240;
        pl1 = Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity);
        Player pl1Script = pl1.GetComponent<Player>();
        pl1Script.isOnGround = true;
        pl1Script.animator.SetBool("isOnGround", pl1Script.isOnGround);
        pl1Script.myRigidBody = pl1.GetComponent<Player>().GetComponent<Rigidbody2D>();
    }

    public void Jump()
     {
        Player pl1Script = pl1.GetComponent<Player>();
        bool isJumpInput = Input.GetKeyDown("space");
        if (isJumpInput && pl1Script.isOnGround)
        {
            pl1Script.myRigidBody.velocity = new Vector3(0, jumpHeight, 0);
            pl1Script.isOnGround = false;
            pl1Script.animator.SetBool("isOnGround", pl1Script.isOnGround);
        }
    }
    public void Move()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        Player pl1Script = pl1.GetComponent<Player>();
        pl1Script.animator.SetFloat("Speed", horizontalInput * moveSpeed * Time.deltaTime);
        movementDirection = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        //update the position
    }
    public void Update()
    {
        Player pl1Script = pl1.GetComponent<Player> ();
        pl1Script.myRigidBody.velocity = movementDirection * gravity * Time.deltaTime;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");

        if(collision.gameObject.CompareTag("Wall"))
        {
            Vector3 slideDirection = Vector3.Reflect(myRigidBody.velocity, collision.contacts[0].normal);
            ApplySlideEffect(slideDirection);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            isOnGround = true;
            animator.SetBool("isOnGround", isOnGround);
        }
    }
    public bool getIsOnGround()
    {
        return isOnGround;
    }
    public void setIsOnGround(bool pIsOnGround)
    {
        isOnGround = pIsOnGround;
    }

    public void ApplySlideEffect(Vector3 slideDirection)
    {
        movementDirection= slideDirection;
    }
}

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

    public void CreatePlayer(float Spawnx, float Spawny)
    {
        pl1 = GameObject.FindGameObjectWithTag("Player");
        x = Spawnx; y = Spawny;
        gameObject.GetComponent<SpriteRenderer>().sprite = monochrome_tilemap_packed_240;
        pl1 = Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity);
        Player pl1Script = pl1.GetComponent<Player>();
        pl1Script.isOnGround = true;
        pl1Script.animator.SetBool("isOnGround", pl1Script.isOnGround);
        myRigidBody = pl1.GetComponent<Player>().GetComponent<Rigidbody2D>();
    }

    public void Jump()
     {
        Player pl1Script = pl1.GetComponent<Player>();
        bool isJumpInput = Input.GetKeyDown("space");
        if (isJumpInput && pl1Script.isOnGround)
        {
            myRigidBody.velocity = new Vector3(0, jumpHeight, 0);
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
        
        //update the position
        pl1.transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime,0, 0);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "tilemap_collision")
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
}

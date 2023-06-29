using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player: MonoBehaviour
{
    public GameObject pl1;
    public Rigidbody2D myRigidBody;
    public Sprite monochrome_tilemap_packed_240;
    private float x = 0;
    private float y = 0;
    public float jumpHeight = 0;
 
    public float moveSpeed = 100;

    public void CreatePlayer(float Spawnx, float Spawny)
    {
       
        x=Spawnx; y=Spawny;
        gameObject.GetComponent<SpriteRenderer>().sprite = monochrome_tilemap_packed_240;
        pl1 = Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity);
        myRigidBody = pl1.GetComponent<Player>().GetComponent<Rigidbody2D>();
    }
    public void Move()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isJumpInput = Input.GetKeyDown("space");
        if (isJumpInput)
            myRigidBody.velocity = new Vector3(0, jumpHeight, 0);
        //update the position
        pl1.transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime,0, 0);

        //output to log the position change
        Debug.Log(transform.position);
    }
}

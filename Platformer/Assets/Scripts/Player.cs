using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player: MonoBehaviour
{ 
    public Sprite monochrome_tilemap_packed_240;
    private float x = 0;
    private float y = 0;
 
    public float moveSpeed = 100;

    public void CreatePlayer(float Spawnx, float Spawny)
    {
        x=Spawnx; y=Spawny;
        gameObject.GetComponent<SpriteRenderer>().sprite = monochrome_tilemap_packed_240;
        Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity);
    }
    public void Move()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");

        //update the position
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        //output to log the position change
        Debug.Log(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player: MonoBehaviour
{ 
    public Sprite monochrome_tilemap_packed_240;
    private float x;
    private float y;
 
    public float moveSpeed = 1;

    public void CreatePlayer(float x, float y)
    {
        GetComponent<SpriteRenderer>().sprite = monochrome_tilemap_packed_240;
        Instantiate(gameObject, new Vector3(0, 0, -1), Quaternion.identity);
        gameObject.transform.position = new Vector3(x, y, -1.0f);
    }
    public bool Move()
    {
        if (Input.GetKey("d"))
        {
            x += moveSpeed;
            this.transform.position = new Vector3(x, y, 0);
            return true;
        }
        if (Input.GetKey("a"))
        {
            x -= moveSpeed;
            this.transform.position = new Vector3(x, y, 0);
            return true;
        }


        return false;
    }
}

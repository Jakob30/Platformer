using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject player;
    public float SpawnX = -40;
    public float SpawnY = 0;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>().CreatePlayer(SpawnX, SpawnY);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().Move()) 
        { 

        }
    }


}

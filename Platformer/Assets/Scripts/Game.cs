using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;
    private Player pl1;
    
    public float SpawnX = 0;
    public float SpawnY = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer(SpawnX, SpawnY);
    }
    private void Update()
    {
        PlayerMove();
    }
    public void PlayerMove()
    {
        if (pl1!= null)
        {
            pl1.Movement();
        }
    }
    public void CreatePlayer(float spawnX, float spawnY)
    {
        playerInstance = Instantiate(playerPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        pl1 = playerInstance.GetComponent<Player>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;
    private Player pl1;
    
    public float SpawnX = 0;
    public float SpawnY = 0;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer(SpawnX, SpawnY);
    }
    private void Update()
    {
        PlayerMove();
        if (pl1.transform.position.y < -2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    public void Retry()
    {
 
    }
}

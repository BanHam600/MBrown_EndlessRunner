using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton definition

    public static GameManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    public GameObject player;
    
    public bool isPlaying;
    
    public float currentScore;

    public int currentCollected;

    public List<GameObject> activeObstacles;

    public float currentObstacleSpeed;
    public float maxObstacleSpeed;
    public float acceleration = 0.1f;

    public bool canSpawn = true;

    public Transform playerLocation;
    public Vector2 respawnPoint;

    

    public string ScoreDisplay()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets score to the time played
        if (isPlaying == true)
        {
            currentScore += Time.deltaTime;

            if(currentObstacleSpeed < maxObstacleSpeed)
            {
                currentObstacleSpeed += acceleration * Time.deltaTime;
                ResumeObstacles();
            }

            
        }


        // reset game on Key press
        if (Input.GetKeyDown("r"))
        {
            if(isPlaying == true)
            {
                ResetGame();
            }
            else 
            {

                ResetGame();
                UIManager.Instance.GameOverDisplay();
            }

            
        }
       

    }

    public void GameOver()
    {
        isPlaying = false;
        PauseObstacles();
        Respawn();
        UIManager.Instance.GameOverDisplay();
    }

    public void ResetGame()
    {
        isPlaying = true;
        currentScore = 0;
        Respawn();
        player.SetActive(true);
        currentCollected = 0;
       
        foreach (GameObject go in activeObstacles)
        {
            Destroy(go);
        }

        activeObstacles.Clear();
        ResumeObstacles();

    }

    public void PauseObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();
            obstacleRB.velocity = Vector2.left * 0;
        }

        canSpawn = false;

    }   

   


    public void ResumeObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();
            obstacleRB.velocity = Vector2.left * currentObstacleSpeed;
        }
        canSpawn = true;
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
    }
   

}

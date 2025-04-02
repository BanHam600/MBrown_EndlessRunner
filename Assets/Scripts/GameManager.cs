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

    public bool canSpawn = true;

    

    public string ScoreDisplay()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    private void Start()
    {
        StartPlatformTimer();

    }

    // Update is called once per frame
    void Update()
    {

        if (isPlaying == true)
        {
            currentScore += Time.deltaTime;

            bool onGround = Physics2D.OverlapCircle(feetPos.position, 02.5f, groundLayer);
            UpdatePlayerPlatformState(onGround);
        }

        

        if (Input.GetKeyDown("j"))
        {
            ResetGame();
        }
            
    }

    public void GameOver()
    {
        currentScore = 0;
        isPlaying = false;
    }

    public void ResetGame()
    {
        isPlaying = true;
        currentScore = 0;
        player.SetActive(true);
        currentCollected = 0;
       
        foreach (GameObject go in activeObstacles)
        {
            Destroy(go);
        }

        activeObstacles.Clear();
        ResumeObstacles();

        platformMoved = false;

        if (platformCoroutine != null)
        {
            StopCoroutine(platformCoroutine);
            platformCoroutine = null;
        }

        StartPlatformTimer();
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

    public Rigidbody2D startingPlatRB;

    public float platformForce = 5f;

    public Transform feetPos;

    public LayerMask groundLayer;

    public bool playerOnStartingPlat = true;

    private bool platformMoved = false;

    private Coroutine platformCoroutine;

    public void UpdatePlayerPlatformState (bool onPlatform)
    {
        if (playerOnStartingPlat != onPlatform && !platformMoved)
        {
            playerOnStartingPlat = onPlatform;
            StartPlatformTimer ();
        }
    }



    void StartPlatformTimer()
    {
        if (platformCoroutine != null)
        {
            StopCoroutine(platformCoroutine);
        }

        float delay = playerOnStartingPlat ? 5f : 3f;

        platformCoroutine = StartCoroutine(MovePlatformAfterDelay(delay));
    }

    public float platformTimerRemaining;

    IEnumerator MovePlatformAfterDelay(float delay)
    {

        float timeRemaining = delay;
        
        while (timeRemaining > 0f)
        {
            platformTimerRemaining = timeRemaining;
            yield return null;
            timeRemaining -= Time.deltaTime;
        }

        platformTimerRemaining = 0f;

        Debug.Log("Platform timer started with delay: " + delay);
        yield return new WaitForSeconds(delay);

        if (!platformMoved)
        {
            Debug.Log("Applying force to starting platform");
            startingPlatRB.AddForce(Vector2.left * platformForce, ForceMode2D.Impulse);
            platformMoved = true;
        }
    }
}

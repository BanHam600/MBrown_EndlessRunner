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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetGame();
        }
            
    }

    public void ResetGame()
    {
        isPlaying = true;
        currentScore = 0;
        player.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] public GameObject startMenuPanel;
    

    

    public TMP_Text scoreText;
    public TMP_Text finalScore;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        startMenuPanel.SetActive(false);
        GameOverDisplay();
        StartMenu();
    }


    private void OnGUI()
    {
        scoreDisplay.text = GameManager.Instance.ScoreDisplay();
        

    }

    public void GameOverDisplay()
    {
        if (gameOverPanel.activeSelf == true)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            gameOverPanel.SetActive(true);
            FinalScore();
            
            
        }
    }

    public void StartMenu()
    {
        if (startMenuPanel.activeSelf == false)
        {
            startMenuPanel.SetActive(true);
        }
        else
        {
            startMenuPanel.SetActive(false);
        }

        GameManager.Instance.isPlaying = false;
    }

    public void FinalScore()
    {
        finalScore.text = GameManager.Instance.ScoreDisplay();
        finalScore.text = "Final Score: " + finalScore.text.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPlatform : MonoBehaviour
{

    public Rigidbody2D startingPlatRB;
    public float platformForce = 5f;
    public bool playerOnStartingPlat = true;

    private bool platformMoved = false;

    private Coroutine platformCoroutine;

    private void Start()
    {

        startingPlatRB = GetComponent<Rigidbody2D>();
        platformCoroutine = GetComponent<Coroutine>();
        

    }

    public void UpdatePlayerPlatformState(bool onPlatform)
    {
        if (playerOnStartingPlat != onPlatform && !platformMoved)
        {
            playerOnStartingPlat = onPlatform;
            StartPlatformTimer();
            
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

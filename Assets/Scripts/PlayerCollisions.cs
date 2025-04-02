using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            gameObject.SetActive(false);
            GameManager.Instance.isPlaying = false;
        }

        if (collision.GetComponent<Collectible>())
        { 
            
        }

        

    }

}

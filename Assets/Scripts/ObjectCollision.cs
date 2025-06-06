using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            if(GameManager.Instance.activeObstacles.Contains(this.gameObject))
            {
                GameManager.Instance.activeObstacles.Remove(this.gameObject);
            }

            Destroy(gameObject);
        }

    }
}

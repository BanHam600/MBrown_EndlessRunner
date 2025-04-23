using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPlatform : MonoBehaviour
{

    public Rigidbody2D startingPlatRB;
    public float platformForce = 5f;


    private void Start()
    {

        startingPlatRB = GetComponent<Rigidbody2D>();



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            startingPlatRB.velocity = Vector2.left * platformForce;
        }
    }
}

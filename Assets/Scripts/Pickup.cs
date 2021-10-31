using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    ScoreSystem scoreSystem;
    GameObject player;

    public static float score = 0;
    [SerializeField] float powerupTime = 2.4f;
    [SerializeField] int pointsToAdd = 1;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        scoreSystem = player.GetComponent<ScoreSystem>();
    }
    private void Update()
    {
        CheckIfPassed();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Pick(other));
        }
    }

    IEnumerator Pick(Collider2D player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        scoreSystem.AddScore(pointsToAdd);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(powerupTime);

        Destroy(gameObject);
    }


    private void CheckIfPassed()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            Destroy(gameObject,4f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos;

public class Laser : MonoBehaviour
{
    public Cainos.CharacterController playerController;
    public GameObject player;

    float offsetX;
    float offsetY;
    private void Start()
    { 
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<Cainos.CharacterController>();
        offsetX = transform.position.x - player.transform.position.x;
        offsetY = transform.position.y - player.transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x + offsetX, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Dead");
            playerController.IsDead = true;
        }
    }
}

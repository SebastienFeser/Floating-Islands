using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOutscreen : MonoBehaviour
{

    GameManager gameManager;
    AudioSource audioSource;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            audioSource.Play();
            gameManager.ResetPlayerPosition();
        }
    }
}

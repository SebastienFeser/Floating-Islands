using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
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
        

        if (collision.tag == "Player")
        {
            gameManager.ResetPlayerPosition();
            audioSource.Play();
        }
    }
}

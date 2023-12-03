using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        BUILD_MODE,
        PLAY_MODE
    }

    public GameState gameState;

    [SerializeField] TextMeshProUGUI buttonSwitchModeText;
    [SerializeField] AudioClip buildClip;
    [SerializeField] AudioClip playClip;
    AudioSource audioSource;
    JumpBetterTest jumpBetterTest;

    private void Start()
    {
        gameState = GameState.BUILD_MODE;
        jumpBetterTest = FindObjectOfType<JumpBetterTest>();
        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeGameMode()
    {
        if(gameState == GameState.BUILD_MODE)
        {
            audioSource.clip = playClip;
            audioSource.Play();
            gameState = GameState.PLAY_MODE;
            buttonSwitchModeText.text = "Build Mode";
        }
        else if(gameState == GameState.PLAY_MODE)
        {
            audioSource.clip = buildClip;
            audioSource.Play();
            gameState = GameState.BUILD_MODE;
            buttonSwitchModeText.text = "Play Mode";
            jumpBetterTest.ResetStartPosition();
        }
    }

    public void ResetPlayerPosition()
    {
        jumpBetterTest.ResetStartPosition();
    }

}

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
    JumpBetterTest jumpBetterTest;

    private void Start()
    {
        gameState = GameState.BUILD_MODE;
        jumpBetterTest = FindObjectOfType<JumpBetterTest>();
    }
    public void ChangeGameMode()
    {
        if(gameState == GameState.BUILD_MODE)
        {
            gameState = GameState.PLAY_MODE;
            buttonSwitchModeText.text = "Build Mode";
        }
        else if(gameState == GameState.PLAY_MODE)
        {
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

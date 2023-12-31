using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{

    public float amp = 1;
    public float freq = 1;
    private float initPos;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //initPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameState == GameManager.GameState.PLAY_MODE)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Sin(Time.time * freq) * amp + initPos);
        }
        else if (gameManager.gameState == GameManager.GameState.BUILD_MODE)
        {
            initPos = transform.position.y;
        }
    }
}

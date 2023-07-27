using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void OnEnable()
    {
        //Subscribe to events of the states of Game and Player
        GameManager.OnGameStateChanged += AllowControls;
        PlayerStateManager.OnPlayerStateChanged += HandleMovement;
    }

    private void OnDisable()
    {
        //Unsubscribe to events of the states of Game and Player
        GameManager.OnGameStateChanged -= AllowControls;
        PlayerStateManager.OnPlayerStateChanged -= HandleMovement;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Handle walking here?
    }


    //Handles player movement according to its state
    void HandleMovement(PlayerState playerState)
    {
        //TODO: If player changes state while moving handle it here for ex
        //PlayerStateManager.Instance.UpdatePlayerState(PlayerState.Swiming);

        //TODO: Check if the state is 
        if (playerState == PlayerState.Walking)
        {
            //Enable Walking
        }
    }

    //Allows or disables the controls according to game state
    void AllowControls(GameState gameState)
    {
        //Check if the game state is paused or not
        //if paused stop player controls 

    }


}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //gamestate enums, add more for other gamestates;;;;;; e.g slowtime
    public enum GameStates
    {
        GAMEPLAY,
        PAUSED
    }
    bool state_changing = false;
    public GameStates state;

    private void LateUpdate()
    {
        stateManager();
    }

    void stateManager()
    {
        //checks for player input, changes the state and then runs a bool to ensure the statechange doesnt loop
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = GameStates.PAUSED;
            state_changing = true;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            state = GameStates.GAMEPLAY;
            state_changing = true;
        }

        if (state_changing)
        {
            state_changing = false;
            switch(state)
            {
                case GameStates.GAMEPLAY:
                    gameplay();
                    break;
                case GameStates.PAUSED:
                    paused();
                    break;
            }
        }
        
    }

    //gamestate voids, flexible!
    void gameplay()
    {
        Time.timeScale = 1.0f;
    }
    void paused()
    {
        Time.timeScale = 0.0f;
    }
}

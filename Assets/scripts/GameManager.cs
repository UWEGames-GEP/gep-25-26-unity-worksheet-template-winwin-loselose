using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //gamestate enums, add more for other gamestates;;;;;; e.g slowtime
    public enum GameStates
    {
        GAMEPLAY,
        PAUSED,
        INVENTORY
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
            state_changed();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            state = GameStates.GAMEPLAY;
            state_changed();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            state = GameStates.INVENTORY;
            state_changed();
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
                case GameStates.INVENTORY:
                    inventory();
                    paused();
                    break;
            }
        }
        
    }
    public void state_changed()
    {
        state_changing = true;
    }
    //gamestate voids, flexible!
    void gameplay()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void paused()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
    }
    void inventory()
    {
        //possibly add more here??????
    }
}

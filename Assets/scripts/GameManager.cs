using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        GAMEPLAY,
        PAUSED
    }
    bool state_changing = false;
    [SerializeField]GameStates state;
    void Start()
    { }
    private void LateUpdate()
    {
        stateManager();
    }

    void stateManager()
    {
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
    void gameplay()
    {
        Time.timeScale = 1.0f;
    }
    void paused()
    {
        Time.timeScale = 0.0f;
    }
}

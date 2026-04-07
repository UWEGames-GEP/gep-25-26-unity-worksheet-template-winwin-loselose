using StarterAssets;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private StarterAssetsInputs _inputs;

    public AudioSource pickup_sfx;
    public AudioSource drop_sfx;
    public enum GameStates
    {
        GAMEPLAY,
        PAUSED,
        INVENTORY
    }
    public GameStates state;
    private bool state_changing = false;

    private void LateUpdate()
    {
        stateManager();
    }

    void stateManager()
    {
        if (_inputs.pause)
        {
            state = GameStates.PAUSED;
            state_changed();
            _inputs.pause = false;
        }

        if (_inputs._return)
        {
            state = GameStates.GAMEPLAY;
            state_changed();
            _inputs._return = false;
        }

        if (_inputs.inventory)
        {
            state = GameStates.INVENTORY;
            state_changed();
            _inputs.inventory = false;
        }

        if (state_changing)
        {
            state_changing = false;
            switch (state)
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

    void gameplay()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void paused()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void inventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
using StarterAssets;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private StarterAssetsInputs _inputs;

    public int grist;
    [SerializeField]TMP_Text grist_text;
    [SerializeField]Slider grist_slider;
    [SerializeField] GameObject grist_visual;
    [SerializeField] shopUI shopUI;
    double gristPickupTimer = 0.0f;

    public AudioSource pickup_sfx;
    public AudioSource drop_sfx;
    public enum GameStates
    {
        GAMEPLAY,
        PAUSED,
        INVENTORY,
        SHOP
    }
    public GameStates state;
    private bool state_changing = false;
    private void Start()
    {
        shopUI = GameObject.FindAnyObjectByType<shopUI>();
    }
    private void LateUpdate()
    {
        stateManager();
        if(grist_slider != null && grist_text != null )
        {
            grist_slider.value = grist;
            grist_text.text = grist.ToString();
        }

        if (gristPickupTimer > 0)
        {
            gristPickupTimer -= 0.35 * Time.deltaTime;
            if (grist_visual.transform.localPosition.y >= 260.0)
                grist_visual.transform.localPosition -= new Vector3(0, 200.0f * Time.deltaTime, 0);
            
        }
        else
        {
            if (grist_visual.transform.localPosition.y <= 460.0)
                grist_visual.transform.localPosition += new Vector3(0, 300.0f * Time.deltaTime, 0);
        }
            
    }

    public void addGrist(int value)
    {
        grist += value;
        gristPickupTimer = 1;
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
                case GameStates.SHOP:
                    shop();
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

    void shop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        
    }

    void inventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
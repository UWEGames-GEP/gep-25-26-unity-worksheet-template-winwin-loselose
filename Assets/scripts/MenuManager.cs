using UnityEngine;
using static GameManager;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameManager game_manager;
    [SerializeField] GameObject pause_menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //finds the game manager in the scene
        game_manager = GameObject.FindAnyObjectByType<GameManager>();
        pause_menu = GameObject.FindWithTag("p_menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (game_manager.state == GameStates.PAUSED)
            pause_menu.SetActive(true);
        else
            pause_menu.SetActive(false);
    }
    public void play()
    {
        game_manager.state = GameStates.GAMEPLAY;
        game_manager.state_changed();
    }
    public void options()
    {

    }
    public void quit()
    {
        Application.Quit();
    }
}

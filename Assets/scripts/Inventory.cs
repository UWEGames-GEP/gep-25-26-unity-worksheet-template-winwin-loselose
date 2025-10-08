using UnityEngine;
using System.Collections.Generic;
using static GameManager;
public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    [SerializeField]GameManager game_manager;
    [SerializeField]bool can_add_item = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //finds the game manager in the scene
        game_manager = GameObject.FindAnyObjectByType<GameManager>();
    }
    private void LateUpdate()
    {
        //checks if you can add items based on the pause state
        if (game_manager.state == GameStates.PAUSED)
            can_add_item = false;
        else
            can_add_item = true;

        //input test for adding items!
        if (Input.GetKeyDown(KeyCode.R))
            addItem("fuck");
        if (Input.GetKeyDown(KeyCode.T))
            removeItem("fuck");
    }

    //voids for adding and removing items to be accessed by other scripts
    public void addItem(string itemName)
    {
        if(can_add_item)
            items.Add(itemName);
    }
    public void removeItem(string itemName)
    {
        if(can_add_item)
            items.Remove(itemName);
    }
}

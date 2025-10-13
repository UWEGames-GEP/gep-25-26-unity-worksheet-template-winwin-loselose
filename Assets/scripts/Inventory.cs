using UnityEngine;
using System.Collections.Generic;
using static GameManager;
using TMPro;
using System.Linq;
public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    [SerializeField]GameManager game_manager;
    [SerializeField]bool can_add_item = false;
    [SerializeField] GameObject[] inventory_slots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //finds the game manager in the scene
        game_manager = GameObject.FindAnyObjectByType<GameManager>();
        inventory_slots = GameObject.FindGameObjectsWithTag("inventory");
        sortItems();
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
            removeLastItemDebug();
    }

    //voids for adding and removing items to be accessed by other scripts
    public void addItem(string itemName)
    {
        if(can_add_item)
            items.Add(itemName);
        sortItems();
    }
    public void removeItem(string itemName)
    {
        if(can_add_item)
            items.Remove(itemName);
        sortItems();
    }
    public void removeLastItemDebug()
    {
        if (can_add_item)
            items.Remove(items.Last());
        sortItems();
    }
    public void sortItems()
    {
        items.Sort();
        //clear all inventory names before setting new#
        for (int i = 0; i < inventory_slots.Length; i++)
        {
            inventory_slots[i].GetComponent<TMP_Text>().SetText("");
        }   
        //set inventory text/icons
        for (int i = 0; i < items.Count; i++)
        {
            inventory_slots[i].GetComponent<TMP_Text>().SetText(items[i].ToString());
        }
    }
}

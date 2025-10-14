using UnityEngine;
using System.Collections.Generic;
using static GameManager;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
public class Inventory : MonoBehaviour
{
    [SerializeField]private GameObject sylladex_item;
    public List<string> items = new List<string>();
    [SerializeField] GameManager game_manager;
    [SerializeField] bool can_add_item = false;
    [SerializeField] GameObject inventory_parent;
    public List<GameObject> inventory_slots = new List<GameObject>();
    GameObject to_destroy;
    float visual_timer = 1.6f;
    //[SerializeField] GameObject[] inventory_slots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //finds the game manager in the scene
        game_manager = GameObject.FindAnyObjectByType<GameManager>();

        //grabs inventory parent, loops through the amount of children in the parent to set the
        //exact amount of inventory slots by linking the children to the slots itself
        inventory_parent = GameObject.FindGameObjectWithTag("inventory");
        for (int i = 0; i < inventory_parent.transform.childCount; i++)
            inventory_slots.Add(inventory_parent.transform.GetChild(i).gameObject);
        //inventory_slots = GameObject.FindGameObjectsWithTag("inventory");
        //sortItems();
    }
    private void LateUpdate()
    {
        //checks if you can add items based on the pause state
        if (game_manager.state == GameStates.PAUSED)
            can_add_item = false;
        else
            can_add_item = true;

        if (game_manager.state == GameStates.INVENTORY)
        {
            inventory_parent.SetActive(true);
            can_add_item = false;
        }
        if (visual_timer > 0 && game_manager.state != GameStates.PAUSED)
        {
            inventory_parent.SetActive(true);
            visual_timer -= 1.0f * Time.deltaTime;
        }
            
        else
        {
            inventory_parent.SetActive(false);
            can_add_item = true;
        }

        //input test for adding items!
        if (Input.GetKeyDown(KeyCode.R))
            addItem("fuck");
        if (Input.GetKeyDown(KeyCode.T))
            removeLastItemDebug();
    }

    //voids for adding and removing items to be accessed by other scripts
    public void addItem(string itemName)
    {
        if (can_add_item)
        {
            visual_timer = 1.6f;
            items.Add(itemName);
            for (int i = 0; i < items.Count; i++)
            {
                if (inventory_slots[i].gameObject.transform.childCount < 1)
                {
                    Instantiate(sylladex_item, inventory_slots[i].transform);
                    Debug.Log("cum");
                }

            }
        }
        sortItems();
    }
    public void removeItem(string itemName)
    {
        if (can_add_item)
        {
            items.Remove(itemName);
            for (int i = 0; i < items.Count; i++)
            {
                //items.Remove(itemName);
                if (inventory_slots[i].gameObject.transform.childCount > 0)
                {
                    Destroy(inventory_slots[i].gameObject.transform.GetChild(0).gameObject);
                }
            }
                
        }
        sortItems();
    }
    public void removeLastItemDebug()
    {
        if (can_add_item)
        {
            items.Remove(items.Last());
            for (int i = items.Count; i > -1; i--)
            {
                //items.Remove(itemName);
                if (inventory_slots[i].gameObject.transform.childCount > 0)
                {
                    Destroy(inventory_slots[i].gameObject.transform.GetChild(0).gameObject);
                }
                break;
            }
        }
        sortItems();
    }
    public void sortItems()
    {
        items.Sort();
        //set inventory text
        /*for (int i = 0; i < items.Count; i++)
        {
            inventory_slots[i].GetComponent<TMP_Text>().SetText(items[i].ToString());
            grabs the inventory slot, checks if it has a child, if not, spawns a new sylladex item onto it.     
        }*/
    }
}

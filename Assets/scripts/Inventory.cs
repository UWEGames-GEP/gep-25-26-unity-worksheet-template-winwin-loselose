using UnityEngine;
using System.Collections.Generic;
using static GameManager;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using StarterAssets;
public class Inventory : MonoBehaviour
{
    [SerializeField]private GameObject sylladex_item;
    public List<GameObject> items = new List<GameObject>();
    [SerializeField] GameManager game_manager;
    [SerializeField] bool can_add_item = false;
    [SerializeField] GameObject inventory_parent;
    public List<GameObject> inventory_slots = new List<GameObject>();
    GameObject to_destroy;
    float visual_timer = 1.6f;
    [SerializeField] StarterAssetsInputs _inputs;
    [SerializeField] AudioSource pickup_sfx;
    [SerializeField] AudioSource drop_sfx;
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
        Debug.Log(items.Count);
        //checks if you can add items based on the pause state
        if (game_manager.state == GameStates.PAUSED)
            can_add_item = false;
        else
            can_add_item = true;

        if (game_manager.state == GameStates.INVENTORY)
        {
            inventory_parent.SetActive(true);
            for (int i = 0; i < items.Count; i++)
            {
                //resets positions
                if (inventory_slots[i].gameObject.transform.childCount > 0)
                {
                    inventory_slots[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Stop();
                    inventory_slots[i].gameObject.transform.GetChild(0).gameObject.transform.localPosition = new Vector2(0,0);
                }

            }
        }
        if (visual_timer > 0 && game_manager.state != GameStates.PAUSED)
        {
            inventory_parent.SetActive(true);
            visual_timer -= 1.0f * Time.deltaTime;
        }
            
        else if(game_manager.state != GameStates.INVENTORY)
        {
            inventory_parent.SetActive(false);
            can_add_item = true;
        }

        //input test for adding items!
        //if (_inputs.addItem)
        //{
        //    addItem("fuck");
        //    _inputs.addItem = false;
        //}

        if(_inputs.removeItem)
        {
            removeLastItemDebug();
            _inputs.removeItem = false;
        }

    }

    //voids for adding and removing items to be accessed by other scripts
    public void addItem(GameObject itemName)
    {
        if (can_add_item)
        {
            pickup_sfx.Play();
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
    public void removeItem(GameObject itemName)
    {
        if (can_add_item)
        {
            items.Remove(itemName);
            for (int i = 0; i < items.Count; i++)
            {
                //items.Remove(itemName);
                if (inventory_slots[i].gameObject.transform.childCount > 0)
                {
                   // Destroy(inventory_slots[i].gameObject.transform.GetChild(0).gameObject);
                }
            }
                
        }
        sortItems();
    }
    public void removeLastItemDebug()
    {
        if (can_add_item)
        {
            for (int i = items.Count; i > 0; i--)
            {
                if (inventory_slots[i].gameObject.transform.childCount > 0)
                {
                    Destroy(inventory_slots[i].gameObject.transform.GetChild(0).gameObject);
                }
                break;
            }
            for (int i = 0; i < items.Count; i++)
            {
                drop_sfx.Play();
                if (items[i].gameObject != null)
                    items[i].transform.parent = null;
                Debug.Log("did something?");
                items[i].gameObject.transform.position = new Vector3(this.gameObject.transform.position.x +1, this.gameObject.transform.position.y + 1.5f, this.gameObject.transform.position.z + 1);
                items.Remove(items.First ());
                
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

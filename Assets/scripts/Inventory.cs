using UnityEngine;
using System.Collections.Generic;
using static GameManager;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using StarterAssets;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    
    [SerializeField] GameManager game_manager;

    [SerializeField] bool can_add_item = false;
    [SerializeField] GameObject inventory_parent;
    [SerializeField] private GameObject sylladex_item;
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> inventory_slots = new List<GameObject>();
    public List<GameObject> slots_children = new List<GameObject>();

    GameObject to_destroy;
    float visual_timer = 1.6f;

    [SerializeField] StarterAssetsInputs _inputs;
    [SerializeField] AudioSource pickup_sfx;
    [SerializeField] AudioSource drop_sfx;

    Quaternion newRotation;
    Vector3 newPosition;
    private void Start()
    {
        //finds the game manager in the scene
        game_manager = GameObject.FindAnyObjectByType<GameManager>();

        //grabs inventory parent, loops through the amount of children in the parent to set the
        //exact amount of inventory slots by linking the children to the slots itself
        inventory_parent = GameObject.FindGameObjectWithTag("inventory");
        for (int i = 0; i < inventory_parent.transform.childCount; i++)
            inventory_slots.Add(inventory_parent.transform.GetChild(i).gameObject);
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
                //resets positions of the ui
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

        if(_inputs.removeItem)
        {
            removeItems();
            _inputs.removeItem = false;
        }

    }

    //voids for adding and removing items to be accessed by other scripts
    public void addItem(GameObject itemObj, string item_name)
    {
        if (can_add_item)
        {
            //adds visuals, palys sounds
            pickup_sfx.Play();
            visual_timer = 1.6f;
            items.Add(itemObj);
            for (int i = 0; i < items.Count; i++)
            {
                    
                    if (inventory_slots[i].gameObject.transform.childCount < 1)
                    {
                        //instantiates the ui elements under their respective slots to have animations and the like
                        Instantiate(sylladex_item, inventory_slots[i].transform);
                        slots_children.Add(inventory_slots[i].gameObject.transform.GetChild(0).gameObject);

                        //sets the colour of the grist visual in the ui based on the name it was given
                        switch (item_name)
                        {
                            case "red_grist":
                                inventory_slots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.red;
                                break;
                            case "orange_grist":
                                inventory_slots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.orange;
                                break;
                            case "green_grist":
                                inventory_slots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.green;
                                break;
                        }
                    }
            }
        }
        
    }   
    void spawnInfrontOfPlayer()
    {
        //grabs the position infront of the player
        Vector3 currentWorldPosition = transform.position;
        Vector3 forward = transform.forward;

        //randomizer on all axis, allowing for slightly better feeling gameplay
        Vector3 randomization = new Vector3(Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f));

        //combines all
        newPosition = (currentWorldPosition + randomization) + forward;
        newPosition += new Vector3(0, 0.5f, 0);

        //grabs the current rotation of the player, to let us use it int he future for spawning infront of the player based on its rotaiton
        Quaternion currentRotation = transform.rotation;
        newRotation = currentRotation * Quaternion.Euler(0, 0, 180);
    }
    public void removeItems()
    {
        spawnInfrontOfPlayer();
        if (can_add_item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                drop_sfx.Play();
                if (items[i].gameObject != null)
                    items[i].transform.parent = null;
                
                //sets item visible and "spawns" it infront of the player once more
                items[i].gameObject.SetActive(true);
                items[i].transform.position = newPosition;
                items[i].transform.rotation = newRotation;
                items.Remove(items[i]);
                break;

            }
         
            for (int i = 0; i < slots_children.Count; i++)
            {
                //removes the ui visual elements
                Destroy(slots_children[i].gameObject);
                slots_children.Remove(slots_children[i]);
            }
        }
        
        sortItems();
    }
    public void removeSelectedItem(int itemToRemove)
    {
        spawnInfrontOfPlayer();
        drop_sfx.Play();
        //checks for one clicked in list
        //finds association of object to that button
        //removes that one from list

        if (items[itemToRemove].gameObject != null)
            items[itemToRemove].transform.parent = null;

        items[itemToRemove].gameObject.SetActive(true);
        items[itemToRemove].transform.position = newPosition;
        items[itemToRemove].transform.rotation = newRotation;   
        items.Remove(items[itemToRemove]);
        Destroy(slots_children[itemToRemove].gameObject);
        slots_children.Remove(slots_children[itemToRemove]);
        
        sortItems();
    }
    public void sortItems()
    {
        items.Sort();
    }
}

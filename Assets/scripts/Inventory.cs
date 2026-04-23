using UnityEngine;
using System.Collections.Generic;
using static GameManager;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using StarterAssets;
using UnityEngine.UI;
using static UnityEditor.ShaderData;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem.HID;
using NUnit.Framework.Interfaces;
public class Inventory : MonoBehaviour
{

    public GameManager game_manager;
    public GameObject inventory_parent;
    public GameObject sylladex_item;
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> inventory_slots = new List<GameObject>();
    public List<GameObject> slots_children = new List<GameObject>();

    public bool can_add_item = true;
    public float visual_timer = 1.6f;
    public Vector3 newPosition;
    public Vector3 newRotation;

    public Vector3 cam_rotation;
    public int selected_card;
    public bool is_selected_card_empty = true;
    public Transform item_holder;
    public GameObject held_item;

    public Sprite katana, cal, seb;

    void Start()
    {
        game_manager = GameObject.FindWithTag("game_manager").GetComponent<GameManager>();
        inventory_parent = GameObject.FindWithTag("inventory");

        for (int i = 0; i < inventory_parent.transform.childCount; i++)
        {
            inventory_slots.Insert(0, inventory_parent.transform.GetChild(i).gameObject);
        };
    }

    public void checker(int check_num)
    {
        if (items.Count < 6)
        {
            visual_timer = 1.6f;
            for (int i = 0; i < items.Count; i++)
            {
                if (inventory_slots[i].transform.childCount <= 1)
                {
                    items[i].transform.SetParent(GameObject.FindWithTag("inventory_objects_parent").transform);
                    items[i].SetActive(false);
                    items[i].transform.position = Vector3.zero;

                    items[i].transform.GetComponent<BoxCollider>().enabled = true;
                }
            }

            if (inventory_slots[check_num].transform.childCount > 0)
            {
                Animation anim = inventory_slots[check_num].transform.GetChild(0).GetComponent<Animation>();
                    if (anim != null)
                {
                    anim.Stop();
                    anim.Play("item_pop_in");
                }
                game_manager.pickup_sfx.Play();

                held_item = items[check_num];

                held_item.SetActive(true);
                held_item.GetComponent<BoxCollider>().enabled = false;
                held_item.GetComponent<Rigidbody>().isKinematic = true;
                held_item.transform.SetParent(item_holder);

                is_selected_card_empty = false;
            }
            else
            {
                is_selected_card_empty = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { checker(0); if (!is_selected_card_empty) selected_card = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        { checker(1); if (!is_selected_card_empty) selected_card = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        { checker(2); if (!is_selected_card_empty) selected_card = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        { checker(3); if (!is_selected_card_empty) selected_card = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) 
        { checker(4); if (!is_selected_card_empty) selected_card = 4; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) 
        { checker(5); if (!is_selected_card_empty) selected_card = 5; }

        if (Input.GetKeyDown(KeyCode.F)) 
            remove_held_item();

        if(held_item != null)
            held_item.transform.position = item_holder.transform.position;

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (game_manager.state != GameManager.GameStates.INVENTORY)
            {
                game_manager.state = GameManager.GameStates.INVENTORY;
                game_manager.state_changed();

                for (int i = 0; i < inventory_slots.Count; i++)
                {
                    if (inventory_slots[i].transform.childCount > 0)
                    {
                        Animation anim = inventory_slots[i].transform.GetChild(0).GetComponent<Animation>();
                        if (anim != null)
                        {
                            anim.Stop();
                            anim.Play("reset_icon");
                        }
                    }
                }
            }
        }

        if (game_manager.state == GameManager.GameStates.PAUSED) can_add_item = false;
        else can_add_item = true;

        if (game_manager.state == GameManager.GameStates.INVENTORY) inventory_parent.SetActive(true);

        if (visual_timer > 0 && game_manager.state != GameManager.GameStates.PAUSED)
        {
            inventory_parent.SetActive(true);
            visual_timer -= 1.0f * Time.deltaTime;
        }

        if (game_manager.state != GameManager.GameStates.INVENTORY || game_manager.state == GameManager.GameStates.PAUSED)
        {
            if (visual_timer < 0)
            {
                inventory_parent.SetActive(false);
            }
            can_add_item = true;
        }
    }

    public void ui_fix()
    {

        foreach (GameObject slot in inventory_slots)
        {
            for (int i = slot.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(slot.transform.GetChild(i).gameObject);
            }
        }

        slots_children.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            GameObject item_node = items[i];
            GameObject instance = Instantiate(sylladex_item, inventory_slots[i].transform);

            // TELL THE BUTTON WHICH INDEX IT IS
            RemoveItemButton btnScript = instance.GetComponentInChildren<RemoveItemButton>();
            if (btnScript != null)
            {
                btnScript.index = i;
            }

            if (item_node.GetComponent<Item>().obj_name == "katana")
                instance.transform.GetChild(0).GetComponent<Image>().sprite = katana;
            if (item_node.GetComponent<Item>().obj_name == "seb")
                instance.transform.GetChild(0).GetComponent<Image>().sprite = seb;
            if (item_node.GetComponent<Item>().obj_name == "cal")
                instance.transform.GetChild(0).GetComponent<Image>().sprite = cal;
            slots_children.Add(instance);
            
        }
        for (int i = 0; i < inventory_slots.Count; i++)
        {
            if (inventory_slots[i].transform.childCount > 0)
            {
                Animation anim = inventory_slots[i].transform.GetChild(0).GetComponent<Animation>();
                if (anim != null)
                {
                    anim.Stop();
                    anim.Play("item_pop_in");
                }
            }
        }
    }

    public void addItem(GameObject itemObj, string item_name)
    {
        if (can_add_item && items.Count < 6)
        {
            if (itemObj.transform.childCount > 0)
            {
                itemObj.transform.parent = item_holder;
                itemObj.transform.GetComponent<BoxCollider>().enabled = false;
            }

            itemObj.SetActive(false);
            items.Add(itemObj);
            game_manager.pickup_sfx.Play();
            visual_timer = 1.6f;
            ui_fix();
            
        }
    }

    public void remove_held_item()
    {
        if (can_add_item && items.Count > selected_card)
        {
            Time.timeScale = 0.0f;
            spawnInFrontOfPlayer();
            GameObject item_to_remove = items[selected_card];

            item_to_remove.SetActive(true);
            item_to_remove.transform.SetParent(null); // root
            item_to_remove.transform.position = newPosition;
            item_to_remove.transform.eulerAngles = cam_rotation;

            object_unfreeze(item_to_remove);
            item_to_remove.transform.position = newPosition;
            items.RemoveAt(selected_card);
            held_item = null;
            ui_fix();
            Time.timeScale = 1.0f;
        }
    }

    public void object_unfreeze(GameObject item_to_remove)
    {
        Rigidbody rb = item_to_remove.GetComponent<Rigidbody>();
        if (rb != null)
        {
            item_to_remove.transform.GetComponent<BoxCollider>().enabled = true;
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        item_to_remove.transform.GetComponent<BoxCollider>().enabled = true;
    }

    public void spawnInFrontOfPlayer()
    {
        game_manager.drop_sfx.Play();
        Vector3 randomization = new Vector3(UnityEngine.Random.Range(0.1f, 0.25f), 0, UnityEngine.Random.Range(0.1f, 0.25f));

        newPosition = transform.Find("drop_area").position + randomization;
    }

    public void removeSelectedUIItem(int itemToRemove)
    {
        
   
        GameObject item_node = items[itemToRemove];
        item_node.transform.GetComponent<BoxCollider>().enabled = false;

        if (item_node != null)
        {
            spawnInFrontOfPlayer();
            item_node.SetActive(true);
            item_node.transform.SetParent(null);
            item_node.transform.position = newPosition;
            item_node.transform.eulerAngles = cam_rotation;
            object_unfreeze(item_node);
            item_node.transform.position = newPosition;
        }
        //Destroy(slots_children[itemToRemove].gameObject);
        items.RemoveAt(itemToRemove);
        //slots_children.Remove(slots_children[itemToRemove]);
        ui_fix();
    }
}
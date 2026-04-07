using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public string obj_name = "null";

    void Start()
    {
        inventory = GameObject.FindAnyObjectByType<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inventory != null && other.gameObject.CompareTag("Player"))
        {
            if(inventory.items.Count < inventory.inventory_slots.Count)
            {
                inventory.addItem(this.gameObject, obj_name);
                this.gameObject.SetActive(false);
            }

        }
    }
}

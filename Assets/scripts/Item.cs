using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    //public string item_type = "default";
    void Start()
    {
        inventory = GameObject.FindAnyObjectByType<Inventory>();
    }


    //checks if the player has entered the items range and adds itself to the inventory then deletes self
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (inventory != null && other.gameObject.CompareTag("Player"))
        {
            inventory.addItem(this.name);
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;

public class RemoveItemButton : MonoBehaviour
{
    public int index;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindAnyObjectByType<Inventory>();
    }

    public void onBtnPress()
    {
        inventory.removeSelectedUIItem(index);
    }
}

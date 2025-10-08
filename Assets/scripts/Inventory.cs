using UnityEngine;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
            addItem("fuck");
        if (Input.GetKeyDown(KeyCode.T))
            removeItem("fuck");
    }
    public void addItem(string itemName)
    {
        items.Add(itemName);
    }
    public void removeItem(string itemName)
    {
        items.Remove(itemName);
    }
}

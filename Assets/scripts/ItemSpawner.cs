using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]GameObject prefab_to_spawn;
    float timer;
    [SerializeField] Material material_red;
    [SerializeField] Material material_orange;
    [SerializeField] Material material_green;

    void Update()
    {
        if(timer > 0)
        {
            timer -= 1.0f * Time.deltaTime;
        }
        else
        {
            Instantiate(prefab_to_spawn, this.transform);
            int colour = Random.Range(0, 3);
            float size = Random.Range(0.2f, 0.35f);
            prefab_to_spawn.gameObject.transform.localScale = new Vector3(size, size, size);
            switch (colour)
            {
                case 0:
                    prefab_to_spawn.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = material_red;
                    prefab_to_spawn.gameObject.GetComponent<Item>().obj_name = "red_grist";
                    break;
                case 1:
                    prefab_to_spawn.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = material_orange;
                    prefab_to_spawn.gameObject.GetComponent<Item>().obj_name = "orange_grist";
                    break;
                case 2:
                    prefab_to_spawn.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = material_green;
                    prefab_to_spawn.gameObject.GetComponent<Item>().obj_name = "green_grist";
                    break;
            }
            //timer loop
            timer = Random.Range(4, 12);
        }
    }
}

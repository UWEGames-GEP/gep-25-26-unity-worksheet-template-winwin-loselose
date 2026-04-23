using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]GameObject prefab_to_spawn;
    float timer;

    void Update()
    {
        if(timer > 0)
        {
            timer -= 1.0f * Time.deltaTime;
        }
        else
        {
            Instantiate(prefab_to_spawn, this.transform);
            float size = Random.Range(0.2f, 0.35f);
            prefab_to_spawn.gameObject.transform.localScale = new Vector3(size, size, size);
            //timer loop
            timer = Random.Range(4, 12);
        }
    }
}

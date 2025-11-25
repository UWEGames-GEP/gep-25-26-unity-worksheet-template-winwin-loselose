using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]GameObject prefab_to_spawn;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= 0.1f * Time.deltaTime;
        }
        else
        {
            Instantiate(prefab_to_spawn, this.gameObject.transform);
            timer = 3;
        }
    }
}

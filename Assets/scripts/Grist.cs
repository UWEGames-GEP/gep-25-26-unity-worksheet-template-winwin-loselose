using UnityEngine;

public class Grist : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] int value = 5;

    void Start()
    {
        value = 5;
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager != null && other.CompareTag("Player"))
        {
            gameManager.addGrist(value);
            gameManager.pickup_sfx.Play();
            Destroy(this.gameObject );
        }
    }
}

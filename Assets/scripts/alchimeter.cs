using UnityEngine;

public class alchimeter : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject defaultItem;
    [SerializeField] GameObject unbreakableKatana;
    [SerializeField] GameObject lilCal;
    [SerializeField] GameObject lilSeb;
    [SerializeField]GameObject spawnPos;
    [SerializeField] GameObject shopInteractVisual;
    [SerializeField] GameObject shopVisual;
    bool inRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inRange = false;
        //shopVisual = GameObject.FindAnyObjectByType<shopUI>().gameObject; //grabs shopui
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        //spawnPos.transform.position;
    }
    private void Update()
    {
        

        if (Input.GetKey(KeyCode.E))
        {
            shopVisual.SetActive(true);
            gameManager.state = GameManager.GameStates.SHOP;
            gameManager.state_changed();
        }
        if (gameManager.state != GameManager.GameStates.SHOP)
            shopVisual.SetActive(false);
    }
    // Update is called once per frame
    public void objSpawn(string obj)
    {
        switch (obj)
        {
            case "unbreakable_katana":
                Instantiate(unbreakableKatana);
                unbreakableKatana.gameObject.transform.position = spawnPos.transform.position;
                break;
            case "lil_cal":
                Instantiate(lilCal);
                lilCal.gameObject.transform.position = spawnPos.transform.position;
                break;
            case "lil_seb":
                Instantiate(lilSeb);
                lilSeb.gameObject.transform.position = spawnPos.transform.position;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopInteractVisual.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        shopInteractVisual.SetActive(false);
        shopVisual.SetActive(false);
        inRange = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

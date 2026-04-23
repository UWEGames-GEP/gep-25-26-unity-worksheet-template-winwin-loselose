using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderData;

public class shopUI : MonoBehaviour
{
    [SerializeField]alchimeter alchemiter;
    [SerializeField]GameObject price_visual;
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text lack_of_funds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alchemiter = GameObject.FindAnyObjectByType<alchimeter>(); //grabs shop
        gameManager = GameObject.FindAnyObjectByType<GameManager>(); //grabs gamemanager

        //lack_of_funds.text = "you need: " + str(10) + "grist"
        //price_visual = $price
    }

    // Update is called once per frame
    void Update()
    {
        //price_visual.transform.position = mousepos
    }

    public void onKatanaBtn()
    {
        if (gameManager.grist < 5)
        {

        }
        //lack_of_funds = "you need: " + str(30) + "grist"
        // price_visual.text = lack_of_funds
        else
            alchemiter.objSpawn("unbreakable_katana");
            gameManager.grist -= 5;
    }
    public void onCalBtn()
    {
        if (gameManager.grist < 10)
        {

        }
        else
            alchemiter.objSpawn("lil_cal");
            gameManager.grist -= 10;
    }
    public void onSebBtn()
    {
        if (gameManager.grist < 15)
        {

        }
        else
            alchemiter.objSpawn("lil_seb");
            gameManager.grist -= 15;
    }
    public void exitShop()
    {

    }

}

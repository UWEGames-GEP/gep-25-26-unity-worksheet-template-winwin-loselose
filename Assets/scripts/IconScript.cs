using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IconScript : MonoBehaviour
{
    public Inventory inventory;
    public GameManager game_manager;
    public int slot_num = -1;

    public Animation anim_player;

    public Texture red_grist;
    public Texture green_grist;
    public Texture orange_grist;


    public Texture katana_texture;
    public Texture lil_cal_texture;
    public Texture lil_seb_texture;

    public RawImage icon_tex;

    void Awake()
    {
        anim_player = GetComponent<Animation>();
        icon_tex = transform.Find("TextureRect/icon_tex").GetComponent<RawImage>();
    }

    void Start()
    {
        inventory = GameObject.FindWithTag("inventory").GetComponent<Inventory>();

        game_manager = GameObject.FindWithTag("game_manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (game_manager.state == GameManager.GameStates.INVENTORY)
            {
                Vector3 pos = transform.GetChild(0).localPosition;
                pos.x = 0;
                transform.GetChild(0).localPosition = pos;
            }
            else
            {
                Vector3 pos = transform.GetChild(0).localPosition;
                pos.x = -256;
                transform.GetChild(0).localPosition = pos;
            }
        }
    }
}
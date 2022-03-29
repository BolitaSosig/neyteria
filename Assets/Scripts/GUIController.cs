using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarRelleno;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private GameObject staminaBarRelleno;

    //Escala de HealtBar y su relleno
    float HBscaleY;
    float HBscaleX;
    float HBRscaleY;
    float HBRscaleX;

    //Escala de StamineBar y su relleno
    float SBscaleY;
    float SBscaleX;
    float SBRscaleY;
    float SBRscaleX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Escala de HealtBar y su relleno
        HBscaleY = healthBar.transform.localScale.y;
        HBscaleX = healthBar.transform.localScale.x;

        HBRscaleY = healthBarRelleno.transform.localScale.y;
        HBRscaleX = healthBarRelleno.transform.localScale.x;

        //Escala de StamineBar y su relleno
        SBscaleY = staminaBar.transform.localScale.y;
        SBscaleX = staminaBar.transform.localScale.x;

        SBRscaleY = staminaBarRelleno.transform.localScale.y;
        SBRscaleX = staminaBarRelleno.transform.localScale.x;
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector2(HBscaleX *(player.MaxHP / 100f), HBscaleY);
        healthBarRelleno.transform.localScale = new Vector2(HBRscaleX * (player.HP / 100f), HBRscaleY);
    }

    void UpdateStaminaBar()
    {
        staminaBar.transform.localScale = new Vector2(SBscaleX * (player.MaxStamina / 100f), SBscaleY);
        staminaBarRelleno.transform.localScale = new Vector2(SBRscaleX * (player.Stamina / 100f), SBRscaleY);
    }
}

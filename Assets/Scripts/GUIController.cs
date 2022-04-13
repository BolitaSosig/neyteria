using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarRelleno;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private GameObject staminaBarRelleno;
    [SerializeField] private TextMeshProUGUI[] moduloCD = new TextMeshProUGUI[3];
    [SerializeField] public Image[] moduloCB = new Image[3];
    [SerializeField] public RawImage[] moduloSprite = new RawImage[3];

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
        UpdateModulosTimings();
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

    void UpdateModulosTimings()
    {
        
        for(int i = 0; i < 3; i++)
        {
            if (player.Modulos[i] != null)
            {
                float cd = (float)player.Modulos[i].GetType().GetField("cd").GetValue(player.Modulos[i]);
                float Duracion = (float)player.Modulos[i].GetType().GetField("Duracion").GetValue(player.Modulos[i]);
                float TdE = (float)player.Modulos[i].GetType().GetField("TdE").GetValue(player.Modulos[i]);
                if (cd <= 0) { moduloCD[i].text = ""; moduloCB[i].fillAmount = 1; moduloCB[i].enabled = false; moduloSprite[i].color = Color.white; }
                else
                {
                    if (moduloCB[i].fillAmount == 0) {
                        moduloCB[i].enabled = false;
                        moduloSprite[i].color = new Color(0.3f,0.3f,0.3f);
                        moduloCD[i].text = cd.ToString();
                        moduloCD[i].text = moduloCD[i].text.Substring(0, moduloCD[i].text.IndexOf(",") + 2);
                    } else
                    {
                        moduloCB[i].enabled = true;
                        moduloCB[i].fillAmount = (cd - TdE + Duracion) /Duracion;
                        Debug.Log((cd - TdE + Duracion) / Duracion);
                    }

                }
            }
        }    
    }
}

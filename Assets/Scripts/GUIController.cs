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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector2(player.MaxHP / 100f, 1);
        healthBarRelleno.transform.localScale = new Vector2(player.HP / 100f, 1);
    }

    void UpdateStaminaBar()
    {
        staminaBar.transform.localScale = new Vector2(player.MaxStamina / 100f, 1);
        staminaBarRelleno.transform.localScale = new Vector2(player.Stamina / 100f, 1);
    }
}

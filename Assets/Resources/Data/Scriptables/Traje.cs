using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Traje", menuName = "Scriptable/Traje")]
public class Traje : Item
{
    public float defensa;
    public float peso;



    public static void Pasiva(int id, bool on)
    {
        switch(id)
        {
            case 251:
                TrajeVitalidadPasiva(on);
                break;
            case 252:
                MantoFuriaPasiva(on);
                break;
            case 253:
                RopaRauvnirPasiva(on);
                break;
            case 254:
                TunicaMensajeroPasiva(on);
                break;
            case 255:
            case 258:
                if(on)
                    FindObjectOfType<PlayerTrajes>().PasiveChecker(id); ;
                break;
        }
    }

    private static void TrajeVitalidadPasiva(bool on)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.AdicHP += on ? 20 : -20;
    }

    private static void MantoFuriaPasiva(bool on)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.AumAttack += on ? 0.2f : -0.2f;
    }

    private static void RopaRauvnirPasiva(bool on)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.AttSpeed += on ? 0.15f : -0.15f;
    }

    private static void TunicaMensajeroPasiva(bool on)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.MovSpeed += on ? 0.15f : -0.15f;
    }

    private static void ConjuntoVoladorPasiva(bool on)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
    }
}

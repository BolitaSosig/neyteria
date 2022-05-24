using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleAdmin : MonoBehaviour
{
    private bool _showing = false;
    private Vector3 originalPos;
    private float originalTimeScale = 1f;

    // REFERENCIAS
    private PlayerController _playerController;
    private PlayerItems _playerItems;

    private bool Showing
    {
        get { return _showing; }
        set
        {
            _showing = value;
            if (_showing)
            {
                transform.position = originalPos;
                originalTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
            else { 
                transform.position -= new Vector3(0, 1000000f);
                Time.timeScale = originalTimeScale;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        Showing = false;

        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _playerItems = GameObject.FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckShowConsole();
    }

    void CheckShowConsole()
    {
        if (!Showing && Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.Return))
        {
            Showing = true;
        }
    }

    public void EnterCommand()
    {
        string command = GetComponent<TMP_InputField>().text;
        ProcessCommand(command);
        Showing = false;
        GetComponent<TMP_InputField>().text = "";
    }

    void ProcessCommand(string command)
    {
        string[] c = command.Split(" ".ToCharArray());
        string error = "[!] COMANDO ERRONEO. ";
        string success = "[+] COMANDO CORRECTO. ";
        if (c[0].Equals("comm"))
        {
            switch(c[1])
            {
                // comm health <cant>
                case "health":
                    float v = 0;
                    if (c[2].Equals("max"))
                    {
                        v = _playerController.MaxHP - _playerController.HP;
                        Debug.Log(success + "Salud del personaje aumentada al máximo");
                    }
                    else
                    {
                        v = float.Parse(c[2]);
                        Debug.Log(success + "Salud del personaje variada en " + c[2] + " puntos.");
                    }
                    _playerController.HP += v;
                    break;
                // comm selfkill
                case "selfkill":
                    _playerController.HP = 0f;
                    Debug.Log(success + "Personaje principal asesinado.");
                    break;
                //comm player <flag> [value]
                case "player":
                    switch (c[2])
                    {
                        case "health":
                            _playerController.HP = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "max_health":
                            _playerController.MaxHP = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "stamina":
                            _playerController.Stamina = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "max_stamina":
                            _playerController.MaxStamina = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "attack":
                            _playerController.Attack = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "defense":
                            _playerController.Defense = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "weight":
                            _playerController.Weight = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "mov_speed":
                            _playerController.MovSpeed = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "jump_cap":
                            _playerController.JumpCap = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "dash_range":
                            _playerController.DashRange = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "gasto_dash":
                            _playerController.gastoDash = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "stamina_vel_rec":
                            _playerController.StaminaVelRec = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "dmg_reduc":
                            _playerController.dmgReduc = float.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "inmune":
                            _playerController.Inmune = bool.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "stamina_decrease":
                            _playerController.staminaDecrease = bool.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "no_cd":
                            _playerController.noCD = bool.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "dash_on_air":
                            _playerController.dashOnAir = bool.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        case "one_hit_kill":
                            _playerController.oneHitKill = bool.Parse(c[3]);
                            Debug.Log(success + "'" + c[2] + "' del personaje principal establecido a " + c[3] + ".");
                            break;
                        default:
                            Debug.LogError(error + "El término '" + c[2] + "' del comando no existe.");
                            break;
                    }
                    break;
                // comm item <opt> <id> <cant>
                case "item":
                    if(c[2].Equals("give"))
                    {
                        int cant = c.Length > 4 ? int.Parse(c[4]) : 1;
                        _playerItems.Add(Item.getItemByID(int.Parse(c[3])), cant);
                        Debug.Log(success + "Otorgado x" + cant + " " + Item.getItemByID(int.Parse(c[3])).nombre + ".");
                    } else if(c[2].Equals("remove"))
                    {
                        if (c.Length > 4)
                        {
                            _playerItems.Remove(Item.getItemByID(int.Parse(c[3])), int.Parse(c[4]));
                            Debug.Log(success + "Eliminado x" + int.Parse(c[4]) + " " + Item.getItemByID(int.Parse(c[3])).nombre + ".");
                        }
                        else
                        {
                            _playerItems.Remove(Item.getItemByID(int.Parse(c[3])));
                            Debug.Log(success + "Eliminado todos los " + Item.getItemByID(int.Parse(c[3])).nombre + ".");
                        }
                    } else
                    {
                        Debug.LogError(error + "El término '" + c[2] + "' del comando no existe.");
                    }
                    break;
                // ERROR
                default:
                    Debug.LogError(error + "El término '" + c[1] + "' del comando no existe.");
                    break;
            }
        } else
        {
            Debug.LogError(error + "Comienza el comando siempre con el término 'comm'.");
        }
    }
}

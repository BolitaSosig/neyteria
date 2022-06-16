using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    private static bool isShow = false;
    public static bool answerReady = false;
    private static bool answer = false;

    private static Vector3 startPos;
    private static TextMeshProUGUI text;

    public static bool GetAnswer
    {
        get
        {
            answerReady = false;
            return answer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        startPos = 
            transform.position;
        FindObjectOfType<WarningMessage>().transform.position = startPos - new Vector3(0f, 1000f);
    }

    public static void Show(string msg)
    {
        if(!isShow)
        {
            isShow = true;
            text.text = msg;
            FindObjectOfType<WarningMessage>().transform.position = startPos;
        }
    }

    private static void UnShow(bool b)
    {
        FindObjectOfType<WarningMessage>().transform.position = startPos - new Vector3(0f, 1000f);
        answer = b;
        answerReady = true;
    }

    public static void Aceptar()
    {
        if(isShow)
        {
            isShow = false;
            UnShow(true);
        }
    }
    public static void Cancelar()
    {
        if (isShow)
        {
            isShow = false;
            UnShow(false);
        }
    }
}

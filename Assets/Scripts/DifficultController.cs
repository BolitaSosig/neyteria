using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultController : MonoBehaviour
{
    [SerializeField] Image normalButtonImage;
    [SerializeField] Image hardButtonImage;

    [SerializeField] int difficultValue;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("difficultValue"))
        {
            PlayerPrefs.SetFloat("difficultValue", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeDifficult1()
    {
        if (difficultValue != 0)
        {
            difficultValue = 0;
            Save();
        }

    }

    public void ChangeDifficult2()
    {
        if (difficultValue != 1)
        {
            difficultValue = 1;
            Save();
        }
    }

    private void Load()
    {
        difficultValue = PlayerPrefs.GetInt("difficultValue");

        if (difficultValue == 0) { normalButtonImage.color = Color.green; hardButtonImage.color = Color.white; }
        else { normalButtonImage.color = Color.white; hardButtonImage.color = Color.green; }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("difficultValue", difficultValue);
        Load();
    }
}

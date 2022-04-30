using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    [SerializeField] Light sceneLight;
    [SerializeField] Slider brightnessSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("brightnessValue"))
        {
            PlayerPrefs.SetFloat("brightnessValue", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeBrightness()
    {
        sceneLight.intensity = brightnessSlider.value;
        Save();
    }

    private void Load()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("brightnessValue");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("brightnessValue", brightnessSlider.value);
    }



}

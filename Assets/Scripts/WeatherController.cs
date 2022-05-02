using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Weather _current = Weather.Day;
    private Weather _last;
    private GLOBAL GLOBAL;
    public enum Weather
    {
        Day = 0,
        Night = 1,
        Storm = 2,
        Eclipse = 3
    }

    public bool WeatherChanged
    {
        get
        {
            return _current != _last;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _last = _current;
        GLOBAL = GameObject.Find("GLOBAL").GetComponent<GLOBAL>();
    }

    // Update is called once per frame
    void Update()
    {
        if(WeatherChanged)
        {
            ChangeWeather();
        }
    }

    public void ChangeWeather()
    {
        switch (_current)
        {
            case Weather.Day:
                break;
            case Weather.Night:
                SwitchNight();
                break;
        }
        _last = _current;
    }

    void SwitchNight()
    {
        //GLOBAL.player = 
    }
}

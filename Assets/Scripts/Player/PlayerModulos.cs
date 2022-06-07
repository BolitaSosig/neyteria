using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModulos : MonoBehaviour
{
    public Modulo[] _modulos = new Modulo[3];
    public float[] _duracion = new float[3];
    public float[] _cooldown = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
            Start(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Start(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Start(2);*/
    }

    public void Start(int s)
    {
        Launch(s);
    }

    public void Launch(int s)
    {
        if (_cooldown[s] == 0)
        {
            StartCoroutine(Skill(s));
        }
    }
    
    public IEnumerator Skill(int s)
    {
        Modulo.SkillOn(_modulos[s].ID, _modulos[s].Nv);
        StartCoroutine(Cooldown(s));
        SendMessageUpwards("ModuloSkillStarted", s);
        Coroutine dur = StartCoroutine(Duration(s));
        if (GetComponent<PlayerController>().noCD) 
            _cooldown[s] = 0;
        yield return dur;
        Modulo.SkillOff(_modulos[s].ID, _modulos[s].Nv);
    }

    public IEnumerator Duration(int s)
    {
        _duracion[s] = _modulos[s].Duracion;
        while (_duracion[s] > 0)
        {
            _duracion[s] -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        _duracion[s] = 0;
        yield return null;
    }

    public IEnumerator Cooldown(int s)
    {
        _cooldown[s] = _modulos[s].TdE;
        while (_cooldown[s] > 0)
        {
            _cooldown[s] -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        _cooldown[s] = 0;
    }

    public bool IsEquiped(Modulo m)
    {
        return m.Equals(_modulos[0]) || m.Equals(_modulos[1]) || m.Equals(_modulos[2]);
    }
}

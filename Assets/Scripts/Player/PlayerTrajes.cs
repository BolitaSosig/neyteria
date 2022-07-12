using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrajes : MonoBehaviour
{
    public Traje _traje = Item.TUNICA_PROTECTORA;
    private Traje _oldTraje;

    private PlayerStats _stats;
    private PlayerController _player;

    
    public bool TrajeChanged
    {
        get { return !_oldTraje.Equals(_traje); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<PlayerStats>();
        _player = GetComponent<PlayerController>();
        EquipTraje();
        _oldTraje = _traje;
    }

    // Update is called once per frame
    void Update()
    {
        if (TrajeChanged)
        {
            EquipTraje();
            _oldTraje = _traje;
        }
    }

    public void EquipTraje()
    {
        if(_oldTraje != null)
        {
            Traje.Pasiva(_oldTraje.ID, false);
            _stats.BaseDefense -= _oldTraje.defensa;
            _stats.BaseWeight -= _oldTraje.peso;
        }
        Traje.Pasiva(_traje.ID, true);
        _stats.BaseDefense += _traje.defensa;
        _stats.BaseWeight += _traje.peso;
        transform.Find("Traje").GetComponent<SpriteRenderer>().sprite = _traje.icono;
    }

    public void PasiveChecker(int id)
    {
        switch(id)
        {
            case 255:
                StartCoroutine(TunicaLigeraChecker());
                break;
            case 258:
                StartCoroutine(ArmaduraFtreqisChecker());
                break;
            case 261:
                StartCoroutine(MantoLocuraChecker());
                break;
        }
    }

    private void EnemyDefeated(Enemigo e)
    {
        switch(_traje.ID)
        {
            case 256:
                VestidoCosechadorPasiva(e.Nivel);
                break;
        }
    }

    private void StaminaConsumed(float cant)
    {
        switch (_traje.ID)
        {
            case 257:
                ConjuntoVoladorPasiva(cant);
                break;
        }
    }

    private void ModuloSkillStarted(int index)
    {
        switch (_traje.ID)
        {
            case 259:
                StartCoroutine(SabanaTiempoPasiva(index));
                break;
        }
    }

    private IEnumerator TunicaLigeraChecker()
    {
        PlayerController playerC = FindObjectOfType<PlayerController>();
        while (_traje.ID == 255)
        {
            if (!_stats.buffStorage.ContainsKey(255) && playerC.HP / playerC.MaxHP > 0.75f)
            {
                _stats.buffStorage.Add(255, 0);
                _stats.MovSpeed += 0.08f;
                _stats.AttSpeed += 0.07f;
            }
            else if (_stats.buffStorage.ContainsKey(255) && playerC.HP / playerC.MaxHP <= 0.75f)
            {
                _stats.buffStorage.Remove(255);
                _stats.MovSpeed -= 0.08f;
                _stats.AttSpeed -= 0.07f;
            }
            yield return null;
        }
        if (_stats.buffStorage.ContainsKey(255))
        {
            _stats.buffStorage.Remove(255);
            _stats.MovSpeed -= 0.08f;
            _stats.AttSpeed -= 0.07f;
        }
        yield return null;
    }

    private void VestidoCosechadorPasiva(int n)
    {
        float cant = n / 2.0f + 4.5f;
        _player.SendMessage("RecuperarSalud", cant);
    }

    private void ConjuntoVoladorPasiva(float c)
    {
        _player.Stamina += !_player.grounded && Random.value < 0.3f? c : 0;
    }

    private IEnumerator ArmaduraFtreqisChecker()
    {
        PlayerController playerC = FindObjectOfType<PlayerController>();
        while (_traje.ID == 258)
        {
            if (!_stats.buffStorage.ContainsKey(258) && playerC.HP / playerC.MaxHP < 0.25f)
            {
                _stats.buffStorage.Add(258, 0);
                _stats.AumAttack += 0.5f;
            }
            else if (_stats.buffStorage.ContainsKey(258) && playerC.HP / playerC.MaxHP >= 0.25f)
            {
                _stats.buffStorage.Remove(258);
                _stats.AumAttack -= 0.5f;
            }
            yield return null;
        }
        if (_stats.buffStorage.ContainsKey(258))
        {
            _stats.buffStorage.Remove(258);
            _stats.AumAttack -= 0.5f;
        }
        yield return null;
    }

    private int sabanaTiempoCount = 0;
    private IEnumerator SabanaTiempoPasiva(int i)
    {
        PlayerModulos mod = GetComponent<PlayerModulos>();
        if(sabanaTiempoCount < 3)
        {
            sabanaTiempoCount++;
            while (mod._cooldown[i] == 0) { yield return null; }
            mod._cooldown[i] -= 0.15f * mod._modulos[i].TdE;
            _stats.MovSpeed -= 0.05f;

            while (mod._cooldown[i] != 0) { yield return null; }
            _stats.MovSpeed += 0.05f;
            sabanaTiempoCount--;
        }
        yield return null;
    }

    private IEnumerator MantoLocuraChecker()
    {
        PlayerAttack playerA = FindObjectOfType<PlayerAttack>();
        while (_traje.ID == 261)
        {
            if (!_stats.buffStorage.ContainsKey(261) && playerA.arma.tipo == Arma.Tipo.Espada)
            {
                _stats.buffStorage.Add(261, 0);
            }
            else if (_stats.buffStorage.ContainsKey(261) && playerA.arma.tipo != Arma.Tipo.Espada)
            {
                _stats.buffStorage.Remove(261);
                _stats.AumAttack -= 0.5f;
            }
            yield return null;
        }
        if (_stats.buffStorage.ContainsKey(261))
        {
            _stats.buffStorage.Remove(261);
            _stats.AumAttack -= 0.5f;
        }
        yield return null;
    }
}

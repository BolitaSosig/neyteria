using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrajes : MonoBehaviour
{
    public Traje _traje = Item.TUNICA_PROTECTORA;
    private Traje _oldTraje;

    private PlayerStats _stats;
    
    public bool TrajeChanged
    {
        get { return !_oldTraje.Equals(_traje); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<PlayerStats>();
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
        }
        Traje.Pasiva(_traje.ID, true);
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
            yield return new WaitForSecondsRealtime(0);
        }
        if (_stats.buffStorage.ContainsKey(255))
        {
            _stats.buffStorage.Remove(255);
            _stats.MovSpeed -= 0.08f;
            _stats.AttSpeed -= 0.07f;
        }
        yield return null;
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
            yield return new WaitForSecondsRealtime(0);
        }
        if (_stats.buffStorage.ContainsKey(258))
        {
            _stats.buffStorage.Remove(258);
            _stats.AumAttack -= 0.5f;
        }
        yield return null;
    }
}

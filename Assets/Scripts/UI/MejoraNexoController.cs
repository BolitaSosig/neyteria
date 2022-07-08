using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MejoraNexoController : MonoBehaviour
{
    public bool _isShow;
    private Vector3 startPos;
    [Space]
    public TextMeshProUGUI _hp;
    public TextMeshProUGUI _stamina;
    public TextMeshProUGUI _attack;
    public TextMeshProUGUI _defense;
    public TextMeshProUGUI _weight;
    public TextMeshProUGUI _movSpeed;
    public TextMeshProUGUI _attSpeed;
    public TextMeshProUGUI _jumpCap;
    public TextMeshProUGUI _gastoDash;
    public TextMeshProUGUI _dmgReduc;
    [Space]
    public TextMeshProUGUI _moneyTotal;
    public TextMeshProUGUI _moneyCost;
    [Space]
    public TextMeshProUGUI _hpNv;
    public TextMeshProUGUI _staminaNv;
    public TextMeshProUGUI _attackNv;
    public TextMeshProUGUI _defenseNv;
    public TextMeshProUGUI _weightNv;

    private EquipmentController equipment;
    private PlayerItems items;
    private PlayerStats stats;


    // Start is called before the first frame update
    void Start()
    {
        equipment = FindObjectOfType<EquipmentController>();
        items = FindObjectOfType<PlayerItems>();
        stats = FindObjectOfType<PlayerStats>();
        startPos = transform.position;
        transform.position = startPos - new Vector3(0f, 1000f);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isShow)
        {
            UpdateNiveles();
            UpdateStats();
        }
    }

    void UpdateNiveles()
    {
        _moneyCost.text = stats.UpgradeCost.ToString();

        _hpNv.text = "nv " + stats.HpNv.ToString();
        _staminaNv.text = "nv " + stats.StaminaNv.ToString();
        _attackNv.text = "nv " + stats.AttackNv.ToString();
        _defenseNv.text = "nv " + stats.DefenseNv.ToString();
        _weightNv.text = "nv " + stats.WeightNv.ToString();

        _hpNv.color = stats.HpNv == 10 ? Color.red : Color.black;
        _staminaNv.color = stats.StaminaNv == 10 ? Color.red : Color.black;
        _attackNv.color = stats.AttackNv == 10 ? Color.red : Color.black;
        _defenseNv.color = stats.DefenseNv == 10 ? Color.red : Color.black;
        _weightNv.color = stats.WeightNv == 10 ? Color.red : Color.black;
    }

    void UpdateStats()
    {
        _hp.text = equipment._hp.text;
        _stamina.text = equipment._stamina.text;
        _attack.text = equipment._attack.text;
        _defense.text = equipment._defense.text;
        _weight.text = equipment._weight.text;
        _movSpeed.text = equipment._movSpeed.text;
        _attSpeed.text = equipment._attSpeed.text;
        _jumpCap.text = equipment._jumpCap.text;
        _gastoDash.text = equipment._gastoDash.text;
        _dmgReduc.text = equipment._dmgReduc.text;

        _moneyTotal.text = items.getByID(1).cant.ToString();
    }

    public void Show()
    {
        _isShow = !_isShow;
        if(_isShow)
        {
            transform.position = startPos;
        } else
        {
            transform.position = startPos - new Vector3(0f, 1000f);
        }
    }
}

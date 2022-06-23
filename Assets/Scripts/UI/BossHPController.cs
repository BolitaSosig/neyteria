using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHPController : MonoBehaviour
{
    [SerializeField] private GameObject hpObject;
    private Transform hpBar;
    private TextMeshProUGUI bossName;
    public Enemigo enemy;
    [SerializeField] private bool _show;

    private Vector3 startPos;

    public bool Show
    {
        get { return _show; }
        set 
        {
            _show = value && enemy.isActiveAndEnabled;
            hpObject.transform.position = _show ? startPos : startPos - new Vector3(0, 1000);
            bossName.text = _show ? enemy.name + " - Nivel " + enemy.Nivel : "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hpObject = GameObject.Find("BossHPBar");
        hpBar = hpObject.GetComponentsInChildren<Transform>()[2];
        bossName = hpObject.GetComponentInChildren<TextMeshProUGUI>();
        startPos = new Vector3(960, 115.97430419921875f);
        Show = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Show)
        {
            hpBar.transform.localScale = new Vector3(enemy.HP / enemy.MaxHP, hpBar.transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Show = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Show = false;
    }
}

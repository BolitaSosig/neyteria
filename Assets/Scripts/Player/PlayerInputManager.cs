using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController _player;
    private PlayerItems _items;
    private PauseMenu _pause;
    private InventoryController _inventoryController;
    private EquipmentController _equipmnetController;
    private ShopController _shopController;
    private MejoraNexoController _statsUpgradeController;
    private PlayerModulos _modulos;
    private ConsoleAdmin _console;

    public bool _isPause;
    public bool _isInventory;
    public bool _isEquipment;
    public bool _isEquipmentNexo;
    public bool _isStatsUpgrade;
    public bool _isShop;
    public bool _isConsole;

    public bool _useItemTimer = false;

    public bool CanPause
    {
        get { return !_isConsole && !_isInventory && !_isEquipment && !_isEquipmentNexo && !_isStatsUpgrade && !_isShop 
                && Input.GetButtonDown("Pause") ; }
    }
    public bool CanModulo
    {
        get { return !_isPause && !_isInventory && !_isEquipment && !_isConsole && !_isEquipmentNexo && !_isStatsUpgrade && !_isShop; }
    }
    public bool CanInventory
    {
        get 
        { 
            return 
                (!_isPause && !_isEquipment && !_isConsole && !_isEquipmentNexo && !_isStatsUpgrade && !_isShop &&
                    (Input.GetButtonDown("Inventory") || 
                    (Input.GetButton("InventoryJoystick1") && Input.GetButtonDown("InventoryJoystick2")))) ||
                (_isInventory && Input.GetButtonDown("Cancel"));  
        }
    }
    public bool CanEquipment
    {
        get 
        { 
            return 
                (!_isPause && !_isInventory && !_isConsole && !_isStatsUpgrade && !_isShop && 
                    (Input.GetButtonDown("Equipment") || 
                    (Input.GetButton("EquipmentJoystick1") && Input.GetButtonDown("EquipmentJoystick2")))) ||
                (_isEquipment && Input.GetButtonDown("Cancel"));  
        }
        set
        {

        }
    }

    public bool CanConsole
    {
        get 
        { 
            return 
                (!_isPause && Input.GetButton("Console1") && Input.GetButtonDown("Console2")) || 
                (_isConsole && (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Console2"))) ; 
        }
    }
    public bool CanConsoleHelp
    {
        get { return _isConsole; }
    }

    public bool CanUseItem
    {
        get { return !_useItemTimer && !_isPause && !_isInventory && !_isEquipment && !_isEquipmentNexo && !_isConsole && !_isStatsUpgrade && !_isShop && 
                Input.GetButtonDown("UseItem"); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _items = FindObjectOfType<PlayerItems>();
        _pause = FindObjectOfType<PauseMenu>();
        _inventoryController = FindObjectOfType<InventoryController>();
        _equipmnetController = FindObjectOfType<EquipmentController>();
        _shopController = FindObjectOfType<ShopController>();
        _statsUpgradeController = FindObjectOfType<MejoraNexoController>();
        _modulos = FindObjectOfType<PlayerModulos>();
        _console = FindObjectOfType<ConsoleAdmin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanPause)
            SetPause();
        if (CanModulo)
            SetModulo();
        if (CanConsole)
            SetConsole();
        if (CanConsoleHelp)
            SetConsoleHelp();
        if (CanInventory)
            SetInventory();
        if (CanEquipment)
            SetEquipment();
        if (CanUseItem)
            SetUseItem();
        if (Input.GetButtonDown("Cancel"))
        {
            if (_isEquipmentNexo)
                SetEquipmentNexo();
            if (_isShop)
                SetShop();
            if (_isStatsUpgrade)
                SetStatsUpgrade();
        }
    }

    void SetPause()
    {
        if (_isPause)
            _pause.Resume();
        else
            _pause.Pause();
        _isPause = !_isPause;
    }

    void SetModulo()
    {
        if (Input.GetButtonDown("Modulo1") || Input.GetAxisRaw("ModuloJoystick13") == 1)
            _modulos.Start(0);
        else if (Input.GetButtonDown("Modulo2") || Input.GetAxisRaw("ModuloJoystick2") == 1)
            _modulos.Start(1);
        else if (Input.GetButtonDown("Modulo3") || Input.GetAxisRaw("ModuloJoystick13") == -1)
            _modulos.Start(2);
    }

    void SetConsole()
    {
        _isConsole = !_isConsole;
        _console.Showing = _isConsole;
    }
    void SetConsoleHelp()
    {
        _console.GetHelp();
    }

    void SetInventory()
    {
        _isInventory = !_isInventory;
        _inventoryController.isShown = _isInventory;
    }

    void SetEquipment()
    {
        _isEquipment = !_isEquipment;
        _equipmnetController.IsShow = _isEquipment;
    }

    void SetShop()
    {
        _isShop = !_isShop;
        _shopController.isShown = _isShop;
    }

    void SetStatsUpgrade()
    {
        _isStatsUpgrade = !_isStatsUpgrade;
        _statsUpgradeController._isShow = _isStatsUpgrade;
    }

    void SetUseItem()
    {
        _useItemTimer = true;
        StartCoroutine(_items.UseQuickItem());
        StartCoroutine(UseItemTimer());
    }
    IEnumerator UseItemTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        _useItemTimer = false;
    }

    public void SetEquipmentNexo()
    {
        _isEquipmentNexo = !_isEquipmentNexo;
        SetEquipment();
    }
}

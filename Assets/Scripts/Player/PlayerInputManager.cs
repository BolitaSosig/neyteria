using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController _player;
    private PauseMenu _pause;
    private InventoryController _inventoryController;
    private EquipmentController _equipmnetController;
    private PlayerModulos _modulos;
    private ConsoleAdmin _console;

    public bool _isPause;
    public bool _isInventory;
    public bool _isEquipment;
    public bool _isConsole;

    public bool CanPause
    {
        get { return !_isConsole && !_isInventory && !_isEquipment && Input.GetButtonDown("Pause") ; }
    }
    public bool CanModulo
    {
        get { return !_isPause && !_isInventory && !_isEquipment && !_isConsole;  }
    }

    public bool CanConsole
    {
        get 
        { 
            return 
                (!_isPause && Input.GetButton("Console1") && Input.GetButtonDown("Console2")) || 
                (_isConsole && Input.GetButtonDown("Pause")) ; 
        }
    }
    public bool CanConsoleHelp
    {
        get { return _isConsole; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _pause = FindObjectOfType<PauseMenu>();
        _inventoryController = FindObjectOfType<InventoryController>();
        _equipmnetController = FindObjectOfType<EquipmentController>();
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
        if (Input.GetButtonDown("Modulo1"))
            _modulos.Start(0);
        else if (Input.GetButtonDown("Modulo2"))
            _modulos.Start(1);
        else if (Input.GetButtonDown("Modulo3"))
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPpal : MonoBehaviour
{
    public GameObject PpalFirstButton, OpcionesFirstButton, CreditosFirstButton;

    public RawImage ImagenCargando;
    public float progreso;

    AsyncOperation asyncLoad = null;

    Vector3 rotationEuler = new Vector3(0, 0, 0);

    SoundController soundController;
    MusicLevelController musicLevelController;

    GameObject player;
    bool firstTime = true;
    private Rigidbody2D _rigidbody2D;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.Find("CanvasM").GetComponent<SoundController>();
        player = GameObject.Find("Player");
        //_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player == null) SceneManager.LoadScene("GLOBAL", LoadSceneMode.Additive);
        //_playerController.canMove = false;
        StartCoroutine(WaitCoroutine2());
    }

    // Update is called once per frame
    void Update()
    {

        //Esto es para que gire la imagen de cargando
        //if (asyncLoad != null) {

        rotationEuler -= Vector3.forward * 1 * Time.deltaTime;     //Incrementa 15 grados cada vez
            transform.rotation = Quaternion.Euler(rotationEuler);
            ImagenCargando.rectTransform.Rotate(transform.rotation.eulerAngles);
        //}
    }

    public void EmpezarJuego()
    {
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        StartCoroutine(WaitCoroutine());
        //asyncLoad = SceneManager.LoadSceneAsync("Nivel1-1", LoadSceneMode.Single);

    }

    public void CerrarJuego()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        Debug.Log("Salir");
    }

    //Esto es para poder usar el teclado en el menu
    public void fPpalAOpciones() { EventSystem.current.SetSelectedGameObject(OpcionesFirstButton); }
    public void fOpcionesAPpal() { EventSystem.current.SetSelectedGameObject(PpalFirstButton); }
    public void fPpalACreditos() { EventSystem.current.SetSelectedGameObject(CreditosFirstButton); }
    public void fCreditosAPpal() { EventSystem.current.SetSelectedGameObject(PpalFirstButton); }

    public void ActivatedButton() { soundController.PlaySound2(); soundController.InvertDisableOnce(); }

    IEnumerator WaitCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 3 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);


        SceneManager.LoadScene("Nivel1-1", LoadSceneMode.Single);
        SceneManager.LoadScene("RestZone", LoadSceneMode.Additive);
        _playerController.canMove = true;
        _rigidbody2D.simulated = true;

        musicLevelController = GameObject.Find("SoundManager").GetComponent<MusicLevelController>();
        musicLevelController.act = 0;
    }

    IEnumerator WaitCoroutine2()
    {

        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(0.5f);

        GameObject player = GameObject.Find("Player");
        _playerController = player.GetComponent<PlayerController>();
        _rigidbody2D = player.GetComponent<Rigidbody2D>();

        _playerController.canMove = false;
        _playerController.SeleccionarEventSystem();
        _rigidbody2D.simulated = false;


        GameObject.Find("GLOBAL").GetComponent<GLOBAL>().StartPos();

    }

}

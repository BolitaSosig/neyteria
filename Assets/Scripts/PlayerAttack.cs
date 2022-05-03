using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] bool attacking = false;
    [SerializeField] bool switchingWeapon = false;

    [SerializeField] bool canAttack = true;

    

    //Referencias
    private Animator _animator;
    private PlayerController _playerController;
    private SoundManager _audioSource;

    //Selector
    public int seleccionado = 2; //0 es pistola, 1 es espada, y 2 es maza
    GameObject _gun;
    GameObject _sword;
    GameObject _maze;

    //Gun
    private Transform _shootTransform;
    public GameObject bullet;
    public float shootForce = 15f;
    public float shootRate = 0.5f;

    //Sword
    private Transform _swordTransform;
    public LayerMask enemyLayers;
    public float swordRange = 0.5f;
    public float swordRate = 0.4f;

    //Maza
    private Transform _mazeTransform;
    public float mazeRange = 0.8f;
    public float mazeRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _audioSource = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        _shootTransform = GameObject.Find("ShootTransform").transform;
        _swordTransform = GameObject.Find("SwordTransform").transform;
        _mazeTransform = GameObject.Find("MazeTransform").transform;

        _gun = GameObject.Find("pistolas sci-fi_3");
        _sword = GameObject.Find("pivote-sword");
        _maze = GameObject.Find("pivote-maze");

        StartCoroutine(UpdateSeleccionado());
    }

    // Update is called once per frame
    void Update()
    {
        canAttack = _playerController.canMove;

        if (canAttack)
        {
            GunAttack();
            SwordAttack();
            MazeAttack();
            if (Input.GetKeyDown(KeyCode.Q) && !switchingWeapon) StartCoroutine(UpdateSeleccionado());
        }
        
        
    }

    void GunAttack()
    {
        if (seleccionado == 0 && Input.GetButtonDown("Fire1") && !attacking)
        {
            _audioSource.PlayAudioOneShot(Random.Range(0,2));
            _animator.SetTrigger("gunAttack");
            GameObject newBullet;
            newBullet = Instantiate(bullet, _shootTransform.position, _shootTransform.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * shootForce, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            float supAtt = _playerController.oneHitKill ? 999999f : 0f;
            newBullet.GetComponent<BulletScript>().gunDMG = _playerController.Attack + supAtt;
            Destroy(newBullet, 2);

            StartCoroutine(Cooldown(shootRate));
        }

    }


    void SwordAttack()
    {
        if (seleccionado == 1 && Input.GetButtonDown("Fire1") && !attacking)
        {
            _audioSource.PlayAudioOneShot(Random.Range(2, 4));
            _animator.SetTrigger("swordAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_swordTransform.position, swordRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(swordDMG);
                //Debug.Log("colision con Enemy");
                float supAtt = _playerController.oneHitKill ? 999999f : 0f;
                enemy.SendMessage("GetDamageByPlayer", _playerController.Attack + supAtt);
            }

            StartCoroutine(Cooldown(swordRate));
        }
    }

    void MazeAttack()
    {

        if (seleccionado == 2 && Input.GetButtonDown("Fire1") && !attacking)
        {
            _animator.SetTrigger("mazeAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_mazeTransform.position, mazeRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(mazeDMG);
                //Debug.Log("colision con Enemy");
                float supAtt = _playerController.oneHitKill ? 999999f : 0f;
                enemy.SendMessage("GetDamageByPlayer", _playerController.Attack + supAtt);
            }

            _audioSource.PlayAudioOneShot(4);


            StartCoroutine(Cooldown(mazeRate));
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_swordTransform == null) return;

        Gizmos.DrawWireSphere(_swordTransform.position, swordRange);
        Gizmos.DrawWireSphere(_mazeTransform.position, mazeRange);

    }

    IEnumerator UpdateSeleccionado()
    {
        switchingWeapon = true;
        seleccionado = (seleccionado + 1) % 3; // seleciona siguiente indice arma
        if (seleccionado == 0) { _gun.SetActive(true); _sword.SetActive(false); _maze.SetActive(false); _playerController.Attack = 1f; }
        else if (seleccionado == 1) { _gun.SetActive(false); _sword.SetActive(true); _maze.SetActive(false); _playerController.Attack = 3f; }
        else /*(seleccionado == 2)*/ { _gun.SetActive(false); _sword.SetActive(false); _maze.SetActive(true); _playerController.Attack = 5f; }

        yield return new WaitForSecondsRealtime(1f);
        switchingWeapon = false;
    }

    IEnumerator Cooldown(float segundos)
    {
        attacking = true;
        yield return new WaitForSecondsRealtime(segundos);
        attacking = false;
    }



}
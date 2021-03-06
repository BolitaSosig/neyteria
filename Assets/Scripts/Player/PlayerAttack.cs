using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public bool attacking = false;
    //[SerializeField] bool switchingWeapon = false;
    [SerializeField] public bool canAttack = true;

    public Arma arma;
    private Arma arma_old;


    //Referencias
    private Animator _animator;
    private PlayerController _playerController;
    private PlayerStats _playerStats;
    private SoundManager _audioSource;

    //Selector
    //public int seleccionado = 2; //0 es pistola, 1 es espada, y 2 es maza
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
        _playerStats = GetComponent<PlayerStats>();
        _audioSource = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        _shootTransform = GameObject.Find("ShootTransform").transform;
        _swordTransform = GameObject.Find("SwordTransform").transform;
        _mazeTransform = GameObject.Find("MazeTransform").transform;

        _gun = GameObject.Find("pistolas sci-fi_3");
        _sword = GameObject.Find("pivote-sword");
        _maze = GameObject.Find("pivote-maze");

        UpdateSpriteArma();
        _playerStats.BaseAttack = arma.ataque;
        _playerStats.BaseWeight = arma.peso;
        arma_old = arma;
    }

    // Update is called once per frame
    void Update()
    {
        canAttack = _playerController.canMove && Time.timeScale > 0 && _playerController.Stamina >= 4.5f * _playerController.Weight;

        if (canAttack)
        {
            GunAttack();
            SwordAttack();
            MazeAttack();
            //if (Input.GetKeyDown(KeyCode.Q) && !switchingWeapon) StartCoroutine(UpdateSeleccionado());
        }
        
        if(!arma.Equals(arma_old))
        {
            _playerStats.BaseAttack += arma.ataque - arma_old.ataque;
            _playerStats.BaseWeight += arma.peso - arma_old.peso;
            arma_old = arma;

            UpdateSpriteArma();
        }
    }

    void GunAttack()
    {
        if (arma.tipo == Arma.Tipo.Laser && Input.GetButtonDown("Hit") && !attacking)
        {
            _audioSource.PlayAudioOneShot(Random.Range(0,2));
            _animator.SetTrigger("gunAttack");
            GameObject newBullet = Instantiate(bullet, _shootTransform.position, _shootTransform.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * shootForce, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            float supAtt = _playerController.oneHitKill ? 999999f : 0f;
            newBullet.GetComponent<BulletScript>().gunDMG = _playerController.Attack + supAtt;
            _playerController.Atacar();
            Destroy(newBullet, 2);

            StartCoroutine(Cooldown(shootRate / _playerController.AttSpeed));
        }

    }


    void SwordAttack()
    {
        if (arma.tipo == Arma.Tipo.Espada && Input.GetButtonDown("Hit") && !attacking)
        {
            _audioSource.PlayAudioOneShot(Random.Range(2, 4));
            _animator.SetTrigger("swordAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_swordTransform.position, swordRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(swordDMG);
                //Debug.Log("colision con Enemy");
                if (enemy.GetType().IsEquivalentTo(typeof(PolygonCollider2D)))
                {
                    float supAtt = _playerController.oneHitKill ? 999999f : 0f;
                    enemy.SendMessage("GetDamageByPlayer", _playerController.Attack + supAtt);
                }
            }
            _playerController.Atacar();

            StartCoroutine(Cooldown(swordRate / _playerController.AttSpeed));
        }
    }

    void MazeAttack()
    {

        if (arma.tipo == Arma.Tipo.Maza && Input.GetButtonDown("Hit") && !attacking)
        {
            _animator.SetTrigger("mazeAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_mazeTransform.position, mazeRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(mazeDMG);
                //Debug.Log("colision con Enemy");
                if (enemy.GetType().IsEquivalentTo(typeof(PolygonCollider2D)))
                {
                    float supAtt = _playerController.oneHitKill ? 999999f : 0f;
                    enemy.SendMessage("GetDamageByPlayer", _playerController.Attack + supAtt);
                }
            }
            _playerController.Atacar();

            _audioSource.PlayAudioOneShot(4);


            StartCoroutine(Cooldown(mazeRate / _playerController.AttSpeed));
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_swordTransform == null) return;

        Gizmos.DrawWireSphere(_swordTransform.position, swordRange);
        Gizmos.DrawWireSphere(_mazeTransform.position, mazeRange);

    }

    IEnumerator Cooldown(float segundos)
    {
        attacking = true;
        yield return new WaitForSecondsRealtime(segundos);
        attacking = false;
    }

    void UpdateSpriteArma()
    {
        if (arma.tipo == Arma.Tipo.Espada)
        {
            _sword.GetComponentInChildren<SpriteRenderer>().sprite = arma.icono;
            _gun.SetActive(false); _sword.SetActive(true); _maze.SetActive(false);
        }
        else if (arma.tipo == Arma.Tipo.Maza)
        {
            _maze.GetComponentInChildren<SpriteRenderer>().sprite = arma.icono;
            _gun.SetActive(false); _sword.SetActive(false); _maze.SetActive(true);
        }
        else
        {
            _gun.GetComponentInChildren<SpriteRenderer>().sprite = arma.icono;
            _gun.SetActive(true); _sword.SetActive(false); _maze.SetActive(false);
        }
    }

}
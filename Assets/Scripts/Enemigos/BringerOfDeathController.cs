using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Tilemaps;

public class BringerOfDeathController : EnemyCheckEnemy
{ 
    public GameObject[] enemigosNivel = new GameObject[5];

    //Attack
    [SerializeField] protected Transform _shootTransform;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float swordRange = 4.73f;
    public LayerMask playerLayers;

    [SerializeField] protected GameObject _audioSource;


    protected float seDispara = 9.95f;
    protected bool attacking = false;


    protected bool firstTimeMediaVida = true;
    protected bool firstTimePocaVida = true;

    protected CameraController _camera;
    protected GameObject _player;
    [SerializeField] private GameObject _togglePlatform;
    [SerializeField] private GameObject _transformPlataform;

    protected override void Start()
    {
        base.Start();

        _player = GameObject.Find("Player");
        _camera = GameObject.FindObjectOfType<CameraController>();
    }




    public override IEnumerator Die()
    {
        _animator.SetTrigger("dead");
        _canvasTranform.gameObject.SetActive(false);
        FindObjectOfType<PlayerController>().SendMessageUpwards("EnemyDefeated", this);
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _boxCollider2D.enabled = false;
        _polygonCollider2D.enabled = false;
        this.enabled = false;
        _rigidbody2D.gravityScale = 0f;
        _audioSource.SetActive(false);
        lookRadius = -1f;
        yield return new WaitForSecondsRealtime(1f);
        if (drop != null) drop.GetDrops(transform, itemOrbPrefab);
        if (_togglePlatform != null) _togglePlatform.SetActive(false);
        //Destroy(gameObject);

    }

    protected IEnumerator DoAttack()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("attack");
            yield return new WaitForSecondsRealtime(0.7f);

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_shootTransform.position, swordRange, playerLayers);

            foreach (Collider2D player in hitPlayers)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(swordDMG);
                Debug.Log("colision con Player");
                player.SendMessage("GetDamageByEnemy", Attack);
            }

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

            attacking = false;
        }
    }

    protected IEnumerator DoAbility()
    {
        if (!attacking)
        {
            attacking = true;



            _animator.SetTrigger("ability");
            yield return new WaitForSecondsRealtime(0.5f);
            GameObject newBullet;
            //newBullet = Instantiate(projectile, target.position, target.rotation);
            newBullet = Instantiate(projectile, new Vector3(target.position.x, target.position.y + 2, target.position.z), target.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            newBullet.GetComponent<BringerOfDeathProjectile>().gunDMG = Attack;
            Destroy(newBullet, 1.4f);

            if (Random.Range(0f, 10f) >= 9)
            {
                InstantiateEnemy(Random.Range(0, 5), 1, new Vector3(target.position.x, target.position.y + 2, target.position.z), target.rotation);
                /*GameObject newBullet;
                newBullet = Instantiate(enemigosNivel[Random.Range(0,5)], new Vector3(target.position.x, target.position.y + 2, target.position.z), target.rotation);
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);*/
            }

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

            attacking = false;
        }
    }

    protected override void DoAttacksAndAbilities()
    {
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility()); }
    }

    public void InstantiateEnemy(int idEnemigo, int nEnemigos, Vector3 donde, Quaternion rotacion)
    {
        for (int i = 0; i < nEnemigos; i++)
        {
            GameObject newEnemy;
            newEnemy = Instantiate(enemigosNivel[idEnemigo], donde, rotacion);
            newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        if (_shootTransform != null) Gizmos.DrawWireSphere(_shootTransform.position, swordRange);
    }

    protected void InvocarEnemigos1()
    {
        firstTimeMediaVida = false;

        for (int i = 0; i < 2; i++) InstantiateEnemy(Random.Range(0, 5), 1, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        InstantiateEnemy(2, 1, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        for (int i = 0; i < 5; i++)
        {
            GameObject newBullet;
            //newBullet = Instantiate(projectile, target.position, target.rotation);
            newBullet = Instantiate(projectile, new Vector3(gameObject.transform.position.x + Random.Range(-8, 8), target.position.y + 2, target.position.z), target.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            newBullet.GetComponent<BringerOfDeathProjectile>().gunDMG = Attack;
            Destroy(newBullet, 1.4f);
        }
    }

    protected void InvocarEnemigos2()
    {
        firstTimePocaVida = false;

        for (int i = 0; i < 2; i++) InstantiateEnemy(Random.Range(0, 4), 1, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        InstantiateEnemy(4, 2, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        for (int i = 0; i < 5; i++)
        {
            GameObject newBullet;
            //newBullet = Instantiate(projectile, target.position, target.rotation);
            newBullet = Instantiate(projectile, new Vector3(gameObject.transform.position.x + Random.Range(-8, 8), target.position.y + 2, target.position.z), target.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            newBullet.GetComponent<BringerOfDeathProjectile>().gunDMG = Attack;
            Destroy(newBullet, 1.4f);
        }
    }




    ////// RECIBE DAÑO //////
    public override void GetDamage(float dmg)
    {

        HP = Mathf.Max(0, HP - dmg);
        ShowDamageDeal(Mathf.RoundToInt(dmg));
        if (HP <= (MaxHP / 2) && firstTimeMediaVida) { InvocarEnemigos1(); healthBarEnemy.gameObject.GetComponent<RawImage>().color = Color.yellow; }
        if (HP <= (MaxHP / 5) && firstTimePocaVida) { InvocarEnemigos2(); healthBarEnemy.gameObject.GetComponent<RawImage>().color = Color.magenta; }

        if (HP <= 0) { StopAllCoroutines(); moving = true; _animator.SetBool("isDead", true); StartCoroutine(Die()); } else { _animator.SetTrigger("hurt"); }
    }
}


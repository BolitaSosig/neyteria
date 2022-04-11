using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //Referencias
    private Animator _animator;

        //Gun
        private Transform _shootTransform;
        public GameObject bullet;
        public float shootForce = 15f;
        public float shootRate = 0.5f;
        public float shootLastTime = 0;

        //Sword
        private Transform _swordTransform;
        public LayerMask enemyLayers;
        public float swordRange = 0.5f;
        public float swordRate = 0.3f;
        public float swordLastTime = 0;

        //Maza
        public float mazeRange = 0.7f;
        public float mazedRate = 1f;
        public float mazeLastTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _shootTransform = GameObject.Find("ShootTransform").transform;
        _swordTransform = GameObject.Find("SwordTransform").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GunAttack();
        SwordAttack();
        MazeAttack();
    }

    void GunAttack()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > shootLastTime)
        {
            Debug.Log("Se crea bullet");
            GameObject newBullet;
            newBullet = Instantiate(bullet, _shootTransform.position, _shootTransform.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * shootForce, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            Destroy(newBullet, 2);

            shootLastTime = Time.time + shootRate;
        }
        
    }


    void SwordAttack()
    {

        if (Input.GetButtonDown("Fire1") && Time.time > swordLastTime)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_swordTransform.position, swordRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
            }

            Debug.Log("golpe con la espada");

            swordLastTime = Time.time + swordRate;
        }
    }

    void MazeAttack()
    {

        if (Input.GetButtonDown("Fire1") && Time.time > swordLastTime)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_swordTransform.position, swordRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
            }

            Debug.Log("golpe con la espada");

            swordLastTime = Time.time + swordRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_swordTransform == null) return;

        Gizmos.DrawWireSphere(_swordTransform.position, swordRange);
   
    }





}

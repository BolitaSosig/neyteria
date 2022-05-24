using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class ArcherController : Enemigo
{
    //Arrow
    public Transform _shootTransform;
    public GameObject projectile;
    public float shootForce = 15f;

    float lookRadius = 10f;
    Transform target;

    float seDispara = 9.95f;
    bool attacking = false;

    
    protected override void Start()
    {
        base.Start();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    protected override void Update()
    {
        base.Update();
        CheckEnemy();
    }

    
    public IEnumerator DoAttack()
    {
        if (!attacking) {
        attacking = true;

        _animator.SetTrigger("attack");
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject newBullet;
        newBullet = Instantiate(projectile, _shootTransform.position, _shootTransform.rotation);
        //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.localScale.x * shootForce, newBullet.GetComponent<Rigidbody2D>().velocity.y);
        newBullet.GetComponent<ProjectileScript>().gunDMG = Attack;
        Destroy(newBullet, 2);

        yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

        attacking = false;
        }
    }

    public void CheckEnemy()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            //StopCoroutine(Moverse()); //moving = false; //StopAllCoroutines();
            if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }

            //Debug.Log(distance);
            // Move towards the target
            
            if (transform.position.x <= target.position.x)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(-Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(2, _rigidbody2D.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(-2, _rigidbody2D.velocity.y);
            }

        }

        else
        { //_rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            //StartCoroutine(Moverse());
        }

        _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator*/
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

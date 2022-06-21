using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class ArcherController : EnemyCheckEnemy
{
    //Arrow
    [SerializeField] protected Transform _shootTransform;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float shootForce = 15f;


    [SerializeField] protected float seDispara = 9.95f;
    [SerializeField] protected bool attacking = false;


    protected override void DoAttacksAndAbilities()
    {
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }
    }


    public virtual IEnumerator DoAttack()
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


    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

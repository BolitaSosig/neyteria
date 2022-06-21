using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class NightBorneController : EnemyCheckEnemy
{
    //Attack
    [SerializeField] protected Transform _shootTransform;
    [SerializeField] protected float swordRange = 0.9f;
    [SerializeField] protected LayerMask playerLayers;

    [SerializeField] protected float seDispara = 9.95f;
    [SerializeField] protected bool attacking = false;


    public IEnumerator DoAttack()
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

    protected override void DoAttacksAndAbilities()
    {
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        if (_shootTransform != null) Gizmos.DrawWireSphere(_shootTransform.position, swordRange);
    }
}

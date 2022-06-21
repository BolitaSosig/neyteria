using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Tilemaps;

public class ElementalWater : BringerOfDeathController
{
    public Transform _ability1Transform;
    public float ability1Range = 2.55f;
    public Transform _ability2Transform;
    public float ability2Range = 2.55f;

    protected override void DoAttacksAndAbilities()
    {
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility()); }

        if (!firstTimeMediaVida)
        {
            if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility1()); }
            if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility2()); }
        }
    }

    public IEnumerator DoAbility1()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("ability1");
            yield return new WaitForSecondsRealtime(0.7f);

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_ability1Transform.position, ability1Range, playerLayers);

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

    public IEnumerator DoAbility2()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("ability2");
            yield return new WaitForSecondsRealtime(0.7f);

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_ability2Transform.position, ability2Range, playerLayers);

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


    ////// RECIBE DAÑO //////
    public override void GetDamage(float dmg)
    {
        _animator.SetTrigger("hurt");
        HP = Mathf.Max(0, HP - dmg);
        ShowDamageDeal(Mathf.RoundToInt(dmg));
        if (HP <= (MaxHP / 2) && firstTimeMediaVida) { InvocarEnemigos1(); healthBarEnemy.gameObject.GetComponent<RawImage>().color = Color.yellow; _animator.SetBool("fury", true); }
        if (HP <= (MaxHP / 5) && firstTimePocaVida) { InvocarEnemigos2(); healthBarEnemy.gameObject.GetComponent<RawImage>().color = Color.magenta; }

        if (HP <= 0) { StopAllCoroutines(); moving = true; _animator.SetBool("isDead", true); StartCoroutine(Die()); } else { _animator.SetTrigger("hurt"); }
    }

}

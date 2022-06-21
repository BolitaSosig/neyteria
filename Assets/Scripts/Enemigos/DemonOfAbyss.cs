using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonOfAbyss : ElementalWater
{
    public GameObject slimeBossPrefab;
    protected bool PrimeraVezMuerto = true;
    protected override void DoAttacksAndAbilities()
    {
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility()); }
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility1()); }
        if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility2()); }

    }

    protected override IEnumerator DoAbility()
    {
        if (!attacking)
        {
            attacking = true;



            _animator.SetTrigger("ability");
            yield return new WaitForSecondsRealtime(0.7f);
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

    protected override IEnumerator DoAbility2()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("ability2");

            notDying = false;
            yield return new WaitForSecondsRealtime(0.7f);
            notDying = true;

            attacking = false;
        }
    }

    public override IEnumerator Die()
    {
        if (PrimeraVezMuerto) {

            PrimeraVezMuerto = false;

            _animator.SetTrigger("dead");
            _canvasTranform.gameObject.SetActive(false);

            yield return new WaitForSecondsRealtime(0.5f);

            GameObject SlimeBoss;
            SlimeBoss = Instantiate(slimeBossPrefab, transform.position, Quaternion.identity);

            //FindObjectOfType<PlayerController>().SendMessageUpwards("EnemyDefeated", this);
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;
            _rigidbody2D.gravityScale = 0f;
            _audioSource.SetActive(false);
            lookRadius = -1f;     

            //slimeBoss.SetActive(true);

            yield return new WaitForSecondsRealtime(1f);

            InvocarEnemigos2();

            this.enabled = false;
        }

    }


}

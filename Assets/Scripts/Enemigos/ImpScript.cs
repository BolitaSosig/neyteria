using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class ImpScript : NightBorneController
{
    //Revive
    float seRevive = 3f;

    public override IEnumerator Die()
    {
        float grav = _rigidbody2D.gravityScale;

        _animator.SetTrigger("dead");
        _boxCollider2D.enabled = false;
        _polygonCollider2D.enabled = false;
        _rigidbody2D.gravityScale = 0f;
        yield return new WaitForSecondsRealtime(0.7f);

        if (Random.Range(0f, 10f) >= seRevive)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            _boxCollider2D.enabled = true;
            _polygonCollider2D.enabled = true;
            _rigidbody2D.gravityScale = grav;
            HP = 0.5f * MaxHP;
            notDying = true;

            _animator.SetTrigger("revive");
        }

        else
        {
            FindObjectOfType<PlayerController>().SendMessageUpwards("EnemyDefeated", this);
            if (drop != null) drop.GetDrops(transform, itemOrbPrefab);
            Destroy(gameObject);
        }

    }
}

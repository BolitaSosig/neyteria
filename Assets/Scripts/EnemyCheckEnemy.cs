using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckEnemy : Enemigo
{
    [SerializeField] protected bool invertMovementPerseguir = true;

    [SerializeField] protected float lookRadius = 10f;
    [SerializeField] protected Transform target;

    protected override void Start()
    {
        base.Start();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    protected override void Update()
    {
        base.Update();
        if (HP > 0 && !pause) CheckEnemy();
    }

    protected virtual void CheckEnemy()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            //StopCoroutine(Moverse()); //moving = false; //StopAllCoroutines();
            DoAttacksAndAbilities();

            //Debug.Log(distance);
            // Move towards the target

            float inv = invertMovementPerseguir == true ? -1 : 1;

            if (transform.position.x <= target.position.x - 0.5f)
            {
                transform.localScale = new Vector2(inv * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(inv * Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(MovSpeed, _rigidbody2D.velocity.y);
            }
            else if (transform.position.x > target.position.x + 0.5f)
            {
                transform.localScale = new Vector2(inv * -Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(inv * -Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(-MovSpeed, _rigidbody2D.velocity.y);
            }

        }

        else
        { //_rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            //StartCoroutine(Moverse());
        }

        _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator*/

    }

    protected virtual void DoAttacksAndAbilities()
    {
        ;
    }
}

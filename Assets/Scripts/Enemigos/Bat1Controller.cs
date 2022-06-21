using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class Bat1Controller : ArcherController
{

    public bool followEnemy = false;


    protected override bool checkBorderPlatform()
    {
        return false;
    }

    public override IEnumerator DoAttack()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("attack");
            yield return new WaitForSecondsRealtime(0.5f);

            Vector3 direction = target.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);

            GameObject newBullet;
            newBullet = Instantiate(projectile, _shootTransform.position, Quaternion.identity);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            //newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.localScale.x * shootForce, 0);
            newBullet.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            newBullet.GetComponent<BatProjectileScript>().gunDMG = Attack;
            newBullet.GetComponent<BatProjectileScript>().follow = followEnemy;
            Destroy(newBullet, 2);

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

            attacking = false;
        }
    }

    protected override void CheckEnemy()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            //StopCoroutine(Moverse()); //moving = false; //StopAllCoroutines();
            if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }

            //Debug.Log(distance);
            // Move towards the target

            // Modifcar la 'x' para ponerse encima
            /*if (transform.position.x <= target.position.x - 0.5f)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(-Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(MovSpeed, _rigidbody2D.velocity.y);
            }
            else if (transform.position.x > target.position.x + 0.5f)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(-MovSpeed, _rigidbody2D.velocity.y);
            }
            */

            // Modifcar la 'x' según convenga

            if (transform.position.x >= target.position.x)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);

                if (transform.position.x > target.position.x + (lookRadius * 0.7))             //Acercarse si estás muy derecha
                {
                    _rigidbody2D.velocity = new Vector2(-MovSpeed, _rigidbody2D.velocity.y);
                }
                else if (transform.position.x < target.position.x + (lookRadius * 0.3))       //Alejarse si estás muy cerca por derecha
                {
                    _rigidbody2D.velocity = new Vector2(MovSpeed, _rigidbody2D.velocity.y);
                }
                else                                                                          //En otro caso, la 'y' se queda quieta
                {
                    _rigidbody2D.velocity = new Vector2(-MovSpeed * 0.5f, _rigidbody2D.velocity.y);
                }

            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(-Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);

                if (transform.position.x < target.position.x - (lookRadius * 0.7))        //Acercarse si estás muy izquierda
                {
                    _rigidbody2D.velocity = new Vector2(MovSpeed, _rigidbody2D.velocity.y);
                }
                else if (transform.position.x > target.position.x - (lookRadius * 0.3))       //Alejarse si estás muy cerca por izquierda
                {
                    _rigidbody2D.velocity = new Vector2(-MovSpeed, _rigidbody2D.velocity.y);
                }
                else                                                                          //En otro caso, la 'y' se queda quieta
                {
                    _rigidbody2D.velocity = new Vector2(MovSpeed * 0.5f, _rigidbody2D.velocity.y);
                }
            }



            // Modificar la 'y' según convenga

            if (transform.position.y >= target.position.y)
            {
                if (transform.position.y > target.position.y + (lookRadius * 0.7))             //Acercarse si estás muy arriba
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -MovSpeed);
                }
                else if (transform.position.y < target.position.y + (lookRadius * 0.3))       //Alejarse si estás muy cerca por arriba
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, MovSpeed);
                }
                else                                                                          //En otro caso, la 'y' se queda quieta
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -MovSpeed * 0.5f);
                }

            }
            else if (transform.position.y < target.position.y)
            {
                if (transform.position.y < target.position.y - (lookRadius * 0.7))        //Acercarse si estás muy abajo
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, MovSpeed);
                }
                else if (transform.position.y > target.position.y - (lookRadius * 0.3))       //Alejarse si estás muy cerca por abajo
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -MovSpeed);
                }
                else                                                                          //En otro caso, la 'y' se queda quieta
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, MovSpeed * 0.5f);
                }
            }
            



        }

        else
        { //_rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            //StartCoroutine(Moverse());
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        }

        _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator*/

    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius * 0.7f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius * 0.3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyFlyScript : NightBorneController
{
    protected override void CheckEnemy()
    {
        base.CheckEnemy();

        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            // Move towards the target

            // Modifcar la 'x' para ponerse encima
            if (transform.position.y <= target.position.y + 0.5f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, MovSpeed);
            }
            else if (transform.position.y > target.position.y + 1.5f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -MovSpeed);
            }


        }

        else { _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0); }
    }

    protected override bool checkBorderPlatform()
    {
        return false;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    public bool move = false;
    Rigidbody2D _rigidbody2D;
    public float MOV_SPEED = 2;
    public float Segs = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        move = true;
        StartCoroutine(Moverse());
    }

    public void InvertMove()
    {
        move = !move;
    }

    protected IEnumerator Moverse()
    {
        while (move)
        {
            //_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Elevator moviendose");

            int dir = 1;
            float cont = 0;
            while (cont < Segs * 10f / MOV_SPEED)
            {
                _rigidbody2D.velocity = new Vector2(0, MOV_SPEED * dir); // desplazamiento del personaje
                cont++;
                yield return new WaitForSecondsRealtime(0.1f);
            }

            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y); // quieto el personaje
            dir = -dir;
            yield return new WaitForSecondsRealtime(1f);
        }

        //_rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

    }




}

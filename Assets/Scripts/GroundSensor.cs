using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    PlayerController _playerScript;

    void Awake()
    {
        _playerScript = GetComponentInParent<PlayerController>();
    }

    public bool isGrounded;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }

        if(collision.gameObject.layer == 7)
        {
            //Destroy(collision.gameObject);
            //Goomba _enemyScript  = collision.gameObject.GetComponent<Goomba>(); 
            //_enemyScript.TakeDamage();

            _playerScript.Bounce();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = false;
        }
    }
}
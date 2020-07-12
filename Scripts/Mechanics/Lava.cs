using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        if(collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().TakeDamage(30);
        }
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(50);
        }
    }
}

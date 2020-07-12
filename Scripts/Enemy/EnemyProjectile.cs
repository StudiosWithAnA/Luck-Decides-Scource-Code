using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    public int Damage;

    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 difference = player.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().TakeDamage(Damage);
            }
            if (!collision.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
    }
}
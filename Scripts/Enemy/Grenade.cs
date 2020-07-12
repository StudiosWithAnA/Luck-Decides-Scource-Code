using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grenade : MonoBehaviour
{
    private AudioSource audioSource;
    float TimeToExplode = 2.5f;
    public int Damage;
    [SerializeField] private GameObject Explosion;

   public static void Create(Transform granade, Vector3 pos, Vector3 target, int Damage)
   {
        Grenade grenade =Instantiate(granade, pos, Quaternion.identity).GetComponent<Grenade>();
        grenade.Setup(target, Damage);
   }

    void Setup(Vector3 target,int Damage)
    {
        Vector3 moveDis = (target - transform.position).normalized;
        float moveSpeed = 15f;
        gameObject.GetComponent<Rigidbody2D>().velocity = moveDis * moveSpeed;
        transform.localEulerAngles = new Vector3(0,0, UtilsClass.GetAngleFromVector(moveDis));
        this.Damage = Damage;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("DestroyThis", TimeToExplode);
    }

    void DestroyThis()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        audioSource.Play();
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(Damage);
            DestroyThis();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private Player player;
    public int UpgradeAmount;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    
    void IncreaseHealth()
    {
        if (player.health < player.MaxHealth)
        {
            player.health += UpgradeAmount;
            if(player.health > player.MaxHealth)
            {
                player.health = player.MaxHealth;
            }
            audioSource.Play();
            Destroy(gameObject, 0.15f) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        if(collision.CompareTag("Player"))
        {
            IncreaseHealth();
        }
        if(collision.CompareTag("Item"))
        {
            Destroy(collision);
        }
    }
}

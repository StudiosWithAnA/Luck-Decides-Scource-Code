using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int health;
    [HideInInspector] public int MaxHealth;
    public TextMeshProUGUI healthDis;

    public int Kills;
    public TextMeshProUGUI killDis;

    public float Timer;
    public TextMeshProUGUI timeDis;

    public AudioSource hit;

    Manager manager;

    public CamShake CS;

    [SerializeField] private GameObject overScreen;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        MaxHealth = health;
    }

    
    void Update()
    {
        healthDis.SetText("HP: " + health);
        killDis.SetText("Kills: " + Kills);
        Timer += Time.deltaTime;
        int TimerInt = Mathf.RoundToInt(Timer);
        timeDis.SetText("Time: " +TimerInt);

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        overScreen.SetActive(true);
        manager.isOver = true;
        CS.BigShake();
        if(Kills > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", Kills);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int Damage)
    {
        hit.Play();
        health -= Damage;
    }
}

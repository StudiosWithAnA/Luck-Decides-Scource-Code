using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeRules : MonoBehaviour
{
    public string ruleName;
    private PlayerController player;
    private Weapon weapon;

    public float startTimeBtwChange;
    private float timeBtwChange;

    [SerializeField] private int rando;
    public TextMeshProUGUI ruleDis;
    public TextMeshProUGUI timeDis;

    public Spawner spawner;

    Manager manager;

    public bool isTesting;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        ChangeRule();
    }

    
    void Update()
    {
        if (manager.isOver == true)
            return;

        if (timeBtwChange > 0)
            timeBtwChange -= Time.deltaTime;

        int timeBtwChangeInt = Mathf.RoundToInt(timeBtwChange);
        timeDis.SetText("Time Till Change: " + timeBtwChangeInt);

        if (timeBtwChange <= 0)
            ChangeRule();
    }

    void ChangeRule()
    {
        if(!isTesting)
        rando = Random.Range(0, 5);


        switch(rando)
        {
            case 0:
                player.MessUp = -1;
                weapon.RecoilIsActive = false;
                spawner.spawning = true;
                ruleName = "Controls Are Flipped";
                break;
            case 1:
                player.MessUp = 1;
                weapon.RecoilIsActive = true;
                spawner.spawning = true;
                ruleName = "Uncontrollable Recoil";
                break;
            case 2:
                ruleName = "You Got Lucky This Time";
                player.MessUp = 1;
                spawner.spawning = true;
                weapon.RecoilIsActive = false;
                break;
            case 3:
                player.MessUp = -1;
                weapon.RecoilIsActive = true;
                spawner.spawning = true;
                ruleName = "Bad Luck";
                break;
            case 4:
                player.MessUp = 1;
                spawner.spawning = false;
                weapon.RecoilIsActive = false;
                ruleName = "Item Spawning is Off";
                break;
        }
        ruleDis.SetText(ruleName);
        timeBtwChange = startTimeBtwChange;
    }
}

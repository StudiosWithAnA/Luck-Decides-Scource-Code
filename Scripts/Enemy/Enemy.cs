using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int health;
    private Transform playerPos;

    public float AttackRange;
    public bool isAttacking;

    private float DisBtw;
    private float dis;

    public GameObject projectile;
    public float startTimeBtwShots;
    private float timeBtwShots;

    public float speed;
    public float nextWayPointDistance;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath;

    public int Damage;

    Seeker seeker;
    Rigidbody2D rb;

    public Spawner spawner;

    public Player player;

    public AudioSource audioSource;
    public AudioSource hit;

    Manager manager;

    private CamShake CS;

    public GameObject gunObject;
    public Transform muzzle;
    float rotZ;

    [SerializeField] private Transform pfGrenade;
    public EnemyType type;
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        CS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>();
        timeBtwShots = startTimeBtwShots;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone());
        seeker.StartPath(rb.position, playerPos.position, OnPathComplete);
    }

    void Update()
    {
        if (manager.isOver == true)
            return;

        if (playerPos == null)
            return;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(transform.position.x < min.x || transform.position.x > max.x)
        {
            Die();
        }

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        DisBtw = transform.position.x - playerPos.position.x;
        dis = Vector2.Distance(transform.position, playerPos.position);

        if(dis > AttackRange)
            isAttacking = false;
        else
            isAttacking = true;


        if(health <= 0)
        {
            Die();
        }

        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (isAttacking)
        {
            if(timeBtwShots <= 0)
            {
                Shoot();
            }
        }

        Vector3 difference = playerPos.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gunObject.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (rotZ < -90 || rotZ > 90)
        {
            gunObject.transform.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            gunObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    private void FixedUpdate()
    {
        if (manager.isOver)
            return;

        if (isAttacking == false)
        {
            if (path == null)
                return;

            if (currentWayPoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if(distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }
        }
    }

    void Die()
    {
        spawner.enemies.Remove(this);
        player.Kills += 1;
        CS.Shake();
        Destroy(gameObject);
    }

    public void TakeDamage(int Damage)
    {
        hit.Play();
        health -= Damage;
    }

    void Shoot()
    {
        if(type == EnemyType.Normal)
        {
            GameObject projectileObj = Instantiate(projectile, muzzle.position, Quaternion.identity);
            EnemyProjectile projectileTransform = projectileObj.GetComponent<EnemyProjectile>();
            projectileTransform.Damage = Damage;
        }

        if(type == EnemyType.Grenade)
        {
            Grenade.Create(pfGrenade, muzzle.position, playerPos.position, Damage);
        }

        audioSource.Play();

        timeBtwShots = startTimeBtwShots;
    }
}
public enum EnemyType
{
    Normal, 
    Grenade
}
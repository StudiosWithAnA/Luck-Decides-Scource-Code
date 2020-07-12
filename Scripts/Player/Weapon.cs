using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    [SerializeField]private Transform muzzle;
    public float Distance;
    public LayerMask shootWut;
    public int Damage;

    private float timeBtwShots;
    public float totalTimeBtwShots;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject projectile;

    [SerializeField] private CamShake CS;

    private float RecoilForce;
    private PlayerController playerController;

    public bool RecoilIsActive;

    [SerializeField]float rotZ;
    [SerializeField] private GameObject pfDamagePopup;

    private int Critical;

    public int TillCritical;
    private int Fired;
    bool isCritical;
    void Start()
    {
        TillCritical = Random.Range(4, 6);
        Critical = Damage * 2;
        timeBtwShots = totalTimeBtwShots;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        if(timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }

        if(Fired == TillCritical)
        {
            isCritical = true;
        }
        else
        {
            isCritical = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (rotZ < -90 || rotZ > 90)
        {
            RecoilForce = 4;
            transform.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            RecoilForce = -4;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    void Shoot()
    {
        if (timeBtwShots > 0)
            return;

        RaycastHit2D info = Physics2D.Raycast(muzzle.position, transform.right, Distance, shootWut);
        GameObject obj = Instantiate(projectile, muzzle.position, Quaternion.identity);
        Bullet bullet = obj.GetComponent<Bullet>();
        CS.Shake();
        if(offset == 0)
        {
            bullet.offset = -90f;
        }
        else
        {
            bullet.offset = 90f;
        }

        audioSource.Play();
        if(info.collider != null)
        {
            if(!info.collider.CompareTag("Environment"))
            {
                Enemy enemy = info.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Vector3 popUp = info.collider.gameObject.transform.position;
                    if(!isCritical)
                    {
                        enemy.TakeDamage(Damage);
                        DamagePopUp.DamagePop(pfDamagePopup, "Hit", popUp);
                    }
                    if (isCritical)
                    {
                        enemy.TakeDamage(Critical);
                        DamagePopUp.DamagePop(pfDamagePopup, "Critical", popUp);
                    }
                }
            }
        }
        if(isCritical)
        {
            Fired = 0;
            TillCritical = Random.Range(4, 6);
        }
        if(RecoilIsActive)
        {
            playerController.KnockBack(10f, new Vector2(1f, 0f), RecoilForce);
        }
        Fired++;
        timeBtwShots = totalTimeBtwShots;
    }
}

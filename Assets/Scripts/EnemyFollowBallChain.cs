using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowBallChain : MonoBehaviour
{
    [SerializeField]
    private float health;

    private bool alive = true;
    public float speed;
    private Transform playerPos;
    private Animator anim;

    private float repelRange = .5f;
    private List<Rigidbody2D> EnemyRBs;
    private Rigidbody2D rb;
    private bool hit = true;
    private bool shooting = false;
    private bool moving = true;

    public Transform bulletPos;
    public GameObject bullet;

    void Start()
    {
        //StartCoroutine(Shoot());
    }

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        if (EnemyRBs == null)
        {
            EnemyRBs = new List<Rigidbody2D>();
        }

        EnemyRBs.Add(rb);
    }

    private void OnDestroy()
    {
        EnemyRBs.Remove(rb);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > .4f && hit && alive && !shooting && moving)
        {
            anim.SetBool("isShooting", false);
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else
        {
            moving = false;
            shooting = true;
            if (shooting)
                StartCoroutine(startShooting());
        }
        if (!shooting)
        {
            if (playerPos.position.x > transform.position.x)
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            else
                transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //Rotation();
        }
    }

    void Rotation()
    {
        Vector2 direction = (playerPos.gameObject.GetComponent<Rigidbody2D>().position - rb.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (playerPos.position.x < transform.position.x)
            rb.rotation = angle + 178;
        else
            rb.rotation = angle - 2;
    }

    IEnumerator startShooting()
    {
        Rotation();
        yield return new WaitForSeconds(.65f);
        anim.SetBool("isShooting", true);
        Instantiate(bullet, bulletPos.position, transform.rotation);
        yield return new WaitForSeconds(.65f);
        shooting = false;
        moving = true;

    }

    void FixedUpdate()
    {
        Vector2 repelForce = Vector2.zero;
        foreach (Rigidbody2D enemy in EnemyRBs)
        {
            if (enemy == rb && enemy.gameObject.tag == "Enemy")
                continue;
            if (Vector2.Distance(enemy.position, rb.position) <= repelRange)
            {
                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repelForce += repelDir;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            anim.SetBool("isDead", true);
            StartCoroutine(Dying());
            Destroy(target.gameObject);
            ScoreManager.instance.AddPoint();
        }
    }

    IEnumerator Dying()
    {
        Ammo.instance.AddBullet();
        alive = false;
        yield return new WaitForSeconds(.35f);
        Destroy(gameObject);
    }

    /*IEnumerator Shoot()
    {
        if (inRange)
            Instantiate(bullet, bulletPos.position, transform.rotation);
        yield return new WaitForSeconds(.65f);
        StartCoroutine(Shoot());
    }*/
}

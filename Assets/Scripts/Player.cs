using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    private Rigidbody2D myBody;
    public float speed;
    private Vector2 moveVelocity;
    private Animator anim;
    //private Vector2 rollDirection;
    
    [SerializeField]
    private int health;

    private bool hit = true;
    private bool firing = false;
    private bool rolling = false;
    /*
    private State state;
    private enum State
    {
        Normal, 
        DodgeRollSliding,
    }*/

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //StartCoroutine(TrackDirection());
        //state = State.Normal;
    }

    private void Update()
    {
        if(health > 0)
            Rotation();
        if (Input.GetMouseButtonDown(0) && !firing && Ammo.instance.GetBullets() > 0 && !rolling)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            Ammo.instance.RemoveBullet();
            firing = true;
            StartCoroutine(FireCooldown());
        }/*
        if (Input.GetMouseButtonDown(1) && !rolling)
        {
            rolling = true;
            hit = false;
            Vector2 rollDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //for (int x = 0; x < 100; x++)
            //{
            //moveVelocity = rollDirection.normalized * speed;
            //for (int x = 0; x < 1000; x++)
            transform.position = Vector2.MoveTowards(transform.position, rollDirection * 1000, speed * Time.deltaTime);
            //}
            rolling = false;
            hit = true;
        }*/
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -4.827f, 4.827f), Mathf.Clamp(transform.position.y, -2.867f, 2.661f));
    }
    /*
    IEnumerator Roll()
    { 
        transform.position += rollDirection * 1f * Time.deltaTime;
        yield return new WaitForSeconds(.1f);
    }
    
    IEnumerator RollLength()
    {
        yield return new WaitForSeconds(.35f);
        
    }*/

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        firing = false;
    }

    void FixedUpdate()
    {
        if(health > 0 && !rolling)
            Movement();
    }

    void Rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        if (angle <= 180 && angle >= 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    /*
    IEnumerator TrackDirection()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            rollDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        yield return new WaitForSeconds(.1f);
        StartCoroutine(TrackDirection());
    }*/
    
    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        myBody.MovePosition(myBody.position + moveVelocity * Time.fixedDeltaTime);
    }
    /*
    void Roll()
    {
        StartCoroutine(RollLength());
        transform.position += rollDirection * 3f * Time.deltaTime;
    }*/

    IEnumerator HitBoxOff()
    {
        hit = false;
        anim.SetBool("isDamaged", true);
        yield return new WaitForSeconds(1.5f);
        hit = true;
        anim.SetBool("isDamaged", false);
    }
    
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
            if (hit)
            {
                StartCoroutine(HitBoxOff());
                health--;
                Destroy(GameObject.Find("HealthHolder").transform.GetChild(0).gameObject);
                if(health < 1)
                {
                    StartCoroutine(Death());
                }
            }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

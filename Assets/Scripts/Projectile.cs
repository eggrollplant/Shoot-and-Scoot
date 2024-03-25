using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 4;
    private Vector2 direction;
    private Vector2 temp;

    void Start()
    {
        direction = GameObject.Find("Direction").transform.position;
        transform.position = GameObject.Find("FirePoint").transform.position;
        Rotate();
    }

    void Rotate()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        if (transform.position.Equals(temp))
        {
            Destroy(gameObject);
        }
        temp = transform.position;

    }
}

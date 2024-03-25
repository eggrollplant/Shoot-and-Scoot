using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Image ammo;
    private Sprite ammoSprite;
    private GameObject ammoHolder;
    public static Ammo instance;
    int bullets = 10;

    void Awake()
    {
        ammoHolder = GameObject.Find("AmmoHolder");
        ammo.sprite = ammoSprite;
        instance = this;
    }

    public void AddBullet()
    {
        bullets += 1;
        var newAmmo = Instantiate(ammo);
        //newAmmo.image.sprtie = ammoSprite;
        //newAmmo.transform.parent = ammoHolder.transform;
        newAmmo.transform.SetParent(ammoHolder.transform);
    }

    public void RemoveBullet()
    {
        bullets -= 1;
        Destroy(GameObject.Find("AmmoHolder").transform.GetChild(0).gameObject);
    }

    public int GetBullets()
    {
        return bullets;
    }
}

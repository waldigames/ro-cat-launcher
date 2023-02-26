using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject GunObj, PlayerHead, PlayerBody, CatHead, CatBody;
    [Header("Rotation")]
    public float rotationSpeed = 10.0f;
    public float rotMultiplier = 1; 
    public float mapHeight;

    [Header("Jump")]
    public bool isReversing = false;

    private float currentAngle = 0.0f;
    [Header("Shooting")]
    //shooting
    public float ReloadSpeed, ShootDelay, ExplosionForce, BulletSpeed;
    float _reloadTimer, _shootdelay;
    public bool isReloading, canSHoot;
    public GameObject bulletPrfb, bulletExplosion;
    public List<GameObject> showBullets;
    GameObject lastBullet;
    public List<Transform> FirePoints;
    int currFirePoint = 0;
    public PhysicMaterial nPmat, bPmat;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(PlayerPrefs.HasKey("difficulty")){ rotMultiplier = PlayerPrefs.GetInt("difficulty");}
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        if(Input.GetMouseButtonDown(1)){Detonate();}
        if(_shootdelay < ShootDelay){_shootdelay += Time.deltaTime;}
        else if(_shootdelay >= ShootDelay && Input.GetMouseButtonDown(0) && !isReloading && canSHoot){Shoot(); _shootdelay = 0;}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isReversing = !isReversing;
        }
    }
    void FixedUpdate()
    {
        float xVelocity = Mathf.Clamp(rb.velocity.x, -9, 9);
        float yVelocity = Mathf.Clamp(rb.velocity.y, -9, 9);
        rb.velocity = new Vector3(xVelocity, yVelocity, rb.velocity.z);
    }
    public void RotateGun()
    {
        float rotationDirection = isReversing ? -1.0f : 1.0f;
        currentAngle += (rotationSpeed * rotMultiplier) * rotationDirection * Time.deltaTime;
        GunObj.transform.rotation = Quaternion.Euler(GunObj.transform.rotation.x, GunObj.transform.rotation.y, currentAngle);
    }
    void Shoot()
    {
        GameObject x = Instantiate(bulletPrfb, FirePoints[currFirePoint].position, FirePoints[currFirePoint].transform.rotation);
        lastBullet = x;
        x.transform.localScale = new Vector3(.1f, .4f, .1f);
        x.GetComponent<Rigidbody>().AddForce(((Quaternion)x.transform.rotation * Vector3.up) * BulletSpeed, ForceMode.Impulse);
        showBullets[0].SetActive(false);
        if(currFirePoint >= FirePoints.Count - 1)
        {
            currFirePoint = 0;
        }
        else currFirePoint++;
        StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(ReloadSpeed / showBullets.Count);
        showBullets[0].SetActive(true);
        isReloading = false;
    }
    void Detonate()
    {
        if(lastBullet != null)
        {
            lastBullet.GetComponent<Bullet>().DetonateEarly();
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        RemoveBoncy();
    }
    public void MakeBoncy()
    {
        GetComponent<Collider>().material = bPmat;
    }
    public void RemoveBoncy()
    {
        GetComponent<Collider>().material = nPmat;
    }
    public void SetDifficulty(float v)
    {
        rotMultiplier = v;
        PlayerPrefs.SetInt("difficulty", (int)v);
        if(v == 4)
        {
            TurnOnCat();
        }
        else
        {
            TurnOffCat();
        }
    }
    void TurnOnCat()
    {
        PlayerBody.SetActive(false);
        PlayerHead.SetActive(false);
        CatBody.SetActive(true);
        CatHead.SetActive(true);
    }
    void TurnOffCat()
    {
        PlayerBody.SetActive(true);
        PlayerHead.SetActive(true);
        CatBody.SetActive(false);
        CatHead.SetActive(false);
    }
}

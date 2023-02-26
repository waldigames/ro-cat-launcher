using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    ShockwaveEmitter se;
    PlayerController pc;
    void Start()
    {
        se = FindObjectOfType<ShockwaveEmitter>();
        pc = FindObjectOfType<PlayerController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            se.TriggerShockwave(transform.position);
            Destroy(Instantiate(pc.bulletExplosion, transform.position, Quaternion.identity), 2);
            Destroy(gameObject);
        }
    }
    public void DetonateEarly()
    {
        se.TriggerShockwave(transform.position);
        Destroy(Instantiate(pc.bulletExplosion, transform.position, Quaternion.identity), 2);
        Destroy(gameObject);
    }
}

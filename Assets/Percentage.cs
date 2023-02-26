using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Percentage : MonoBehaviour
{
    TMP_Text txt;
    GameObject player;
    public float MapHeight;
    void Start()
    {
        txt = GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(((player.transform.position.y / MapHeight) * 100.0f) > 0)
        {
            txt.text = ((player.transform.position.y / MapHeight) * 100.0f).ToString("f0") + "%";
        }
    }
}

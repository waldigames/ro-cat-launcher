using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Goal : MonoBehaviour
{
    public GameObject UI;
    public TMP_Text WinText;
    PlayerController pc;
    public string[] dMessages = new string[4];
    int DsBeaten;
    void Start()
    {
        pc = FindObjectOfType<PlayerController> ();
        pc.canSHoot = false;
        UI.SetActive(true);
        if(PlayerPrefs.HasKey("difficulty"))
        {
            pc.rotMultiplier = PlayerPrefs.GetInt("difficulty");
        }
        else
        {
            PlayerPrefs.SetInt("difficulty", 1);
        }
        if(PlayerPrefs.HasKey("maxDifficulty"))
        {
            DsBeaten = PlayerPrefs.GetInt("maxDifficulty");
        }
        else 
        {
            PlayerPrefs.SetInt("maxDifficulty", 0);
        }
        if(PlayerPrefs.HasKey("WinText"))
        {
            WinText.text = PlayerPrefs.GetString("WinText");
        }
        else
        {
            PlayerPrefs.SetString("WinText", "So far you've been too bad, to beat this game...");
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            UI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F4) && Input.GetKey(KeyCode.LeftAlt))
        {
            Debug.Log("Alt+F4 pressed!");
            ALtFfour();
            return;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Win();
        }
    }
    public void PlayGame()
    {
        UI.SetActive(false);
        pc.canSHoot = true;
    }
    void Win() 
    {
        pc.canSHoot = false;
        UI.SetActive(true);
        pc.gameObject.transform.position = Vector3.zero;
        pc.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(PlayerPrefs.GetInt("difficulty") > PlayerPrefs.GetInt("maxDifficulty"))
        {
            PlayerPrefs.SetInt("maxDifficulty", PlayerPrefs.GetInt("difficulty"));
        }
        PlayerPrefs.SetString("WinText", dMessages[PlayerPrefs.GetInt("maxDifficulty") - 1]);
        WinText.text = PlayerPrefs.GetString("WinText");
    }
    void ALtFfour()
    {
        pc.gameObject.transform.position = Vector3.zero;
    }
}

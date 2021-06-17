using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PannelManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject jokersPanel;
    public GameObject gameOverPanel;    
    public GameObject questionPanel;    
    public GameObject youWinPannel;
    public GameObject keepPlayingBtn;

    public bool _gameOver;
    //bool keepPlayin = false;
    bool freezegame = false;


    GameManager gameManager;
    QuestionManager questionManager;
    
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(true);
        youWinPannel.SetActive(true);
        optionsPanel.SetActive(true);
    }

    
    public void exit_btn()
    {
        SceneManager.LoadScene("Scenes/LoginLevel");

        PlayerPrefs.SetInt("Doğru Sayısı", 0); 
        PlayerPrefs.SetInt("Yanlış Sayısı", 0); 
        PlayerPrefs.SetInt("geçici_skor",0);
    }
    public void youWin()
    {
        questionPanel.SetActive(false);
        if (youWinPannel != null)
        {
            Animator animator = youWinPannel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }            
        }
        freezeTime();

    }
    public void gameOver()
    {
        youWinPannel.SetActive(false);
        _gameOver = true;
        if (gameOverPanel != null)
        {
            Animator animator = gameOverPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
        freezeTime();
    }

    public void options_btn()
    {
        gameOverPanel.SetActive(false);
        youWinPannel.SetActive(false);
        if (optionsPanel != null)
        {
            Animator animator = optionsPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
        freezeTime();        
    }
    public void backOptions_btn()
    {
        options_btn();
        gameOverPanel.SetActive(true);
        youWinPannel.SetActive(true);        
    }
    public void freezeTime()
    {
        freezegame = !freezegame;

        if (freezegame == true)
        {
            Time.timeScale = 0.0f;
            Debug.Log("Zaman durdu!");

        }
        else
        {
            Time.timeScale = 1.0f;
            Debug.Log("Zaman devam ediyor!");

        }
    }
    public void XButton()
    {
        jokersButton();
    }
    public void jokersButton()
    {
        if (jokersPanel != null)
        {
            Animator animator = jokersPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }
   
}

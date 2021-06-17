using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour { 

         public GameObject mainmanupanel;
         public GameObject settingspanel;
         public GameObject cikispanel;

         public GameObject background;
         //public GameObject filter;

          public GameObject logo;


    QuestionManager questionManager;
    private void Awake()
    {
        Time.timeScale = 1f;
        questionManager = Object.FindObjectOfType<QuestionManager>();

        //settingspanel.SetActive(false);
        //mainmanupanel.SetActive(true);
    }
    void Start()
    {
        settingspanel.SetActive(false);
        Application.targetFrameRate = 30;
    }


    public void GameSettings()
    {
        settingspanel.SetActive(true);
        mainmanupanel.SetActive(false);

        background.SetActive(false);
        //filter.SetActive(false);
        logo.SetActive(false);
        Debug.Log("Ayarlar butonu");

    }
    public void return_main_menu()
    {
        mainmanupanel.SetActive(true);

        background.SetActive(true);
        //filter.SetActive(true);
        logo.SetActive(true);
        settingspanel.SetActive(false);
        cikispanel.SetActive(false);

        Debug.Log("Return butonu");
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("Doğru Sayısı", 0);
        PlayerPrefs.SetInt("Yanlış Sayısı", 0);
        Application.Quit();

        Debug.Log("Oyun Kapandı!");
    }

    public void quit_panel()
    {
        cikispanel.SetActive(true);
    }
}

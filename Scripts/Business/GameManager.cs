using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text correctText, answerText, trueText, falseText;
    [SerializeField] private Button Button1, Button2, Button3, Button4;
    [SerializeField] private bool joker_3 = false;
    [SerializeField] private float delay = 1f;



    public GameObject cift_joker_buton;
    public GameObject can_joker_buton;
    public GameObject zaman_joker_buton;
    public GameObject jokersPannel;
    public GameObject quesionPanel;
    public GameObject nextQuestion_btn;

    
    public int countTrue = 0, countFalse = 0;
    public int altin_sayisi = 0;
    public int skor = 0;
    public int second;
    public Text altin;
    public Text timeText;
    public Image can_bari;
    public float can = 3.0f;
    public float guncel_can = 3.0f;

    private string dogruCevap;
    private int cevapver_count = 0;
    private bool buton1 = false;
    private bool buton2 = false;
    private bool buton3 = false;
    private bool buton4 = false;    
    private bool joker_bir = false;
    private bool joker_2 = false;   

    QuestionManager questionManager;
    PannelManager gameOverControl;
    
    public TextMeshProUGUI skorYazi; //pause paneldeki  skor
    public TextMeshProUGUI yüksekskorYazi; //pause paneldeki yüksek skor
    public TextMeshProUGUI skor_panel; //youwin paneldeki yüksek skor
    public TextMeshProUGUI skor_bitti_panel; // youwin paneldeki skor
    public TextMeshProUGUI yuksek_skor_text; //gameover paneldeki skor 
    public TextMeshProUGUI yuksek_skor_pitti_panel; //gameover paneldeki yüksek skor
    public TextMeshProUGUI dogru_sayi_durdurma_panel; //pause paneldeki doğru 
    public TextMeshProUGUI yanlis_sayi_durdurma_panel; //pause paneldeki yanlış 
    public TextMeshProUGUI dogru_sayi_bitti_panel; //gameover paneldeki doğru 
    public TextMeshProUGUI yanlis_sayi_bitti_panel; // gameover paneldeki yanlış
    public TextMeshProUGUI canHakki; 



    void Awake()
    {
        gameOverControl = Object.FindObjectOfType<PannelManager>();
        questionManager = Object.FindObjectOfType<QuestionManager>();
    }


    void Start()
    {
        Time.timeScale = 1.0f;
        nextQuestion_btn.SetActive(false);
        yuksek_skor_pitti_panel.text = PlayerPrefs.GetInt("Puan") + "";
        skor_panel.text = PlayerPrefs.GetInt("Puan") + ""; // youwin panelindeki high score
        yüksekskorYazi.text = PlayerPrefs.GetInt("Puan") + "";// pause panaldeki yüksek skor
        altin.text = PlayerPrefs.GetInt("altin") + "";

        skor_bitti_panel.text = PlayerPrefs.GetInt("geçici_skor").ToString();
        yuksek_skor_text.text = PlayerPrefs.GetInt("geçici_skor").ToString();

        guncel_can = PlayerPrefs.GetFloat("Güncel can"); // SONRADAN
        

        dogru_sayi_durdurma_panel.text = PlayerPrefs.GetInt("Doğru Sayısı").ToString();  //countTrue.ToString(); // (skor / 10).ToString();
        dogru_sayi_bitti_panel.text = PlayerPrefs.GetInt("Doğru Sayısı").ToString();
        trueText.text = PlayerPrefs.GetInt("Doğru Sayısı").ToString();

        yanlis_sayi_durdurma_panel.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString(); //countFalse.ToString();
        yanlis_sayi_bitti_panel.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString();
        falseText.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString();

        if (PlayerPrefs.GetFloat("Güncel can")<=0f)
        {
            gameOverControl.gameOver();
        }

        //PlayerPrefs.SetInt("altin",0);
    }

    private void Update()
    {
        TimerSay();        
    }
    public void TimerSay()
    {
        can_bari.fillAmount = guncel_can / can;
        canHakki.text = PlayerPrefs.GetFloat("Güncel can").ToString();
        questionManager.targetTime -= Time.deltaTime;
        second = Mathf.RoundToInt(questionManager.targetTime);
        timeText.text = second.ToString();
        if (second <= 0)
        {
            if (PlayerPrefs.GetInt("altin") >= 50)
            {
                altin_sayisi = PlayerPrefs.GetInt("altin") - 50;
                altin.text = altin_sayisi.ToString();
                PlayerPrefs.SetInt("altin", altin_sayisi);
            }

            countFalse = PlayerPrefs.GetInt("Yanlış Sayısı")+1;
            PlayerPrefs.SetInt("Yanlış Sayısı", countFalse);

            guncel_can--;
            PlayerPrefs.SetFloat("Güncel can", guncel_can); // SONRADAN
            can_bari.fillAmount = guncel_can / can;
            falseText.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString(); // countFalse.ToString();

            SceneManager.LoadScene(1);
            //StartCoroutine(NextQuestionDelay());
            if (questionManager.oyunBitti==true)
            {
                questionManager.oyunBitti = false;
                gameOverControl.exit_btn();
            }

            if (gameOverControl._gameOver==true)
            {
                gameOverControl._gameOver = false;
                gameOverControl.exit_btn();

            }
        }
        
    }
    IEnumerator NextQuestionDelay()
    {
        CalculateTrueFalse();
        questionManager.remove();
        Button1.GetComponent<Button>().interactable = false;
        Button2.GetComponent<Button>().interactable = false;
        Button3.GetComponent<Button>().interactable = false;
        Button4.GetComponent<Button>().interactable = false;
        PlayerPrefs.GetFloat("Güncel can", guncel_can);

        

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void iki_hak_buton()
    {
        
        if (PlayerPrefs.GetInt("altin") >= 1500)
        {
            joker_bir = true;
            cift_joker_buton.SetActive(false);
            gameOverControl.jokersButton();   //altin_sayisi -= 1500; 
            altin_sayisi = PlayerPrefs.GetInt("altin") - 1500;      // PlayerPrefs.GetInt("altin") - 1500;
            altin.text = altin_sayisi.ToString();
            Toast.Instance.Show("Çift Cevap Joker Hakkını Kullandınız ", 2f, Toast.ToastColor.Magenta);

            PlayerPrefs.SetInt("altin", altin_sayisi);            
        }
        else
        {
            Toast.Instance.Show("Altınınız Yetersiz.",2f,Toast.ToastColor.Magenta);
        }
       

    }
    public void Can_Joker_button()
    {        
        if (PlayerPrefs.GetInt("altin") >= 3500)
        {
            joker_2 = true;
            if (joker_2)
            {
                joker_2 = false;
                guncel_can = 3f;
                PlayerPrefs.SetFloat("Güncel can", guncel_can); // SONRADAN
                can_bari.fillAmount = 1;
                gameOverControl.jokersButton();
                can_joker_buton.SetActive(false);
            }            
            altin_sayisi = PlayerPrefs.GetInt("altin") - 3500;
            altin.text = altin_sayisi.ToString();
            Toast.Instance.Show("Can Doldurma Jokeri Kullandınız.", 2f, Toast.ToastColor.Green);
            PlayerPrefs.SetInt("altin", altin_sayisi);
        }
        else
        {
            Toast.Instance.Show("Altınınız Yetersiz.", 2f, Toast.ToastColor.Green);
        }
    }
    public void zaman_joker_button()
    {

        if (PlayerPrefs.GetInt("altin") >= 1000)
        {
            joker_3 = true;
            if (joker_3)
            {
                joker_3 = false;
                questionManager.targetTime = 30;
                zaman_joker_buton.SetActive(false);
            }
            
            altin_sayisi = PlayerPrefs.GetInt("altin") - 1000;
            altin.text = altin_sayisi.ToString();
            Toast.Instance.Show("Zaman Joker Hakkını Kullandınız ", 2f, Toast.ToastColor.Blue);

            PlayerPrefs.SetInt("altin", altin_sayisi);
            gameOverControl.jokersButton();
        }
        else
        {
            Toast.Instance.Show("Altınınız Yetersiz.", 2f, Toast.ToastColor.Blue);
        }

    }
    //Doğru ve yanlış hesaplama
    public void CalculateTrueFalse()
    {
        
        Buttons();
        nexQuestionControl();
        if (questionManager.playAgain == true)
        {
            countTrue = PlayerPrefs.GetInt("Doğru sayısı", countTrue) - PlayerPrefs.GetInt("Doğru sayısı", countTrue);
            PlayerPrefs.SetInt("Doğru sayısı", countTrue);
            countFalse = PlayerPrefs.GetInt("Yanlış sayısı", countFalse) - PlayerPrefs.GetInt("Yanlış sayısı", countFalse);
            PlayerPrefs.SetInt("Yanlış sayısı", countFalse);
        }
        if (answerText.text == questionManager.answerText.text)
        {
            dogruCevap = questionManager.trueText.text;
            if (correctText.text == dogruCevap)
            {

                questionManager.dogru_renk_degis(correctText.text);

                countTrue = PlayerPrefs.GetInt("Doğru Sayısı")+1;
                PlayerPrefs.SetInt("Doğru Sayısı",countTrue);
                trueText.text = PlayerPrefs.GetInt("Doğru Sayısı").ToString();  // countTrue.ToString();

                altin_sayisi = PlayerPrefs.GetInt("altin") + 100;
                altin.text = altin_sayisi.ToString();
                PlayerPrefs.SetInt("altin", altin_sayisi);

                skor = PlayerPrefs.GetInt("geçici_skor") + 10;
                PlayerPrefs.SetInt("geçici_skor",skor);
                skorYazi.text = skor.ToString();

                dogru_sayi_durdurma_panel.text = PlayerPrefs.GetInt("Doğru Sayısı").ToString();  //countTrue.ToString(); // (skor / 10).ToString();
                dogru_sayi_bitti_panel.text = PlayerPrefs.GetInt("Doğru Sayısı").ToString();  //countTrue.ToString();  //(skor / 10).ToString();
                yuksek_skor_text.text = PlayerPrefs.GetInt("geçici_skor").ToString(); //skor.ToString();
                skor_bitti_panel.text = PlayerPrefs.GetInt("geçici_skor").ToString(); // skor.ToString();

                if (skor > int.Parse(yüksekskorYazi.text))
                {
                    skor_panel.text = skor.ToString();
                    //PlayerPrefs.SetInt("Puan", skor);

                    yuksek_skor_pitti_panel.text = skor.ToString();
                    //PlayerPrefs.SetInt("Puan", skor);

                    yüksekskorYazi.text = skor.ToString();
                    PlayerPrefs.SetInt("Puan", skor);
                }
            }

            else
            {
                if (joker_bir == false)
                {

                    questionManager.yanlis_renk_degis(correctText.text);

                    countFalse = PlayerPrefs.GetInt("Yanlış Sayısı")+1;
                    PlayerPrefs.SetInt("Yanlış Sayısı", countFalse);
                    falseText.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString();  //countFalse.ToString();
                    guncel_can--;
                    PlayerPrefs.SetFloat("Güncel can", guncel_can); // SONRADAN
                    can_bari.fillAmount = guncel_can / can;
                    if (PlayerPrefs.GetInt("altin") >= 50)
                    {
                        altin_sayisi = PlayerPrefs.GetInt("altin") - 50;
                        altin.text = altin_sayisi.ToString();
                        PlayerPrefs.SetInt("altin", altin_sayisi);
                    }
                    

                    yanlis_sayi_durdurma_panel.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString(); //countFalse.ToString();
                    yanlis_sayi_bitti_panel.text = PlayerPrefs.GetInt("Yanlış Sayısı").ToString();  //countFalse.ToString();
                }

                if (cevapver_count == 0 && joker_bir == true)
                {
                    joker_bir = false;
                    cevapver_count++;
                    altin.text = altin_sayisi.ToString();

                    questionManager.yanlis_renk_degis(correctText.text);
                }
            }
        }
        if (can_bari.fillAmount == 0f) // Can bittiğinde gameOverPanel devreye girecek.
        {
            gameOverControl.gameOver();
        }
    }
    public void Buttons()
    {
        Button1.gameObject.SetActive(true);
        Button2.gameObject.SetActive(true);
        Button3.gameObject.SetActive(true);
        Button4.gameObject.SetActive(true);
    }
    //1,2,3 ve 4. butonların textlerini alma ve calculatetruefalse fonksiyonu aktif etme
    public void OnclickButton1()
    {
        correctText.text = Button1.GetComponentInChildren<Text>().text;        
        nextQuestion_btn.SetActive(true);
        buton1 = true;
         
    }
    public void OnclickButton2()
    {
        correctText.text = Button2.GetComponentInChildren<Text>().text;
        nextQuestion_btn.SetActive(true);
        buton2 = true;
         
    }
    public void OnclickButton3()
    {
        correctText.text = Button3.GetComponentInChildren<Text>().text;
        nextQuestion_btn.SetActive(true);
        buton3 = true;
         
    }
    public void OnclickButton4()
    {
        correctText.text = Button4.GetComponentInChildren<Text>().text;
        nextQuestion_btn.SetActive(true);
        buton4 = true;
         
    }
    public void nexQuestionControl()
    {
        nextQuestion_btn.SetActive(false);
        buton1 = false;
        buton2 = false;
        buton3 = false;
        buton4 = false;
    }
    public void nextQuestion()
    {
        if (buton1 == true) 
        {
            if (joker_bir == true)
            {
                joker_bir = false;
                if (correctText.text == dogruCevap)
                {
                    Debug.Log("Joker kullanmanıza rağmen doğru cevap verdiniz");

                    StartCoroutine(NextQuestionDelay());
                }
                else
                {
                    Debug.Log("Joker kullandınız, 1 cevap hakkınız daha var.");
                    Button1.gameObject.SetActive(false);
                }

            }
            else
            {
                StartCoroutine(NextQuestionDelay());
            }
        }
        else if (buton2 == true) 
        {
            if (joker_bir == true)
            {
                joker_bir = false;
                if (correctText.text == dogruCevap)
                {
                    Debug.Log("Joker kullanmanıza rağmen doğru cevap verdiniz");

                    StartCoroutine(NextQuestionDelay());
                }
                else
                {
                    Debug.Log("Joker kullandınız, 1 cevap hakkınız daha var.");
                    Button2.gameObject.SetActive(false);
                }

            }
            else
            {
                StartCoroutine(NextQuestionDelay());
            }

        }
        else if (buton3 == true)
        {
            if (joker_bir == true)
            {
                joker_bir = false;
                if (correctText.text == dogruCevap)
                {
                    Debug.Log("Joker kullanmanıza rağmen doğru cevap verdiniz");

                    StartCoroutine(NextQuestionDelay());
                }
                else
                {
                    Debug.Log("Joker kullandınız, 1 cevap hakkınız daha var.");
                    Button3.gameObject.SetActive(false);
                }

            }
            else
            {
                StartCoroutine(NextQuestionDelay());
            }
        }
        else if (buton4 == true)
        {
            if (joker_bir == true)
            {
                joker_bir = false;
                if (correctText.text == dogruCevap)
                {
                    Debug.Log("Joker kullanmanıza rağmen doğru cevap verdiniz. Tebrikler :)");

                    StartCoroutine(NextQuestionDelay());
                }
                else
                {
                    Debug.Log("Joker kullandınız, 1 cevap hakkınız daha var.");
                    Button4.gameObject.SetActive(false);
                }

            }
            else
            {
                StartCoroutine(NextQuestionDelay());
            }
        }
    }  
}

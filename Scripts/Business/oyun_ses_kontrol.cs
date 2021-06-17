using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class oyun_ses_kontrol : MonoBehaviour
{
    public AudioSource genel_ses;
    public Image ses_acik;
    public Image ses_kapali;
    public bool kontrol = true;
    private string ikonlar;
    void Start()
    {
        //Time.timeScale = 1f;
        //ses_acik.enabled = false;
        //ses_kapali.enabled = true;

        ikonlar = PlayerPrefs.GetString("Ses Kontrolü", kontrol.ToString());
        if (ikonlar == "True")
        {
            Ac();
        }
        else
        {
            Durdur();
        }
    }

    public void Ac()
    {
        genel_ses.Play();
        ses_acik.enabled = false;
        ses_kapali.enabled = true;
        kontrol = true;

        PlayerPrefs.SetString("Ses Kontrolü", kontrol.ToString());
    }

    public void Durdur()
    {
        genel_ses.Pause();
        ses_acik.enabled = true;
        ses_kapali.enabled = false;
        kontrol = false;

        PlayerPrefs.SetString("Ses Kontrolü", kontrol.ToString());
    }    
}

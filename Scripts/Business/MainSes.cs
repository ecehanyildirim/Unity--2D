using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSes : MonoBehaviour
{

    public AudioSource genel_ses;

    public bool kontrol;
    private string kontrol_stringi = "";
    // Start is called before the first frame update
    void Start()
    {
        kontrol_stringi = PlayerPrefs.GetString("Ses Kontrolü");

        if (kontrol_stringi == "True")
        {
            kontrol = true;
            Ac();
            Debug.Log("kontrol" + kontrol);
        }

        if (kontrol_stringi == "False")
        {
            kontrol = false;
            Durdur();
            Debug.Log("kontrol"+kontrol);
        }

    }

    public void Ac()
    {
        genel_ses.Play();
    }
    public void Durdur()
    {
        genel_ses.Pause();
    }

}

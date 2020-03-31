using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Kello : MonoBehaviour
{
    //Viisarit
    public GameObject sViisari;
    public GameObject mViisari;
    public GameObject tViisari;

    public TextMeshProUGUI digikello;

    string aikasi;
    public InputField customaika;

    void Update()
    {
        //Tietokoneen aika nyt
        DateTime aika = DateTime.Now;
        

        //aikayksiköt radiaaneiksi kelloa varten
        //Viisareiden liikkeiden pehmentämiseksi lisätään aiempi aikayksikkö
        float sRad = (float)((aika.Second + aika.Millisecond / 1000f) /60f) * -360f;

        //Liikuttaa viisareita
        sViisari.transform.eulerAngles = new Vector3(0, 0, sRad);


        float mRad = (float)((aika.Minute + aika.Second / 60f) / 60f) * -360f ;
        mViisari.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, mRad));

        float tRad = (float)((aika.Hour + aika.Minute / 60f) / 12f) * -360f;
        tViisari.transform.eulerAngles = new Vector3(0, 0, tRad);

        
        //lisää nollan digikelloon kun aikayksikkö on yksilukuinen

        string sDigi = Mathf.Floor(aika.Second).ToString("00");
        string mDigi = Mathf.Floor(aika.Minute).ToString("00");
        string tDigi = Mathf.Floor(aika.Hour).ToString("00");

        //sijoittaa ajan tekstikenttään

        digikello.text = tDigi + ":" + mDigi + ":" + sDigi;

    }



    void OmaAika()
    {
        aikasi = customaika.text;
        Debug.Log(aikasi);
    }
}

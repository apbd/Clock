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

    public string aikasi;
    public InputField customaika;
    public Toggle nappi;
    public GameObject error;
    
    public bool on;
    public bool mss;
    public string msDigi;

    
    float sRad;
    float mRad;
    float tRad;

    void Update()
    {
        //Tietokoneen aika nyt
        DateTime aika = DateTime.Now;

        //onko kello päällä
        if (on)
        {

            //aikayksiköt radiaaneiksi kelloa varten
            //Viisareiden liikkeiden pehmentämiseksi lisätään aiempi aikayksikkö

            //nopeutus on extra ominaisuus 
            sRad = (float)((aika.Second + aika.Millisecond / 1000f) / (60f)) * -360f;

            //Liikuttaa viisareita
            sViisari.transform.eulerAngles = new Vector3(0, 0, sRad);


            mRad = (float)((aika.Minute + aika.Second / 60f) / (60f)) * -360f;
            mViisari.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, mRad));

            tRad = (float)((aika.Hour + aika.Minute / 60f) / (12f)) * -360f;
            tViisari.transform.eulerAngles = new Vector3(0, 0, tRad);



            //lisää nollan digikelloon kun aikayksikkö on yksilukuinen
            //millisekunnit
      
            string sDigi = Mathf.RoundToInt(aika.Second).ToString("00");
            string mDigi = Mathf.RoundToInt(aika.Minute).ToString("00");
            string tDigi = Mathf.RoundToInt(aika.Hour).ToString("00");

            if (mss == true)
            {
                msDigi = ":" + Mathf.RoundToInt(aika.Millisecond).ToString("000");
            }
            else
            {
                msDigi = "";
            }

            //sijoittaa ajan tekstikenttään
            digikello.text = tDigi + ":" + mDigi + ":" + sDigi + msDigi;

        }
    }

    public void MsToggle(bool toggle)
    {
        mss = toggle;
    }

    //aseta oma aika
    public void OmaAika()
    {
        //sammuttaa kellon
        on = false;
        nappi.isOn = true;
        //ottaa ajan input tekstikentästä
        aikasi = customaika.text;

        //erottaa aikayksiköt
        try
        {
            String[] aikaerotettu = aikasi.Split(':');




            // muuntaa inputin floatiksi ja aikayksiköt radiaaneiksi analogiselle
            sRad = (float.Parse(aikaerotettu[2]) / 60f) * -360f;
            mRad = ((float.Parse(aikaerotettu[1]) + (float.Parse(aikaerotettu[2]) / 60f)) / 60f) * -360f;
            tRad = ((float.Parse(aikaerotettu[0]) + (float.Parse(aikaerotettu[1]) / 60f)) / 12f) * -360f;

            //asettaa rotaatiot 
            sViisari.transform.eulerAngles = new Vector3(0, 0, sRad);
            mViisari.transform.eulerAngles = new Vector3(0, 0, mRad);
            tViisari.transform.eulerAngles = new Vector3(0, 0, tRad);


            //digikello
            string sDigi = Mathf.Floor(float.Parse(aikaerotettu[2])).ToString("00");
            string mDigi = Mathf.Floor(float.Parse(aikaerotettu[1])).ToString("00");
            string tDigi = Mathf.Floor(float.Parse(aikaerotettu[0])).ToString("00");

            digikello.text = tDigi + ":" + mDigi + ":" + sDigi;

        }
        catch
        {
            Debug.Log("error");
            error.SetActive(true);
        }
        //Debug.Log(aikasi + "   " + sRad + mRad + tRad);
    }

    public void Sammuta(bool toggle)
    {
        error.SetActive(false);
        on = !toggle;
    }
}

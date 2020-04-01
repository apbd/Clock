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

    //radiaanit
    float sAste;
    float mAste;
    float tAste;

    public TextMeshProUGUI digikello;



    //extra ominaisuuksia

    public string aikasi;
    public InputField customaika;
    public Toggle stopnappi;
    public GameObject error;
    
    public bool on;
    public bool mss;
    public string msDigi;

    


    void Update()
    {
        //Tietokoneen aika nyt
        DateTime aika = DateTime.Now;
        

        //onko kello päällä
        if (on)
        {

            //aikayksiköt radiaaneiksi kelloa varten
            //Viisareiden liikkeiden pehmentämiseksi lisätään aiempi aikayksikkö

            sAste = (float)((aika.Second + aika.Millisecond / 1000f) / (60f)) * -360f;

            //Liikuttaa viisareita
            sViisari.transform.eulerAngles = new Vector3(0, 0, sAste);


            mAste = (float)((aika.Minute + aika.Second / 60f) / (60f)) * -360f;
            mViisari.transform.eulerAngles = new Vector3(0, 0, mAste);

            tAste = (float)((aika.Hour + aika.Minute / 60f) / (12f)) * -360f;
            tViisari.transform.eulerAngles = new Vector3(0, 0, tAste);



            //lisää nollan digikelloon kun aikayksikkö on yksilukuinen

            string sDigi = Mathf.RoundToInt(aika.Second).ToString("00");
            string mDigi = Mathf.RoundToInt(aika.Minute).ToString("00");
            string tDigi = Mathf.RoundToInt(aika.Hour).ToString("00");

            //millisekunnit jotka näkyvät jos toggle on päällä
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

    //millisekunttitoggle
    public void MsToggle(bool toggle)
    {
        mss = toggle;
    }

    //aseta oma aika
    public void OmaAika()
    {
        //sammuttaa kellon
        on = false;
        stopnappi.isOn = true;

        //ottaa ajan input tekstikentästä
        aikasi = customaika.text;

        
        //virheiden varalta
        try
        {
            //: kohdassa erottaa merkkijonot ja laittaa ne listaan 
            String[] aikaerotettu = aikasi.Split(':');

            // muuntaa inputin floatiksi ja aikayksiköt radiaaneiksi analogiselle
            sAste = (float.Parse(aikaerotettu[2]) / 60f) * -360f;
            mAste = ((float.Parse(aikaerotettu[1]) + (float.Parse(aikaerotettu[2]) / 60f)) / 60f) * -360f;
            tAste = ((float.Parse(aikaerotettu[0]) + (float.Parse(aikaerotettu[1]) / 60f)) / 12f) * -360f;

            //asettaa rotaatiot 
            sViisari.transform.eulerAngles = new Vector3(0, 0, sAste);
            mViisari.transform.eulerAngles = new Vector3(0, 0, mAste);
            tViisari.transform.eulerAngles = new Vector3(0, 0, tAste);

            //oman ajan digikello
            string sDigi = Mathf.Floor(float.Parse(aikaerotettu[2])).ToString("00");
            string mDigi = Mathf.Floor(float.Parse(aikaerotettu[1])).ToString("00");
            string tDigi = Mathf.Floor(float.Parse(aikaerotettu[0])).ToString("00");

            digikello.text = tDigi + ":" + mDigi + ":" + sDigi;

        }
        catch
        {
            //punainen error teksti
            Debug.Log("input error");
            error.SetActive(true);
        }
      
    }

    //sammuttaa kellon jos toggle on päällä
    public void Sammuta(bool toggle)
    {
        error.SetActive(false);
        on = !toggle;
    }
}

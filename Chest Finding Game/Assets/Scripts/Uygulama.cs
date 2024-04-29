using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uygulama : MonoBehaviour
{
    // Start is called before the first frame update

    public static string hazineOutput = "";

    public static string altinOutput = "";
    public static string gumusOutput = "";
    public static string bakirOutput = "";
    public static string zumrutOutput = "";

    public static int adim_sayisi;

    public static int sandik_sayisi;

    public string getHazineOutput()
    {
        return hazineOutput;
    }

    public Uygulama()
    {
        
    }


    public void NewHazineOutput(string hazineOutput_, string tur)
    {
       if(tur == "Altin")
        {
            altinOutput += hazineOutput_;
            altinOutput += "\n";
            Debug.Log("gelen str" + hazineOutput_ +  " " +"altinOutput" + altinOutput );

        }
        else if(tur == "Gumus")
        {
            gumusOutput += hazineOutput_;
            gumusOutput += "\n";
        }
        else if(tur == "Bakir")
        {
            bakirOutput += hazineOutput_;
            bakirOutput += "\n";
        }
        else if(tur == "Zumrut")
        {
            zumrutOutput += hazineOutput_;
            zumrutOutput += "\n";
        }
        else
        {
            Debug.Log("Hazine turu hatali");
        }

        hazineOutput = altinOutput  +gumusOutput + zumrutOutput +bakirOutput;
        Debug.Log("hazineOutput" + hazineOutput);
        //yazdirma islemi
    }

    public void setAdimSayisi(int adim_sayisi_)
    {
        adim_sayisi = adim_sayisi_;
    }

    public int getAdimSayisi()
    {
        return adim_sayisi;
    }

    public void setSandikSayisi(int sandik_sayisi_)
    {
        sandik_sayisi = sandik_sayisi_;
    }

    public int getSandikSayisi()
    {
        return sandik_sayisi;
    }

}

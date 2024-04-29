using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Textler : MonoBehaviour
{
    public Text sagText;
    public Text solText;
    public Text sayiText;
    public Text sayiText2;
    public Text sandikText;
    
    void Update()
    {
        Uygulama u = new Uygulama();
        sagText.text = u.getHazineOutput();
        //solText.text =

        SpawnGenerator sg = new SpawnGenerator();
        solText.text = sg.getKesfetmeOutput();

        //adim guncelleme
        sayiText.text = u.getAdimSayisi().ToString();


        sayiText2.text = u.getAdimSayisi().ToString();

        sandikText.text = u.getSandikSayisi().ToString();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGenerator : MonoBehaviour
{
    //karakter
    public GameObject karakterPreFab;
    public GameObject karakterInstance;
    //tum nesneler
    public List<GameObject> nesneler = new List<GameObject>();

    //haraketsiz nesneler
    public GameObject YazAgacPreFab;
    //public GameObject YazAgacInstance;

    public GameObject KisAgacPreFab;
    //public GameObject KisAgacInstance;

    public GameObject YazAgacPreFabKucuk;

    public GameObject KisAgacPreFabKucuk;

    public GameObject YazKayaPreFab;
    //public GameObject YazKayaInstance;

    public GameObject KisKayaPreFab;
    //public GameObject KisKayaInstance;

    public GameObject YazKayaPreFabBuyuk;

    public GameObject KisKayaPreFabBuyuk;

    public GameObject DuvarlarPreFab;
    //public GameObject DuvarlarInstance;

    public GameObject YazDagPreFab;
    //public GameObject YazDagInstance;

    public GameObject KisDagPreFab;
    //public GameObject KisDagInstance;

    //haraketli nesneler

    public GameObject KusPreFab;
    public GameObject KusPre;
    //public GameObject KusInstance;

    public GameObject AriPreFab;
    public GameObject AriPre;
    //public GameObject AriInstance;

    public GameObject AltinSandikPreFab;
    public GameObject GumusSandikPreFab;
    public GameObject BakirSandikPreFab;
    public GameObject ZumrutSandikPreFab;


    public int agac_no;
    public int kucuk_agac_no;
    public int dag_no;
    public int kaya_no;
    public int kucuk_kaya_no;
    public int duvar_no;
    public int kus_no;
    public int ari_no;

    public static List<Engel> engeller = new List<Engel>();


    public int  gumus_sandik_no;
    public int  altin_sandik_no;
    public int  bakir_sandik_no;
    public int  zumrut_sandik_no;
    public static List<Hazine> hazineList = new List<Hazine>();


    public GameObject BeyazKarePreFab;
    public GameObject BeyazKarePreFab2;

    private List<Kuslar> kuslarList = new List<Kuslar>();
    private List<Arilar> arilarList = new List<Arilar>();

    public GameObject kamera;


    public float hiz = 100f;

    public GameOverScreen gameOverScreen;
    public GameOverScreen gameOverScreen2;
    public GameObject endGameScreen;

    public InputField boyutInput;
    public int int_boyut;
    public int objId = 2;

    public GameObject sagPanel;
    public GameObject solPanel;
    public GameObject adimPanel;
    public GameObject sandikPanel;
    

    

    public static List<Engel> kucuk_agac_list = new List<Engel>();
    public static List<Engel> buyuk_agac_list = new List<Engel>();

    public static List<Engel> dag_listesi = new List<Engel>();

    public static List<Engel> kucuk_kaya_listesi = new List<Engel>();
    public static List<Engel> buyuk_kaya_listesi = new List<Engel>();

    public static List<Engel> duvar_listesi = new List<Engel>();

    public static List<Engel> kuslarListesi = new List<Engel>();

    public static List<Engel> arilarListesi = new List<Engel>();

    public static List<Hazine> gumus_sandik_listesi = new List<Hazine>();
    public static List<Hazine> altin_sandik_listesi = new List<Hazine>();
    public static List<Hazine> bakir_sandik_listesi = new List<Hazine>();
    public static List<Hazine> zumrut_sandik_listesi = new List<Hazine>();

    public static string kesfetmeOutput = "";


    public SpawnGenerator()
    {
    }

    public List<Hazine> getHazineList()
    {
        return hazineList;
    }
    public string getKesfetmeOutput()
    {
        return kesfetmeOutput;
    }


    public void BoyutAtama()
    {
        Temizle();
        string boyut;
        boyut = boyutInput.text;
        int_boyut = int.Parse(boyut);

        Lokasyon karakter_lokasyonu = new Lokasyon(Random.Range(-int_boyut/2, int_boyut/2), Random.Range(-int_boyut/2, int_boyut/2));
        Karakter a = new Karakter(1, "Sefa", karakter_lokasyonu);
        GridArray g = new GridArray(int_boyut,int_boyut);
        print("Karakterin Lokasyonu: " + karakter_lokasyonu.getX() + " " + karakter_lokasyonu.getY());
        g.SetGrid(karakter_lokasyonu.getX(), karakter_lokasyonu.getY(), 1); // 1 -> KARAKTER
        //g.printGrid();

       

        float x = karakter_lokasyonu.getX() + 0.5f;
        float y = karakter_lokasyonu.getY() + 0.5f;
        UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y, 0);


        //karakter clone degerleri guncelleme
       //karakterPreFab.GetComponent<Karakter>().getAd() = a.getAd();
       //karakterPreFab.GetComponent<Karakter>().id = a.getId();

        karakterInstance = Instantiate(karakterPreFab, randomLokasyon, UnityEngine.Quaternion.identity);
        kamera.GetComponent<CameraSystem>().player = karakterInstance;
        a.setInstance(karakterInstance);
        GameObject BeyazKare = Instantiate(BeyazKarePreFab);
        GameObject BeyazKareSecondary = Instantiate(BeyazKarePreFab2);
        BeyazKare.transform.parent = karakterInstance.transform;
        BeyazKareSecondary.transform.parent = karakterInstance.transform;
        BeyazKare.transform.localPosition = UnityEngine.Vector3.zero;
        BeyazKareSecondary.transform.localPosition = UnityEngine.Vector3.zero;


        //pathfinder kontrolü
        /*

        int x_kontrol = karakter_lokasyonu.getX() +  int_boyut/2;
        int y_kontrol = karakter_lokasyonu.getY() + int_boyut/2;

        if(x_kontrol-3 >= 0 && x_kontrol+3 < int_boyut && y_kontrol-3 >= 0 && y_kontrol+3 < int_boyut)
        {
            Debug.Log("Pathfinder yapilabilir");
            int x_karakter = karakter_lokasyonu.getX();
            int y_karakter = karakter_lokasyonu.getY();

            Kayalar k1 = new Kayalar(x_karakter-1,y_karakter+2,2,2,1000,false);

            Kayalar k2 = new Kayalar(x_karakter+1,y_karakter+2,2,2,1001,false);

            Kayalar k3 = new Kayalar(x_karakter+2,y_karakter,2,2,10*2,false);

            Kayalar k4 = new Kayalar(x_karakter-1,y_karakter,2,2,1003,false);

            Kayalar k5 = new Kayalar(x_karakter+1,y_karakter-2,2,2,1004,false);

            GameObject go_k1 = k1.SpawnObject(new UnityEngine.Vector3(x_karakter-1, y_karakter+2, 1),KisKayaPreFab, k1.GetYukseklik(),k1.GetGenislik(), false, 1, int_boyut);
            GameObject go_k2 = k2.SpawnObject(new UnityEngine.Vector3(x_karakter+1, y_karakter+2, 1),KisKayaPreFab, k2.GetYukseklik(),k2.GetGenislik(), false, 1, int_boyut);
            GameObject go_k3 = k3.SpawnObject(new UnityEngine.Vector3(x_karakter+2, y_karakter, 1),KisKayaPreFab, k3.GetYukseklik(),k3.GetGenislik(), false, 1, int_boyut);
            GameObject go_k4 = k4.SpawnObject(new UnityEngine.Vector3(x_karakter-1, y_karakter, 1),KisKayaPreFab, k4.GetYukseklik(),k4.GetGenislik(), false, 1, int_boyut);
            GameObject go_k5 = k5.SpawnObject(new UnityEngine.Vector3(x_karakter+1, y_karakter-2, 1),KisKayaPreFab, k5.GetYukseklik(),k5.GetGenislik(), false, 1, int_boyut);

            nesneler.Add(go_k1);
            nesneler.Add(go_k2);
            nesneler.Add(go_k3);
            nesneler.Add(go_k4);
            nesneler.Add(go_k5);
            
        }
        else
        {
            Debug.Log("Pathfinder yapilamaz");
        }
        */
        
        //pathfinder kontrol sonu


        //engelleri olusturma
        EngelOlusturma();
        //g.printGrid();
    }

    

    public void EngelOlusturma()
    {
        GridArray gridArray = new GridArray();

        int sabit_ust_sinir = 8;
        int agac_sayisi = Random.Range(6, sabit_ust_sinir);
        int dag_sayisi =  Random.Range(3, sabit_ust_sinir);
        int kaya_sayisi = Random.Range(8, sabit_ust_sinir);
        int duvar_sayisi = Random.Range(3, sabit_ust_sinir);

        //Debug.Log(agac_sayisi);
        //Debug.Log(dag_sayisi);
        //Debug.Log(kaya_sayisi);
        //Debug.Log(duvar_sayisi);

        int hareketli_ust_sinir = 4;

        int kus_sayisi = Random.Range(1, hareketli_ust_sinir);
        int arisayisi = Random.Range(2, hareketli_ust_sinir);
        //Debug.Log(kus_sayisi);
        //Debug.Log(arisayisi);

        int sandik_ust_sinir = 8;
        int altin_sandik_sayisi = Random.Range(5, sandik_ust_sinir);
        int gumus_sandik_sayisi = Random.Range(5, sandik_ust_sinir);
        int bakir_sandik_sayisi = Random.Range(5, sandik_ust_sinir);
        int zumrut_sandik_sayisi = Random.Range(5, sandik_ust_sinir);

        //SAYI KADAR DONGULER ASAGIDA YAPILACAK
        int deneme_sayisi = 1000;
        //agac
        Debug.Log("agac sayisi" + agac_sayisi);


        
        int kucuk_agac_sayisi = Random.Range(1,agac_sayisi);
        int buyuk_agac_sayisi = agac_sayisi - kucuk_agac_sayisi;

        //buyuk agac
        for(int i = 0; i < buyuk_agac_sayisi; i++)
        {

            int kontrol_uzunlugu = 3;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0;s < deneme_sayisi;s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Agaclar a = new Agaclar(x,y,5,5,objId,false);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!a.IsColliding(randomLokasyon, a.GetYukseklik(), a.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    buyuk_agac_list.Add(a);
                    yer_bulundu_mu = 1;
                    //a.setVec(randomLokasyon);
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = a.SpawnObject(randomLokasyon,KisAgacPreFab, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        //objId++;
                    }
                    else
                    {
                        GameObject engel1 = a.SpawnObject(randomLokasyon,YazAgacPreFab, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        //objId++;
                    }
                    break;
                }
                
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();

                
                return;
            }
        }
        this.agac_no = objId;
        gridArray.setBuyukAgacNo(objId);
        
        objId++;
        


        //kucuk agac 4x4
        Debug.Log("kucuk agac sayisi" + kucuk_agac_sayisi);
        for(int i = 0; i < kucuk_agac_sayisi; i++)
        {
            int kontrol_uzunlugu = 2;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Agaclar a = new Agaclar(x,y,4,4,objId,false);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!a.IsColliding(randomLokasyon, a.GetYukseklik(), a.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    kucuk_agac_list.Add(a);
                    engeller.Add(a);
                    //a.setVec(randomLokasyon);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = a.SpawnObject(randomLokasyon,KisAgacPreFabKucuk, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        //objId++;
                    }
                    else
                    {
                        GameObject engel1 = a.SpawnObject(randomLokasyon,YazAgacPreFabKucuk, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        //objId++;
                    }
                    break;
                }

            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        this.kucuk_agac_no = objId;
        gridArray.setKucukAgacNo(objId);
        objId++;
        
        //dag
        Debug.Log("dag sayisi" + dag_sayisi);
        for(int i = 0; i < dag_sayisi; i++)
        {
            int kontrol_uzunlugu = 8;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0; s < deneme_sayisi ; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Daglar d = new Daglar(x,y,15,15,objId,false);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!d.IsColliding(randomLokasyon, d.GetYukseklik(), d.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    dag_listesi.Add(d);
                    engeller.Add(d);
                    //d.setVec(randomLokasyon);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = d.SpawnObject(randomLokasyon,KisDagPreFab, d.GetYukseklik(),d.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        //objId++;
                    }
                    else
                    {
                        GameObject engel1 = d.SpawnObject(randomLokasyon,YazDagPreFab, d.GetYukseklik(),d.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        //objId++;
                    }
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
               return;
            }
        }
        this.dag_no = objId;
        gridArray.setDagNo(objId);
        objId++;
        

        //kaya
        Debug.Log("kaya sayisi" + kaya_sayisi);

        int kucuk_kaya_sayisi = Random.Range(1,kaya_sayisi);
        int buyuk_kaya_sayisi = kaya_sayisi - kucuk_kaya_sayisi;

        


        Debug.Log("kucuk kaya sayisi" + kucuk_kaya_sayisi);
        //kucuk kaya
        for(int i = 0; i < kucuk_kaya_sayisi;i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Kayalar k = new Kayalar(x,y,2,2,objId,false);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!k.IsColliding(randomLokasyon, k.GetYukseklik(), k.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    kucuk_kaya_listesi.Add(k);
                    //k.setVec(randomLokasyon);
                    engeller.Add(k);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = k.SpawnObject(randomLokasyon,KisKayaPreFab, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        //objId++;
                    }
                    else
                    {
                        GameObject engel1 = k.SpawnObject(randomLokasyon,YazKayaPreFab, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        //objId++;
                    }
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
               
                return;
            }
            
        }

        this.kucuk_kaya_no = objId;
        gridArray.setKucukKayaNo(objId);
        objId++;
        
        //buyuk kaya 3x3

        for(int i = 0; i < buyuk_kaya_sayisi; i++)
        {
            int kontrol_uzunlugu = 2;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Kayalar k = new Kayalar(x,y,3,3,objId,false);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!k.IsColliding(randomLokasyon, k.GetYukseklik(), k.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    buyuk_kaya_listesi.Add(k);
                    //k.setVec(randomLokasyon);
                    engeller.Add(k);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = k.SpawnObject(randomLokasyon,KisKayaPreFabBuyuk, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        //objId++;
                    }
                    else
                    {
                        GameObject engel1 = k.SpawnObject(randomLokasyon,YazKayaPreFabBuyuk, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        //objId++;
                    }
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }

        this.kaya_no = objId;
        gridArray.setBuyukKayaNo(objId);
        objId++;
        
        //duvar
        Debug.Log("duvar sayisi" + duvar_sayisi);
        for(int i = 0; i < duvar_sayisi; i++)
        {

            int kontrol_uzunlugu = 5;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+1, int_boyut/2-1);
                Duvarlar d = new Duvarlar(x,y,1,10,objId,false);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!d.IsColliding(randomLokasyon, d.GetYukseklik(), d.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    duvar_listesi.Add(d);
                    //d.setVec(randomLokasyon);
                    engeller.Add(d);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject engel = d.SpawnObject(randomLokasyon,DuvarlarPreFab, d.GetYukseklik(),d.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    nesneler.Add(engel);
                    //gridArray.printGrid();
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        this.duvar_no = objId;
        gridArray.setDuvarNo(objId);
        objId++;

        //kus
        Debug.Log("kus sayisi" + kus_sayisi);
        for(int i = 0; i < kus_sayisi; i++)
        {
            int kontrol_uzunlugu_x = 1;
            int kontrol_uzunlugu_y = 6;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu_x, int_boyut/2-kontrol_uzunlugu_x);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu_y, int_boyut/2-kontrol_uzunlugu_y);
                //Arilar a = new Arilar(x,y,12,2,objId);
                Kuslar k = new Kuslar(x,y,12,2,objId,false);
                
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!k.IsColliding(randomLokasyon, k.GetYukseklik(), k.GetGenislik(),kontrol_uzunlugu_y, tek_mi, int_boyut))
                {
                    //k.setVec(randomLokasyon);
                    kuslarListesi.Add(k);
                    engeller.Add(k);
                    kuslarList.Add(k);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject engel = k.SpawnObject(randomLokasyon,KusPreFab, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu_y, int_boyut);
                    nesneler.Add(engel);
                    GameObject hayvan = k.SpawnAnimal(randomLokasyon,KusPre, tek_mi);
                    nesneler.Add(hayvan);
                    //gridArray.printGrid();
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }

        this.kus_no = objId;
        gridArray.setKusNo(objId);
        objId++;

        //ari
        Debug.Log("ari sayisi" + arisayisi);
        for(int i = 0; i < arisayisi; i++)
        {
            int kontrol_uzunlugu_x = 4;
            int kontrol_uzunlugu_y = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {

                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu_x, int_boyut/2-kontrol_uzunlugu_x);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu_y, int_boyut/2-kontrol_uzunlugu_y);
                Arilar a = new Arilar(x,y,2,8,objId,false);
                
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!a.IsColliding(randomLokasyon, a.GetYukseklik(), a.GetGenislik(),kontrol_uzunlugu_x, tek_mi, int_boyut))
                {
                    //a.setVec(randomLokasyon);
                    arilarListesi.Add(a);
                    engeller.Add(a);
                    arilarList.Add(a);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject engel = a.SpawnObject(randomLokasyon,AriPreFab, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu_x, int_boyut);
                    nesneler.Add(engel);
                    GameObject hayvan = a.SpawnAnimal(randomLokasyon,AriPre, tek_mi);
                    nesneler.Add(hayvan);
                    
                    //gridArray.printGrid();
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }

        }
        this.ari_no = objId;
        gridArray.setAriNo(objId);
        objId++;

        //sandiklar

        //gumus sandik
        Debug.Log("gumus sandik sayisi" + gumus_sandik_sayisi);
        for(int i = 0; i < gumus_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine gumus = new Hazine(x,y,2,2,"gumus",objId,false);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!gumus.IsColliding(randomLokasyon, gumus.getGenislik(), gumus.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    //gumus.setVec(randomLokasyon);
                    gumus_sandik_listesi.Add(gumus);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = gumus.SpawnObject(randomLokasyon,GumusSandikPreFab, gumus.getYukseklik(),gumus.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(gumus);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        gumus_sandik_no = objId;
        gridArray.setGumusSandikNo(gumus_sandik_no);
        objId++;

        //altin sandik
        Debug.Log("altin sandik sayisi" + altin_sandik_sayisi);
        for(int i = 0; i < altin_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine altin = new Hazine(x,y,2,2,"altin",objId,false);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!altin.IsColliding(randomLokasyon, altin.getGenislik(), altin.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    altin_sandik_listesi.Add(altin);
                    //altin.setVec(randomLokasyon);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = altin.SpawnObject(randomLokasyon,AltinSandikPreFab, altin.getYukseklik(),altin.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(altin);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        altin_sandik_no = objId;
        gridArray.setAltinSandikNo(altin_sandik_no);
        objId++;

        //bakir sandik
        Debug.Log("bakir sandik sayisi" + bakir_sandik_sayisi);
        for(int i = 0; i < bakir_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine bakir = new Hazine(x,y,2,2,"bakir",objId,false);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!bakir.IsColliding(randomLokasyon, bakir.getGenislik(), bakir.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    bakir_sandik_listesi.Add(bakir);
                    yer_bulundu_mu = 1;
                    //bakir.setVec(randomLokasyon);
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = bakir.SpawnObject(randomLokasyon,BakirSandikPreFab, bakir.getYukseklik(),bakir.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(bakir);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        bakir_sandik_no = objId;
        gridArray.setBakirSandikNo(bakir_sandik_no);
        objId++;

        //zumrut sandik
        
        Debug.Log("zumrut sandik sayisi" + zumrut_sandik_sayisi);
        for(int i = 0; i < zumrut_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine zumrut = new Hazine(x,y,2,2,"zumrut",objId,false);
                UnityEngine.Vector3 randomLokasyon = new UnityEngine.Vector3(x, y , 1); //z'ye bakilacak
                if(!zumrut.IsColliding(randomLokasyon, zumrut.getGenislik(), zumrut.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    zumrut_sandik_listesi.Add(zumrut);
                    //zumrut.setVec(randomLokasyon);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = zumrut.SpawnObject(randomLokasyon,ZumrutSandikPreFab, zumrut.getYukseklik(),zumrut.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(zumrut);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        zumrut_sandik_no = objId;
        gridArray.setZumrutSandikNo(zumrut_sandik_no);
        objId++;
        

        if(hazinePathFinder() == false)
        {
            Debug.Log("HATA");
            ObjeTemizle();
            gameOverScreen2.Setup();

            return;

        }

        Uygulama u = new Uygulama();
        u.setAdimSayisi(0);
        u.setSandikSayisi(hazineList.Count);

        GridArray.herSeyHazirMi = true;


        //RotaHesaplama();


        
    }

    public void ObjeTemizle() // kamera olayi icin - game over olayi
    {
        foreach(Hazine h in hazineList)
        {
            if(h.silindi_mi == false)
            {
                h.setSilindi_mi(true);
                Destroy(h.getHazine());
            }
        }

        foreach(GameObject nesne in nesneler)
        {
            Destroy(nesne);
        }
        nesneler.Clear();


        //listeleri temizle
        kuslarList.Clear();
        arilarList.Clear();
    }

    public void Temizle()
    {
        if(karakterInstance != null) // Check if an instance exists
        {
            Destroy(karakterInstance);
            
        }
        foreach(Hazine h in hazineList)
        {
            if(h.silindi_mi == false)
            {
                h.setSilindi_mi(true);
                Destroy(h.getHazine());
            }
        }
        foreach(GameObject nesne in nesneler)
        {
            Destroy(nesne);
        }
        nesneler.Clear();

        //listeleri temizle
        kuslarList.Clear();
        arilarList.Clear();
    }


    public bool tumSandiklarToplandiMi()
    {
        Debug.Log("hazine sayisi" + hazineList.Count);
        foreach(Hazine h in hazineList)
        {
            if(h.silindi_mi == false)
            {
                return false;
            }
        }

        return true;
    }

    void Update()
    {
        GridArray gridArray = new GridArray();
        if(gridArray.getHazir())
        {
            foreach (Kuslar kus in kuslarList)
            {
                GameObject kusPrefab = kus.kus;
                kusPrefab.transform.position += kus.getYon()*30 * 0.1f * Time.deltaTime;
                if(kusPrefab.transform.position.y <= kus.min_y)
                {
                    kus.Yon = UnityEngine.Vector3.up;
                }
                else if(kusPrefab.transform.position.y >= kus.max_y)
                {
                    kus.Yon = UnityEngine.Vector3.down;
                }
            }
            foreach(Arilar ari in arilarList)
            {
                GameObject ariPrefab = ari.ari;
                ariPrefab.transform.position += ari.GetYon()*30 * 0.1f * Time.deltaTime;
                if(ariPrefab.transform.position.x <= ari.min_x)
                {
                    ari.Yon = UnityEngine.Vector3.right;
                }
                else if(ariPrefab.transform.position.x >= ari.max_x)
                {
                    ari.Yon = UnityEngine.Vector3.left;
                }
            }

            if(!boyutInput.IsActive())
            {
                KarakterHareket();   
                
            }



            if(tumSandiklarToplandiMi())
            {
                Debug.Log("Oyun bitti");
                endGameScreen.SetActive(true);
                sagPanel.SetActive(false);
                solPanel.SetActive(false);
                adimPanel.SetActive(false);
                sandikPanel.SetActive(false);
            }
        }
    }
    
    void KarakterHareket()
    {
        Karakter k = new Karakter();
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //karakterInstance.transform.Translate(Vector3.left);
            //karakterInstance.transform.position += Vector3.left;
            k.SolaGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //karakterInstance.transform.Translate(Vector3.right);
            k.SagaGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //karakterInstance.transform.Translate(Vector3.down);
            k.AsagiGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //karakterInstance.transform.Translate(Vector3.up);
            k.YukariGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        }
    }


    public void cevreKontrol(int x, int y)
    {
        GridArray gridArray = new GridArray();
        int[,] grid = gridArray.GetGrid();
        x += gridArray.getSize()/2;
        y += gridArray.getSize()/2;
        //Debug.Log(x + " " + y + " " + "son");
        if(x < 0 || y < 0 || x >= gridArray.getSize() || y >= gridArray.getSize())
        {
            return;
        }
        int baslangic_x = 0;
        int baslangic_y = 0;
        int son_x;
        int son_y;
        if(x-3 < 0)
        {
            baslangic_x = 0;
        }
        else
        {
            baslangic_x = x-3;
        }
        if(x+4 >= gridArray.getSize())
        {
            son_x = gridArray.getSize()-1;
        }
        else
        {
            son_x = x+4;
        }
        if(y-3 < 0)
        {
            baslangic_y = 0;
        }
        else
        {
            baslangic_y = y-3;
        }
        if(y+4 >= gridArray.getSize())
        {
            son_y = gridArray.getSize()-1;
        }
        else
        {
            son_y = y+4;
        }

        //Debug.Log(baslangic_x + " " + baslangic_y + " " + son_x + " " + son_y + "*");
        Debug.Log("engel sayisi" + engeller.Count);
        for(int y_ = baslangic_y; y_ < son_y; y_++)
        {
            for(int x_ = baslangic_x; x_ < son_x; x_++)
            {
                if(grid[y_,x_] != 0 && grid[y_,x_] != 1)
                {
                    //Debug.Log(y_ + " " + x_ + " " + grid[y_,x_] + " ALOO " + gridArray.getBuyukAgacNo() + " " + gridArray.getKucukAgacNo());
                    //Debug.Log(gridArray.getBuyukAgacNo() + " " + gridArray.getKucukAgacNo() + "*****************" + kucuk_agac_list.Count + " " + buyuk_agac_list.Count);
                    if(grid[y_,x_] == gridArray.getKucukAgacNo())
                    {
                        Debug.Log("aha! agac" + engeller.Count);
                        foreach(Engel e in kucuk_agac_list)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);


                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {

                                    //Debug.Log("Kucuk Agac Goruldu");
                                    kesfetmeOutput += "Kucuk Agac Kesfedildi" + "\n";

                                    e.goruldu_mu = true;
                                    //kucuk_agac_list.Remove(e);

                                }


                            }
                            
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getBuyukAgacNo())
                    {
                        Debug.Log("aha! buyuk agac" + engeller.Count);
                        foreach(Engel e in buyuk_agac_list)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);

                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Buyuk Agac Goruldu");
                                    kesfetmeOutput += "Buyuk Agac Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //buyuk_agac_list.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getKucukKayaNo())
                    {
                        Debug.Log("aha! kucuk kaya" + engeller.Count);
                        foreach(Engel e in kucuk_kaya_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);

                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Kucuk Kaya Goruldu");
                                    kesfetmeOutput += "Kucuk Kaya Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //kucuk_kaya_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getBuyukKayaNo())
                    {
                        Debug.Log("aha! buyuk kaya" + engeller.Count);
                        foreach(Engel e in buyuk_kaya_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Buyuk Kaya Goruldu");
                                    kesfetmeOutput += "Buyuk Kaya Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //buyuk_kaya_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getDuvarNo())
                    {
                        Debug.Log("aha! duvar" + engeller.Count);
                        foreach(Engel e in duvar_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Duvar Goruldu");
                                    kesfetmeOutput += "Duvar Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //duvar_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getDagNo())
                    {
                        Debug.Log("aha! dag" + engeller.Count);
                        foreach(Engel e in dag_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Dag Goruldu");
                                    kesfetmeOutput += "Dag Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //dag_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getKusNo())
                    {
                        Debug.Log("aha! kus" + engeller.Count);
                        foreach(Engel e in kuslarListesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Kus Goruldu");
                                    kesfetmeOutput += "Kus Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //kuslarListesi.Remove(e);
                                }
                            }
                        }

                    }
                    else if(grid[y_,x_] == gridArray.getAriNo())
                    {
                        Debug.Log("aha! ari" + engeller.Count);
                        foreach(Engel e in arilarListesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.GetGenislik();
                                int y_gezen_int_son = y_gezen_int + e.GetYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max_y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Ari Goruldu");
                                    kesfetmeOutput += "Ari Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //arilarListesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getGumusSandikNo())
                    {
                        Debug.Log("aha! gumus sandik" + engeller.Count);
                        foreach(Hazine e in gumus_sandik_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.getGenislik();
                                int y_gezen_int_son = y_gezen_int + e.getYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Gumus Sandik Goruldu");
                                    kesfetmeOutput += "Gumus Sandik Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //gumus_sandik_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getAltinSandikNo())
                    {
                        Debug.Log("aha! altin sandik" + engeller.Count);
                        foreach(Hazine e in altin_sandik_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.getGenislik();
                                int y_gezen_int_son = y_gezen_int + e.getYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Altin Sandik Goruldu");
                                    kesfetmeOutput += "Altin Sandik Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //altin_sandik_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getBakirSandikNo())
                    {
                        Debug.Log("aha! bakir sandik" + engeller.Count);
                        foreach(Hazine e in bakir_sandik_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.getGenislik();
                                int y_gezen_int_son = y_gezen_int + e.getYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Bakir Sandik Goruldu");
                                    kesfetmeOutput += "Bakir Sandik Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //bakir_sandik_listesi.Remove(e);
                                }
                            }
                        }
                    }
                    else if(grid[y_,x_] == gridArray.getZumrutSandikNo())
                    {
                        Debug.Log("aha! zumrut sandik" + engeller.Count);
                        foreach(Hazine e in zumrut_sandik_listesi)
                        {
                            //e.getVac()
                            if(e.goruldu_mu == false)
                            {
                                float x_gezen = e.getVec().x;
                                float y_gezen = e.getVec().y;

                                int x_gezen_int = (int)x_gezen;
                                int y_gezen_int = (int)y_gezen;

                                int x_gezen_int_son = x_gezen_int + e.getGenislik();
                                int y_gezen_int_son = y_gezen_int + e.getYukseklik();
                                //Debug.Log("min x: " +x_gezen_int + " max x:" + x_gezen_int_son + " min y:" + y_gezen_int + " max y:" + y_gezen_int_son + " " + "-----" + x_ + " " + y_);
                                if(x_ >= x_gezen_int && x_ < x_gezen_int_son && y_ >= y_gezen_int && y_ < y_gezen_int_son)
                                {
                                    //Debug.Log("Zumrut Sandik Goruldu");
                                    kesfetmeOutput += "Zumrut Sandik Kesfedildi" + "\n";
                                    e.goruldu_mu = true;
                                    //zumrut_sandik_listesi.Remove(e);
                                }
                            }
                        }
                    }
                }
            }
           
        }

    }


    bool isCoinOrPath(int y,int x)
    {
        GridArray gridArray = new GridArray();
        int[,] grid = gridArray.GetGrid();
        if(grid[y,x] == 0 || grid[y,x] == gridArray.getGumusSandikNo() || grid[y,x] == gridArray.getAltinSandikNo() || grid[y,x] == gridArray.getBakirSandikNo() || grid[y,x] == gridArray.getZumrutSandikNo())
        {
            return true;
        }
        return false;

    }

    void pathfinderRecursive(int[,] grid, List<Lokasyon> list)
    {
        if(list[0].x < 0 || list[0].y < 0 || list[0].x >= int_boyut || list[0].y >= int_boyut)
        {
            list.RemoveAt(0);
            return;
        }
        /*// kontrol ama gerek yok gibi
        if(x < 0 || y < 0 || x >= int_boyut || y >= int_boyut)
        {
            return grid;
        }
        */

        if(grid[list[0].y,list[0].x] != -27)
        {
            grid[list[0].y,list[0].x] = -27;
        }

        if(list[0].y-1 >= 0 && isCoinOrPath(list[0].y-1,list[0].x) && grid[list[0].y-1,list[0].x] != -27)
        {
            grid[list[0].y-1,list[0].x] = -27;
            //pathfinderRecursive(grid,y-1,x);
            list.Add(new Lokasyon(list[0].x,list[0].y-1));
        }
        if(list[0].x+1 < int_boyut && isCoinOrPath(list[0].y,list[0].x+1) && grid[list[0].y,list[0].x+1] != -27)
        {
            grid[list[0].y,list[0].x+1] = -27;
            //pathfinderRecursive(grid,y,x+1);
            list.Add(new Lokasyon(list[0].x+1,list[0].y));
        }
        if(list[0].y+1 < int_boyut && isCoinOrPath(list[0].y+1,list[0].x) && grid[list[0].y+1,list[0].x] != -27)
        {
            grid[list[0].y+1,list[0].x] = -27;
            //pathfinderRecursive(grid,y+1,x);
            list.Add(new Lokasyon(list[0].x,list[0].y+1));
        }
        if(list[0].x-1 >= 0 && isCoinOrPath(list[0].y,list[0].x-1) && grid[list[0].y,list[0].x-1] != -27)
        {
            grid[list[0].y,list[0].x-1] = -27;
            //pathfinderRecursive(grid,y,x-1);
            list.Add(new Lokasyon(list[0].x-1,list[0].y));
        }
        list.RemoveAt(0);

    }

    bool hazinePathFinder()
    {
        GridArray gridArray = new GridArray();
        int[,] grid = gridArray.GetGrid();

        //copy grid

        int[,] gridCopy = new int[int_boyut,int_boyut];
        for(int i = 0; i < int_boyut; i++)
        {
            for(int j = 0; j < int_boyut; j++)
            {
                gridCopy[i,j] = grid[i,j];
            }
        }



        Karakter k = new Karakter();
        int x = k.getLokasyon().getX() + int_boyut/2;
        int y = k.getLokasyon().getY() + int_boyut/2;

        List<Lokasyon> pathfinderLokasyonlari = new List<Lokasyon>();
        pathfinderLokasyonlari.Add(new Lokasyon(x,y));

        while(pathfinderLokasyonlari.Count > 0)
        {
             pathfinderRecursive(gridCopy, pathfinderLokasyonlari);
        }

        //Debug.Log("bitti");

        
        //yazdirma
        for(int i = 0; i < int_boyut; i++)
        {
            string str = "";
            for(int j = 0; j < int_boyut; j++)
            {
                str += gridCopy[i,j].ToString() + " ";
            }
            Debug.Log(str);
        }


        

        //hazine kontrolu
        foreach(Hazine h in hazineList)
        {
            bool erisilebilir_mi = false;
            if(h.silindi_mi == false)
            {
                //sol kenar
                int sol_kenar_kontrol_x = h.getBaslangic_x() + int_boyut/2  - 2;
                int sol_kenar_kontrol_y = h.getBaslangic_y() + int_boyut/2 - 1;
                //Debug.Log(sol_kenar_kontrol_x + " " + sol_kenar_kontrol_y);
                if(sol_kenar_kontrol_x >= 0 && sol_kenar_kontrol_y >= 0 && sol_kenar_kontrol_x < int_boyut && sol_kenar_kontrol_y < int_boyut)
                {
                    if((gridCopy[sol_kenar_kontrol_y,sol_kenar_kontrol_x] == -27) || (gridCopy[sol_kenar_kontrol_y+1,sol_kenar_kontrol_x] == -27))
                    {
                        erisilebilir_mi = true;
                    }  
                }
                if(erisilebilir_mi)
                {
                    continue;
                }
                //sag kenar
                int sag_kenar_kontrol_x = h.getBaslangic_x()+ int_boyut/2 + 1;
                int sag_kenar_kontrol_y = h.getBaslangic_y()+ int_boyut/2 - 1;

                if(sag_kenar_kontrol_x >= 0 && sag_kenar_kontrol_y >= 0 && sag_kenar_kontrol_x < int_boyut && sag_kenar_kontrol_y < int_boyut)
                {
                    if((gridCopy[sag_kenar_kontrol_y,sag_kenar_kontrol_x] == -27) || (gridCopy[sag_kenar_kontrol_y+1,sag_kenar_kontrol_x] == -27))
                    {
                        erisilebilir_mi = true;
                    }
                }
                
                if(erisilebilir_mi)
                {
                    continue;
                }
                //ust kenar
                int ust_kenar_kontrol_x = h.getBaslangic_x()+ int_boyut/2 - 1;
                int ust_kenar_kontrol_y = h.getBaslangic_y()+ int_boyut/2 - 2;
                
                if(ust_kenar_kontrol_x >= 0 && ust_kenar_kontrol_y >= 0 && ust_kenar_kontrol_x < int_boyut && ust_kenar_kontrol_y < int_boyut)
                {
                    if((gridCopy[ust_kenar_kontrol_y,ust_kenar_kontrol_x] == -27) || (gridCopy[ust_kenar_kontrol_y,ust_kenar_kontrol_x+1] == -27))
                    {
                        erisilebilir_mi = true;
                    }
                }
                if(erisilebilir_mi)
                {
                    continue;
                }
                //alt kenar
                int alt_kenar_kontrol_x = h.getBaslangic_x()+ int_boyut/2 - 1;
                int alt_kenar_kontrol_y = h.getBaslangic_y()+ int_boyut/2 + 1;
                
                if(alt_kenar_kontrol_x >= 0 && alt_kenar_kontrol_y >= 0 && alt_kenar_kontrol_x < int_boyut && alt_kenar_kontrol_y < int_boyut)
                {
                    if((gridCopy[alt_kenar_kontrol_y,alt_kenar_kontrol_x] == -27) || (gridCopy[alt_kenar_kontrol_y,alt_kenar_kontrol_x+1] == -27))
                    {
                        erisilebilir_mi = true;
                    }
                }

                if(erisilebilir_mi)
                {
                    continue;
                }
                else //4 kontrolde de true olmadiysa
                {
                    return false;
                }
            }
        }

        return true;
    }

}





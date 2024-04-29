using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Tilemap))]

public class GridManager : MonoBehaviour
{
    Tilemap tilemap;
    Grid grid;
    [SerializeField] TileBase tileBase;
    [SerializeField] TileBase tileBase2;

    public InputField boyutInput;
    public int mapBoyut;
    public GameObject YeniHarita;
    public GameObject BoyutGir;
    public GameObject BaslatDugme;
    public Camera MainCamera;
    public Camera SecondaryCamera;
    public RawImage rawImage;
    public GameObject sagPanel;
    public GameObject solPanel;
    public GameObject adimPanel;
    public GameObject sandikPanel;

    public GridManager()
    {

    }
    

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<Grid>();
        grid.Init(100);
        UpdateTilemap();
    }

    void UpdateTilemap()
    {
        for(int x = 0; x < grid.pixel; x++)
        {
            for(int y = 0; y < grid.pixel; y++)
            {
                if(x>=grid.pixel/2)
                {
                    tilemap.SetTile(new Vector3Int(x - (grid.pixel / 2), y - (grid.pixel / 2), 0), tileBase);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x - (grid.pixel / 2), y - (grid.pixel / 2), 0), tileBase2);
                }
            }
        }
    }

    void UpdateMap(int mapBoyut)
    {
        //test
        //Debug.Log(mapBoyut);
        
        
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<Grid>();
        grid.Init(mapBoyut);
        UpdateTilemap();
        

    }



    public void BoyutAtama()
    {
        BaslatDugme.SetActive(true);
        MapiTemizle();
        string boyut;
        boyut = boyutInput.text;
        int int_boyut = int.Parse(boyut);
        float float_boyut = float.Parse(boyut);
        UpdateMap(int_boyut);
        rawImage.rectTransform.sizeDelta = new Vector2(float_boyut,float_boyut);
        MainCamera.orthographicSize = float_boyut/2;
        SecondaryCamera.orthographicSize = float_boyut/2;

    }
    void MapiTemizle()
    {
        tilemap.ClearAllTiles();
    }
    public int getMapBoyut()
    {
        return mapBoyut;
    }

    public void Baslat()
    {
        YeniHarita.SetActive(false);
        BoyutGir.SetActive(false);
        BaslatDugme.SetActive(false);
        rawImage.gameObject.SetActive(true);
        sagPanel.SetActive(true);
        solPanel.SetActive(true);
        adimPanel.SetActive(true);
        sandikPanel.SetActive(true);
    }
}
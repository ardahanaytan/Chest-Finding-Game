using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridArray
{
    public static int[,] grid;
    public static int size;

    public static bool herSeyHazirMi = false;


    public static bool GidisHazirMi = false;

    public static int gumus_sandik_no;
    public static int altin_sandik_no;
    public static int bakir_sandik_no;
    public static int zumrut_sandik_no;

    public static int buyuk_agac_no;
    public static int kucuk_agac_no;
    public static int dag_no;
    public static int kucuk_kaya_no;
    public static int buyuk_kaya_no;
    public static int duvar_no;
    public static int kus_no;
    public static int ari_no;




    public GridArray()
    {

    }
    public GridArray(int x, int y)
    {
        grid = new int[x, y];
        //0'lar ile doldur
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grid[i, j] = 0;
            }
        }
        size = x;
    }

    public  int[,] GetGrid()
    {
        return grid;
    }

    public void SetGrid(int x, int y, int value)
    {
        x += size / 2;
        y += size / 2;

        //Debug.Log("x: " + x + " y: " + y + " value: " + value);
        
        //Debug.Log("önce: " + grid[y, x]);
        grid[y, x] = value;
        //Debug.Log("sonra: " + grid[y, x]);

    }

    public int getOne(int x, int y)
    {
        x+= size/2;
        y+= size/2;
        //Debug.Log("sonra" + x + " "+ y);
        return grid[y,x];
    }

    public void SetGridSqaure(int x1, int y1, int x2, int y2, int value)
    {
        x1 += size / 2;
        y1 += size / 2;
        x2 += size / 2;
        y2 += size / 2;
        for (int i = x1; i <= x2; i++)
        {
            for (int j = y1; j <= y2; j++)
            {
                grid[j, i] = value;
            }
        }
    }


    public void printGrid()
    {
        for (int i = size-1; 0 <= i; i--)
        {

            string str = "";
            for (int j = 0; j < size; j++)
            {
                str += grid[i, j].ToString() + " ";
            }
            Debug.Log(str+"\n");
        }
    }

    public bool getHazir()
    {
        return herSeyHazirMi;
    }

    public int getSize()
    {
        return size;
    }

    public void setAltinSandikNo(int no)
    {
        altin_sandik_no = no;
    }

    public void setBakirSandikNo(int no)
    {
        bakir_sandik_no = no;
    }

    public void setGumusSandikNo(int no)
    {
        gumus_sandik_no = no;
    }

    public void setZumrutSandikNo(int no)
    {
        zumrut_sandik_no = no;
    }

    public int getAltinSandikNo()
    {
        return altin_sandik_no;
    }

    public int getBakirSandikNo()
    {
        return bakir_sandik_no;
    }

    public int getGumusSandikNo()
    {
        return gumus_sandik_no;
    }

    public int getZumrutSandikNo()
    {
        return zumrut_sandik_no;
    }

    public void setBuyukAgacNo(int no)
    {
        buyuk_agac_no = no;
    }

    public void setKucukAgacNo(int no)
    {
        kucuk_agac_no = no;
    }

    public void setDagNo(int no)
    {
        dag_no = no;
    }

    public void setKucukKayaNo(int no)
    {
        kucuk_kaya_no = no;
    }

    public void setBuyukKayaNo(int no)
    {
        buyuk_kaya_no = no;
    }

    public void setDuvarNo(int no)
    {
        duvar_no = no;
    }

    public void setKusNo(int no)
    {
        kus_no = no;
    }

    public void setAriNo(int no)
    {
        ari_no = no;
    }

    public int getBuyukAgacNo()
    {
        return buyuk_agac_no;
    }

    public int getKucukAgacNo()
    {
        return kucuk_agac_no;
    }

    public int getDagNo()
    {
        return dag_no;
    }

    public int getKucukKayaNo()
    {
        return kucuk_kaya_no;
    }

    public int getBuyukKayaNo()
    {
        return buyuk_kaya_no;
    }

    public int getDuvarNo()
    {
        return duvar_no;
    }

    public int getKusNo()
    {
        return kus_no;
    }

    public int getAriNo()
    {
        return ari_no;
    }

    


    
}
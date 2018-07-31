using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_GO : MonoBehaviour {
    public GameObject goWall;
    public GameObject empty;
    public GameObject goEmpty;
    public GameObject goBlue;
    public GameObject goGreen;
    public GameObject goOrange;
    public GameObject goRed;
    public GameObject goViolet;

    public static int x;
    public static int y;
    private static int lastRandomNumber;
    public static int randX = 0, randY = 0, rand1X = 0, rand1Y = 0 , rand2X = 0, rand2Y = 0;


    GameObject[] objective = new GameObject[3];

    public void Start()
    {
        x = Veribles.BoardRow;
        y = Veribles.BoardColumn;
        CreateWall();
        FillBoard();
        GenerateObjectives(objective);
       
    }



    public  void CreateWall()
    {
        for (int i = 0; i <= x + 1; i++)
        {
            GameObject go = Instantiate(goWall, new Vector3Int(i, 0, 0), Quaternion.identity) ;
            go.name = (i + "_" + 0);
            go = Instantiate(goWall, new Vector3Int(i, y+1, 0), Quaternion.identity);
            go.name = (i + "_" + (y + 1) );
        }
        for (int i = 1; i <= y + 1 ; i++)
        {
            GameObject go = Instantiate(goWall, new Vector3Int(0, i, 0), Quaternion.identity);
            go.name = (0 + "_" + i);
            go = Instantiate(goWall, new Vector3Int(x + 1, i, 0), Quaternion.identity);
            go.name = (x + 1 + "_" + i);
        }
    }



    public void FillBoard()
    {
        randX = Random.Range(1, Veribles.BoardRow + 1);
        randY = Random.Range(1, Veribles.BoardColumn);

        for (; ; )
        {
            rand1X = Random.Range(1, Veribles.BoardRow + 1);
            rand1Y = Random.Range(1, Veribles.BoardColumn);
            if (!(randX == rand1X && randY == rand1Y))
            {
                break;
            }
        }
        for (; ; )
        {
            rand2X = Random.Range(1, Veribles.BoardRow + 1);
            rand2Y = Random.Range(1, Veribles.BoardColumn);
            if (!(randX == rand2X && randY == rand2Y) || !(rand1X == rand2X && rand1Y == rand2Y))
            {
                break;
            }
        }

        GameObject em1 = Instantiate(goEmpty, new Vector3Int(randX, randY, 0), Quaternion.identity);
        GameObject em2 = Instantiate(goEmpty, new Vector3Int(rand1X, rand1Y, 0), Quaternion.identity);
        GameObject em3 = Instantiate(goEmpty, new Vector3Int(rand2X, rand2Y, 0), Quaternion.identity);
        em1.name = randX + "_" + randY;
        em2.name = rand1X + "_" + rand1Y;
        em3.name = rand2X + "_" + rand2Y;

        for (int i = 1; i <= y; i++)
        {
            for (int j = 1; j <= x; j++)
            {
                if (!(j == randX && i == randY) && !(j == rand1X && i == rand1Y) && !(j == rand2X && i == rand2Y))
                {
                    Instantiate(empty, new Vector3Int(j, i, 0), Quaternion.identity);
                }
                else Debug.Log(j + "***" + i);
            }
        }
    }

    void GenerateObjectives(GameObject[] obj)
    {
        int[] xx = new int[3];
        xx[0] = Random.Range(0, 5);

        for (; ; )
        {
            xx[1] = Random.Range(0, 5);
            if (xx[1] != xx[0]) break;
        }
        for (; ; )
        {
            xx[2] = Random.Range(0, 5);
            if (xx[2] != xx[1] && xx[2] != xx[0]) break;
        }
        for (int i = 0; i < Veribles.ObjCount; i++)
        {
            int j = -1;
            if (i == 1) j = 1; if (i == 2) j = 3;
            if (xx[i] == 0)
            {
                obj[i] = Instantiate(goBlue, new Vector3Int(j, 14, 0), Quaternion.identity);
                Main.obj[i] = "Blue";
            }
            else if (xx[i] == 1)
            {
                obj[i] = Instantiate(goGreen, new Vector3Int(j, 14, 0), Quaternion.identity);
                Main.obj[i] = "Green";
            }
            else if (xx[i] == 2)
            {
                obj[i] = Instantiate(goOrange, new Vector3Int(j, 14, 0), Quaternion.identity);
                Main.obj[i] = "Orange";
            }
            else if (xx[i] == 3)
            {
                obj[i] = Instantiate(goRed, new Vector3Int(j, 14, 0), Quaternion.identity);
                Main.obj[i] = "Red";
            }
            else if (xx[i] == 4)
            {
                obj[i] = Instantiate(goViolet, new Vector3Int(j, 14, 0), Quaternion.identity);
                Main.obj[i] = "Violet";
            }
        }
    }
}

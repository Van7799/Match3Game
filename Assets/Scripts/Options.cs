using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public static  string  BoardRow = Veribles.BoardRow + "";
  public static  string BoardColumn = Veribles.BoardRow + "";
  public static  string movesCount = "10";
  public static  string ObjCount = "3";
  public static  string Obj1 = "10";
  public static  string Obj2 = "11";
  public static  string Obj3 = "14";
  public static  string ColorsCount = "5";

    void OnGUI()
    {
        // Make a text field that modifies stringToEdit.
        GUI.Label(new Rect(10, 10, 200, 20), "Board Row");
        BoardRow = GUI.TextField(new Rect(300, 10, 20, 20), BoardRow, 25);

        GUI.Label(new Rect(10, 40, 200, 20), "Board Column");
        BoardColumn = GUI.TextField(new Rect(300, 40, 20, 20), BoardColumn, 25);
        
        GUI.Label(new Rect(10, 70, 200, 20), "Moves Count");
        movesCount = GUI.TextField(new Rect(300, 70, 20, 20), movesCount, 25);

        GUI.Label(new Rect(10, 100, 200, 20), "Objective Count");
        ObjCount = GUI.TextField(new Rect(300, 100, 20, 20), ObjCount, 25);

        GUI.Label(new Rect(10, 130, 200, 20), "Objective 1");
        Obj1 = GUI.TextField(new Rect(300, 130, 20, 20), Obj1, 25);

        GUI.Label(new Rect(10, 160, 200, 20), "Objective 2");
        Obj2 = GUI.TextField(new Rect(300, 160, 20, 20), Obj2, 25);

        GUI.Label(new Rect(10, 190, 200, 20), "Objective 3");
        Obj3 = GUI.TextField(new Rect(300, 190, 20, 20), Obj3, 25);

        GUI.Label(new Rect(10, 220, 200, 20), "Colors Count");
        ColorsCount = GUI.TextField(new Rect(300, 220, 20, 20), ColorsCount, 25);



        if (GUI.Button(new Rect(150, 260, 60, 50), "Submit"))
        {
            try
            {
                    if (int.Parse(BoardRow) >= 7 && int.Parse(BoardRow) <= 10)
                    {
                        Veribles.BoardRow = int.Parse(BoardRow);
                    }
                    if (int.Parse(BoardColumn) >= 7 && int.Parse(BoardColumn) <= 10)
                    {
                        Veribles.BoardColumn = int.Parse(BoardColumn);
                    }

                    Veribles.movesCount = int.Parse(movesCount);

                    if (int.Parse(ObjCount) >= 1 && int.Parse(ObjCount) <= 3)
                    {
                        Veribles.ObjCount = int.Parse(ObjCount);
                    }

                    Veribles.Obj1 = int.Parse(Obj1);
                    Veribles.Obj2 = int.Parse(Obj2);
                    Veribles.Obj3 = int.Parse(Obj3);

                    if (int.Parse(ColorsCount) >= 3 && int.Parse(ColorsCount) <= 5)
                    {
                        Veribles.ColorsCount = int.Parse(ColorsCount);
                    }
                //   SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }
            catch { }
        }

        if (GUI.Button(new Rect(250, 260, 60, 50), "Cancle"))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}

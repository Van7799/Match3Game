using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Veribles : MonoBehaviour {

    public static int BoardRow = 8;
    public static int BoardColumn = 8;
    public static int movesCount = 10;

    public static int ObjCount = 3;
    public static int Obj1 = 10;
    public static int Obj2 = 11;
    public static int Obj3 = 14;
    public static int ColorsCount = 5;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }
	
}

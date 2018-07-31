using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Empty_GO : MonoBehaviour {
    public static float speed = 20.0f;
    public static GameObject goWall;
    public GameObject goBlue;
    public GameObject goGreen;
    public GameObject goOrange;
    public GameObject goRed;
    public GameObject goViolet;
    GameObject go_above;
    public static int emptyCount;
    private static int lastRandomNumber;



    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        emptyCount++;
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log(" Empty " + emptyCount);
        try
        {
            go_above = GameObject.Find(transform.position.x + "_" + (transform.position.y + 1));
            if (go_above.tag == "wall")
            {
                if (emptyCount == 1) { Main.processEnded = true; }
                emptyCount--;
                CreateGO((int)transform.position.x, (int)transform.position.y);
                DestroyImmediate(gameObject);
            }
            else if (go_above.tag != "goEmpty")
            {
                Vector3 current = new Vector3(go_above.transform.position.x, go_above.transform.position.y, 0.0f);
                Vector3 target = gameObject.transform.position;
                if (current.y > target.y)
                {
                    go_above.transform.position = Vector3.MoveTowards(go_above.transform.position, target, speed * Time.deltaTime);
                }
                /*   else if (current.y < target.y)
                   {
                       go_above.transform.position = new Vector3(go_above.transform.position.x, (int)go_above.transform.position.y + 1, 0);
                       gameObject.transform.position = new Vector3Int((int)go_above.transform.position.x, (int)go_above.transform.position.y + 1, 0);
                       go_above.name = go_above.transform.position.x + "_" + go_above.transform.position.y;
                   } */
                else
                {
                    gameObject.transform.position = new Vector3Int((int)go_above.transform.position.x, (int)go_above.transform.position.y + 1, 0);
                    go_above.name = go_above.transform.position.x + "_" + go_above.transform.position.y;
                }
            }
            else if (go_above.tag == "goEmpty")
            {
                try
                {
                    GameObject upLeft = GameObject.Find((go_above.transform.position.x - 1) + "_" + go_above.transform.position.y);
                    if (upLeft.tag != "wall" && upLeft.tag != "goEmpty" && upLeft.tag != "Empty")
                    {
                      //  Vector3 current = new Vector3(upLeft.transform.position.x, upLeft.transform.position.y, 0.0f);
                        //  Vector3 target = gameObject.transform.position;
                        upLeft.transform.position = gameObject.transform.position;
                        upLeft.name = upLeft.transform.position.x + "_" + upLeft.transform.position.y;
                        gameObject.transform.position = new Vector3Int((int)(upLeft.transform.position.x - 1), (int)upLeft.transform.position.y + 1, 0);
                    }
                    else {
                        GameObject upRight = GameObject.Find((go_above.transform.position.x + 1) + "_" + go_above.transform.position.y);
                        if (upRight.tag != "wall" && upRight.tag != "goEmpty" && upLeft.tag != "Empty")
                        {
                         //   Vector3 current = new Vector3(upRight.transform.position.x, upRight.transform.position.y, 0.0f);
                            upRight.transform.position = gameObject.transform.position;
                            upRight.name = upRight.transform.position.x + "_" + upRight.transform.position.y;
                            gameObject.transform.position = new Vector3Int((int)(upRight.transform.position.x + 1), (int)upRight.transform.position.y + 1, 0);
                        }
                        else Destroy(this);
                    }
                    /*  if (current.y > target.y)
                      {
                          upLeft.transform.position = Vector3.MoveTowards(upLeft.transform.position, target, speed * Time.deltaTime);
                      }
                      else
                      {
                          gameObject.transform.position = new Vector3Int((int)upLeft.transform.position.x, (int)upLeft.transform.position.y + 1, 0);
                          upLeft.name = upLeft.transform.position.x + "_" + upLeft.transform.position.y;
                      }*/
                }
                catch
                {
                    
                    // Vector3 target = gameObject.transform.position;
                    // if (current.y > target.y)
                    // {
                    //     upRight.transform.position = Vector3.MoveTowards(upRight.transform.position, target, speed * Time.deltaTime);
                    // }
                    // else
                    //{
                    //   gameObject.transform.position = new Vector3Int((int)upRight.transform.position.x, (int)upRight.transform.position.y + 1, 0);
                    //  upRight.name = upRight.transform.position.x + "_" + upRight.transform.position.y;
                    //}
                }
                finally
                {
                    //Destroy(this.gameObject);
                }
            }
        }
        catch { }
    }



    /// create random gameobject
    public void CreateGO(int x, int y)
    {
            int rand = Random.Range(0, Veribles.ColorsCount);
            if (rand == lastRandomNumber)
            {
                CreateGO(x, y);
                return;
            }
            else
            {
                GameObject goo = Instantiate(chooseGO(rand), new Vector3Int(x, y, 0), Quaternion.identity);
                goo.name = x + "_" + y;
                lastRandomNumber = rand;
            }
    }
    public GameObject chooseGO(int i)
    {
        GameObject go = goViolet;
        if (i == 0) go = goBlue;
        else
        if (i == 1) go = goGreen;
        else
        if (i == 2) go = goOrange;
        else
        if (i == 3) go = goRed;
        return go;
    }

    ////////////
}

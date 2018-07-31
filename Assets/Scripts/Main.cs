using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    private bool isFirstClick;
    bool moveValid = false;
    public static bool allowMove;
    public int x, y;
    public static int moveSpeed = 35;
    public GameObject empty;
    public GameObject hBomb;
    public GameObject vBomb;
    public GameObject Bomb;
    bool firstCheck;
    public static bool processEnded;
    public static string[] obj;
    //List<GameObject> matched;
    GameObject end;
    GameObject go1;
    GameObject go2;
    int firstX = 0, firstY = 0;
    int secondX = 0, secondY = 0;
    int obj1m;
    int obj2m;
    int obj3m;
    int moveCountM;
    // Use this for initialization
    private void Awake()
    {
        x = Veribles.BoardRow;
        y = Veribles.BoardColumn;
        //     matched = new List<GameObject>();
        obj = new string[3];
        processEnded = false;
        allowMove = false;
        isFirstClick = true;
        go1 = new GameObject();
        go2 = new GameObject();
        firstCheck = true;
        GameObject.Find("ScoreText").GetComponent<TextMesh>().text = "" + Veribles.movesCount;
        obj1m = Veribles.Obj1;
        obj2m = Veribles.Obj2;
        obj3m = Veribles.Obj3;
        moveCountM = Veribles.movesCount;
    }
    void Start()
    {
        end = GameObject.Find("End");
        end.SetActive(false);
        GameObject.Find("obj1T").GetComponent<TextMesh>().text = "" + obj1m;
        GameObject.Find("obj2T").GetComponent<TextMesh>().text = "" + obj2m;
        GameObject.Find("obj3T").GetComponent<TextMesh>().text = "" + obj3m;

      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Empty_GO.emptyCount == 0 && firstCheck) {
            hardCheck();
            firstCheck = false;
        } else
        if (processEnded) {
            hardCheck();
       //     Debug.Log(matched.Count);
        }

        if (Input.GetMouseButtonDown(0) && Empty_GO.emptyCount == 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                string tag = hit.collider.gameObject.tag;
                if (hit.collider.name == "Restart") { Empty_GO.emptyCount = 0; SceneManager.LoadScene("Main", LoadSceneMode.Single);
                }
                if (hit.collider.name == "Close") { SceneManager.LoadScene("Menu", LoadSceneMode.Single); }
                if (hit.collider != null && (tag == "Blue" || tag == "Green"  || tag == "Orange" || tag == "Red" || tag == "Violet" || tag == "vBomb" || tag == "hBomb" || tag == "Bomb"))
                {
                    if (isFirstClick) {
                        firstX = (int)hit.collider.gameObject.transform.position.x;
                        firstY = (int)hit.collider.gameObject.transform.position.y;
                        isFirstClick = false;
                    }
                    else {
                        secondX = (int)hit.collider.gameObject.transform.position.x;
                        secondY = (int)hit.collider.gameObject.transform.position.y;
                        if ((Mathf.Abs(secondX - firstX) == 1 && secondY == firstY) || (Mathf.Abs(secondY - firstY) == 1 && secondX == firstX))
                        {
                            isFirstClick = true;
                            go1 = GameObject.Find(firstX + "_" + firstY);
                            go2 = GameObject.Find(secondX + "_" + secondY);
                            if (go1.tag != go2.tag)
                            {
                                allowMove = true;
                            }
                        }
                        else isFirstClick = true;
                    }
                }
            }
        }
        if (allowMove)
        {
            moveValid = false;
            Vector3 VGo1 = go1.transform.position;
            Vector3 VGo2 = go2.transform.position;
            go1.transform.position = Vector3.MoveTowards(VGo1, new Vector3(secondX, secondY), moveSpeed * Time.deltaTime);
            go2.transform.position = Vector3.MoveTowards(VGo2, new Vector3(firstX, firstY), moveSpeed * Time.deltaTime);
            if (VGo1.x == secondX && VGo1.y == secondY && VGo2.x == firstX && VGo2.y == firstY) {
                allowMove = false;
                isFirstClick = true;
                Debug.Log("ended");
                go1.name = secondX + "_" + secondY;
                go2.name = firstX + "_" + firstY;
                List<Point> matched = new List<Point>();
                matched.AddRange(check_Objects(go1));
                matched.AddRange(check_Objects(go2));
                if (go1.tag == "vBomb" || go1.tag == "hBomb" || go1.tag == "Bomb" || go2.tag == "vBomb" || go2.tag == "hBomb" || go2.tag == "Bomb")
                    moveValid = true;
                if (moveValid)
                {
                    moveCountM--;
                    GameObject.Find("ScoreText").GetComponent<TextMesh>().text = "" + moveCountM;
                    if (moveCountM == 0) {
                        end.SetActive(true);
                        GameObject.Find("LoseWin").GetComponent<TextMesh>().text = "You Lose :(";
                    }
                    destroyGO(matched);
                }

                else 
                {
                    go1.transform.position = new Vector3(firstX, firstY, 0.0f);
                    go2.transform.position = new Vector3(secondX, secondY, 0.0f);
                    go1.name = firstX + "_" + firstY;
                    go2.name = secondX + "_" + secondY;
                    allowMove = false;
                    /*    go1.transform.position = Vector3.MoveTowards(VGo1, new Vector3(secondX, secondY), moveSpeed * Time.deltaTime);
                        go2.transform.position = Vector3.MoveTowards(VGo2, new Vector3(firstX, firstY), moveSpeed * Time.deltaTime);
                        if (VGo1.x == firstX && VGo1.y == firstY && VGo2.x == secondX && VGo2.y == secondY)
                        {
                            Debug.Log("hasanq");
                            allowMove = false;
                            isFirstClick = true;
                            Debug.Log("ended");
                            go1.name = secondX + "_" + secondY;
                            go2.name = firstX + "_" + firstY;
                        }*/
                }
                /*
                                if (!check_Objects(go1, go2)) {
                                    int se = secondX; int see = secondY;
                                    secondX = firstX; secondY = firstY;
                                    firstX = se; secondX = see;
                                    go1.name = secondX + "_" + secondY;
                                    go2.name = firstX + "_" + firstY;
                                    moveValid = false;
                                };
                  */
            };
        }

    }
    public List<Point> check_Objects(GameObject go1)
    {
        List<Point> goHorRight = new List<Point>();
        List<Point> goHorLeft = new List<Point>();
        List<Point> goVertUp = new List<Point>();
        List<Point> goVertDown = new List<Point>();
        bool first_iteration = true;
        bool areBomb = false;
        List<Point> matched = new List<Point>();

        if (go1.tag == "vBomb" || go1.tag == "hBomb" || go1.tag == "Bomb")
        {
            moveValid = true;
            if (go1.tag == "vBomb")
            {
                for (int i = 1; i <= y; i++)
                {
                    GameObject ggg = GameObject.Find(go1.transform.position.x + "_" + i);
                    if (ggg.tag == "hBomb" || ggg.tag == "Bomb") {
                        matched.AddRange(check_Objects(ggg));
                    }
                    else if (!(go1.transform.position.x == Create_GO.randX && i == Create_GO.randY) && !(go1.transform.position.x == Create_GO.rand1X && i == Create_GO.rand1Y) && !(go1.transform.position.x == Create_GO.rand2X && i == Create_GO.rand2Y))
                        matched.Add(new Point((int)ggg.transform.position.x, (int)ggg.transform.position.y));
                }
            }
            else
            if (go1.tag == "hBomb")
            {
                for (int i = 1; i <= x; i++)
                {
                    GameObject ggg = GameObject.Find(i + "_" + go1.transform.position.y);
                    if ((ggg.tag == "vBomb" || ggg.tag == "Bomb"))
                    {
                        matched.AddRange(check_Objects(ggg));
                    }
                    else if (!(i == Create_GO.randX && go1.transform.position.y == Create_GO.randY) && !(i == Create_GO.rand1X && go1.transform.position.y == Create_GO.rand1Y) && !(i == Create_GO.rand2X && go1.transform.position.y == Create_GO.rand2Y))
                        matched.Add(new Point((int)ggg.transform.position.x, (int)ggg.transform.position.y));
                }
            }
            else if (go1.tag == "Bomb")
            {
                int xx = (int)(go1.transform.position.x - 1);
                int yy = (int)(go1.transform.position.y + 1);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        GameObject ggg = GameObject.Find((xx + j) + "_" + (yy - i));
                        if (!((int)ggg.transform.position.x == Create_GO.randX && ggg.transform.position.y == Create_GO.randY) && !(ggg.transform.position.x == Create_GO.rand1X && ggg.transform.position.y == Create_GO.rand1Y) && !(ggg.transform.position.x == Create_GO.rand2X && ggg.transform.position.y == Create_GO.rand2Y))
                            if (ggg.tag == "Blue" || ggg.tag == "Green" || ggg.tag == "Orange" || ggg.tag == "Red" || ggg.tag == "Violet" || ggg.tag == "vBomb" || ggg.tag == "hBomb" || ggg.tag == "Bomb")
                        {
                            if ((ggg.tag == "vBomb" || ggg.tag == "hBomb" || ggg.tag == "Bomb") && (ggg.name != go1.name && ggg.tag != go1.tag))
                            {
                                matched.AddRange(check_Objects(ggg));
                            }
                            else 
                                matched.Add(new Point((int)ggg.transform.position.x, (int)ggg.transform.position.y));
                        }
                    }
            }

        }
        else
        {
            /// var dis = gObjects.GroupBy(x => x.transform.position.x).Select(y => y.FirstOrDefault()).ToList();
            string bomb = "";
            first_iteration = true;
            checkAroundHorLeft(go1, goHorLeft, ref first_iteration, ref areBomb);
            first_iteration = true;
            checkAroundHorRight(go1, goHorRight, ref first_iteration, ref areBomb);
            goHorLeft.AddRange(goHorRight);

            var dis = goHorLeft.GroupBy(x => new { x.a, x.b}).Select(y => y.FirstOrDefault()).ToList();
            if (dis.Count < 3)
            {
                goHorLeft = new List<Point>();
                goHorRight = new List<Point>();
            }
            else
            {
                matched.AddRange(dis);
                moveValid = true;
           //     destroyList(dis);
                /// Match 4 or more
                if (dis.Count > 3 && !areBomb )
                {
                    bomb = "hor";
                }
            }
            first_iteration = true;
            checkAroundVertUp(go1, goVertUp, ref first_iteration, ref areBomb);
            first_iteration = true;
            checkAroundVertDown(go1, goVertDown, ref first_iteration, ref areBomb);
            goVertUp.AddRange(goVertDown);
            dis = goVertUp.GroupBy(x => new { x.a, x.b }).Select(y => y.FirstOrDefault()).ToList();
            if (dis.Count < 3) {
                goVertUp = new List<Point>();
                goVertDown = new List<Point>();
            }
            else
            {
                matched.AddRange(dis);
                moveValid = true;
                //  destroyList(dis);
                if (dis.Count > 3 && bomb == "" && !areBomb)
                {
                    bomb = "vert";
                }
            }
            

            if (areBomb) bomb = "Bomb";
            ////// extras
            if (bomb != "")
            {
                moveValid = true;
                if (bomb == "hor")
                {
                    GameObject vh = Instantiate(vBomb, new Vector3Int((int)go1.transform.position.x, (int)go1.transform.position.y, 0), Quaternion.Euler(180,180,0));
                    vh.name = (int)go1.transform.position.x + "_" + (int)go1.transform.position.y;
                }
                else
                if (bomb == "vert")
                {
                    GameObject vb = Instantiate(hBomb, new Vector3Int((int)go1.transform.position.x, (int)go1.transform.position.y, 0), Quaternion.Euler(180, 180, 0));
                    vb.name = (int)go1.transform.position.x + "_" + (int)go1.transform.position.y;
                }
                else
                if (bomb == "Bomb")
                {
                    GameObject vb = Instantiate(Bomb, new Vector3Int((int)go1.transform.position.x, (int)go1.transform.position.y, 0), Quaternion.Euler(180, 180, 0));
                    vb.name = (int)go1.transform.position.x + "_" + (int)go1.transform.position.y;
                }
                matched.RemoveAll(x =>  x.a == (int)go1.transform.position.x && x.b == (int)go1.transform.position.y );
             //  matched.Remove(new Point((int)go1.transform.position.x, (int)go1.transform.position.y));
                
                Destroy(go1);
                go1 = null;
            } 
        }
        return matched;
    }

    public void checkAroundHorLeft(GameObject go, List<Point> goHor, ref bool count, ref bool areBombb) {
        try
        {
            GameObject left = GameObject.Find((go.transform.position.x - 1) + "_" + go.transform.position.y);
            if (left.tag == go.tag &&  left.tag != "goEmpty")
            {
                goHor.Add (new Point((int)left.transform.position.x, (int)left.transform.position.y));
                goHor.Add(new Point((int)go.transform.position.x, (int)go.transform.position.y));
                // goHor.Add(go);
                //square match
                //    **  .
                //    **  .
                //    . . .
                if (count && !areBombb)
                {
                    GameObject goYp1, leftYp1;
                    goYp1 = GameObject.Find(go.transform.position.x + "_" + (go.transform.position.y + 1));
                    leftYp1 = GameObject.Find(left.transform.position.x + "_" + (left.transform.position.y + 1));
                    if (goYp1.tag == go.tag && leftYp1.tag == left.tag)
                    {
                        goHor.Add(new Point((int)leftYp1.transform.position.x, (int)leftYp1.transform.position.y));
                        goHor.Add(new Point((int)goYp1.transform.position.x, (int)goYp1.transform.position.y));
                        Destroy(goYp1);
                        Destroy(leftYp1);
                        Destroy(left);
                        Destroy(go);
                        areBombb = true;
                        return;
                    }
                }
 
                if (areBombb && !count) return;
                if(!count) Destroy(left); 
                count = false;
                checkAroundHorLeft(left, goHor, ref count, ref areBombb);
            }
            else return ;
        }
        catch {
            return ;
        }
    }
    public  GameObject checkAroundHorRight(GameObject go, List<Point> goHor, ref bool count, ref bool areBombb)
    {
        try
        {
            GameObject right = GameObject.Find((go.transform.position.x + 1) + "_" + go.transform.position.y);
            if (right.tag == go.tag && right.tag != "goEmpty")
            {
                goHor.Add(new Point((int)right.transform.position.x, (int)right.transform.position.y));
                goHor.Add(new Point((int)go.transform.position.x, (int)go.transform.position.y));
                //   goHor.Add(go);

                if (count && !areBombb)
                {
                    //square match
                    //    .  .  .
                    //    .  *  *
                    //    .  *  *
                    GameObject goYm1, rightYm1;
                    goYm1 = GameObject.Find(go.transform.position.x + "_" + (go.transform.position.y - 1));
                    rightYm1 = GameObject.Find(right.transform.position.x + "_" + (right.transform.position.y - 1));
                    if (goYm1.tag == go.tag && rightYm1.tag == right.tag)
                    {
                        goHor.Add(new Point((int) goYm1.transform.position.x, (int)goYm1.transform.position.y));
                        goHor.Add(new Point((int) rightYm1.transform.position.x, (int)rightYm1.transform.position.y));
                        Destroy(goYm1);
                        Destroy(rightYm1);
                        Destroy(right);
                        Destroy(go);
                        areBombb = true;
                        return null;
                    }
                }
            //    Destroy(right);
                if (areBombb) return null;
                if (!count) Destroy(right);
                count = false;
                checkAroundHorRight(right, goHor, ref count, ref areBombb);
            }
            else return right;
        }
        catch(Exception e) {
            Debug.Log(e.Message);
        }
        return null;
    }

    public void checkAroundVertUp(GameObject go, List<Point> goVert,ref bool count, ref bool areBombb)
    {
        try
        {
            GameObject up = GameObject.Find((go.transform.position.x) + "_" + (go.transform.position.y + 1));
            if (up.tag == go.tag && up.tag != "goEmpty")
            {
                goVert.Add(new Point((int) up.transform.position.x, (int) up.transform.position.y)) ;
                goVert.Add(new Point((int) go.transform.position.x, (int) go.transform.position.y));
                //    goVert.Add(go);

                if (count && !areBombb)
                {
                    //square match
                    //    .  *  *
                    //    .  *  *
                    //    .  .  .
                    GameObject goXp1, rightXp1;
                    goXp1 = GameObject.Find((go.transform.position.x + 1) + "_" + go.transform.position.y);
                    rightXp1 = GameObject.Find((up.transform.position.x + 1) + "_" + up.transform.position.y);
                    if (goXp1.tag == go.tag && rightXp1.tag == up.tag)
                    {
                        goVert.Add(new Point((int)goXp1.transform.position.x, (int) goXp1.transform.position.y));
                        goVert.Add(new Point((int)rightXp1.transform.position.x, (int)rightXp1.transform.position.y));
                        Destroy(goXp1);
                        Destroy(rightXp1);
                        Destroy(up);
                        Destroy(go);
                        areBombb = true;
                        return;
                    }
                }
                //  Destroy(up);
                if (areBombb) return;
                if (!count) Destroy(up);
                count = false;
                checkAroundVertUp(up, goVert, ref count, ref areBombb);
            }
            else return ;
        }
        catch
        {
            return;
        }
    }

    public void checkAroundVertDown(GameObject go, List<Point> goVert,ref bool count, ref bool areBombb)
    {
        try
        {
            GameObject down = GameObject.Find((go.transform.position.x) + "_" + (go.transform.position.y - 1));
            if (down.tag == go.tag && down.tag != "goEmpty")
            {
                goVert.Add(new Point((int) down.transform.position.x, (int) down.transform.position.y));
                goVert.Add(new Point((int)go.transform.position.x, (int)go.transform.position.y));
                //      goVert.Add(go);

                if (count && !areBombb)
                {
                    //square match
                    //    .  .  .
                    //    *  *  .
                    //    *  *  .
                    GameObject goXm1, rightXm1;
                    goXm1 = GameObject.Find((go.transform.position.x - 1) + "_" + go.transform.position.y);
                    rightXm1 = GameObject.Find((down.transform.position.x - 1) + "_" + down.transform.position.y);
                    if (goXm1.tag == go.tag && rightXm1.tag == down.tag)
                    {
                        goVert.Add(new Point((int)goXm1.transform.position.x, (int)goXm1.transform.position.y));
                        goVert.Add(new Point((int)rightXm1.transform.position.x, (int)rightXm1.transform.position.y));
                        Destroy(goXm1);
                        Destroy(rightXm1);
                        Destroy(down);
                        Destroy(go);
                        areBombb = true;
                        return ;
                    }
                }
                if (areBombb) return ;
                if (!count) Destroy(down);
                count = false;
                checkAroundVertDown(down, goVert, ref count, ref areBombb);
            }
            else return ;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        return ;
    }


    /// using in start application to collect colors
    void hardCheck() {
        bool first_iteration = true;
        bool areBomb = false;
        List<Point> goHorRight = new List<Point>();
        List<Point> goVertDown = new List<Point>();
        Point go1p ;
        List<Point> matched = new List<Point>();
        string bomb = "";
        matched.Clear();
        for (int i = y; i >= 1; i--)
        {
            for (int j = 1; j <= x; j++)
            {
                
                goHorRight = new List<Point>();
                goVertDown =  new List<Point>();
                bomb = "";
                areBomb = false;
                first_iteration = true;
                if (matched.Count > 1000) {
                    matched.Clear(); Debug.Log("Hajox");
                }
                try
                {
                    GameObject go1 = GameObject.Find(j + "_" + i);
                    go1p = new Point((int)go1.transform.position.x, (int)go1.transform.position.y);
                    var dis = goHorRight.GroupBy(x => new { x.a, x.b }).Select(y => y.FirstOrDefault()).ToList();
                    if (dis.Count < 3)
                    {
                        goHorRight.Clear();
                        dis.Clear();
                    }
                    else
                    {
                        matched.AddRange(dis);
                  //      foreach (Point gggg in matched) {
                    //        Debug.Log(gggg.a + "(((((((((" + gggg.b);
                      //  }
                        //     destroyList(dis);
                        /// Match 4 or more
                        if (dis.Count > 3 && !areBomb)
                        {
                            bomb = "hor";
                        }
                     //   j = (int)(goEnd.transform.position.x);
                        dis.Clear();
                    }
                    first_iteration = true;


                    checkAroundVertDown(go1, goVertDown, ref first_iteration, ref areBomb);
                    dis = goVertDown.GroupBy(x => new { x.a, x.b }).Select(y => y.FirstOrDefault()).ToList();
                    //List<Point> dis = goVertDown;
                    if (dis.Count < 3)
                    {
                        goVertDown.Clear();
                    }
                    else
                    {
                        matched.AddRange(dis);
                        //  destroyList(dis);
                        if (dis.Count > 3 && !areBomb )
                        {
                            bomb = "vert";
                        }
                    }
                    if (areBomb) bomb = "Bomb";
                    ////// extras
                    if (bomb != "")
                    {
                        matched.RemoveAll(x => (x.a == (int)go1.transform.position.x && x.b == (int)go1.transform.position.y));
                        Destroy(go1);
                        if (bomb == "hor")
                        {
                            GameObject vh = Instantiate(vBomb, new Vector3Int(go1p.a, go1p.b, 0), Quaternion.Euler(180, 180, 0));
                            vh.name = (int)go1p.a + "_" + (int)go1p.b;
                        }
                        else
                        if (bomb == "vert")
                        {
                            GameObject vb = Instantiate(hBomb, new Vector3Int(go1p.a, go1p.b, 0), Quaternion.Euler(180, 180, 0));
                            vb.name = (int)go1p.a + "_" + (int)go1p.b;
                        }
                        else
                        if (bomb == "Bomb")
                        {
                            GameObject vb = Instantiate(Bomb, new Vector3Int((int)go1p.a, (int)go1p.b, 0), Quaternion.Euler(180, 180, 0));
                            vb.name = (int)go1p.a + "_" + (int)go1p.b;
                        }
                     //   destroyGO();
                    } 
                    else
                        //      if (matched.Count() > 0)
                        //    {
                        //   destroyGO();
                        //  }
                        foreach (Point gggg in matched)
                        {
                            Debug.Log(gggg.a + ")))))))(((" + gggg.b);
                        }
                    
                }
                catch(Exception e) { Debug.Log(e.Message); }  
            }
        }
        var diss = matched.GroupBy(x => new { x.a, x.b }).Select(y => y.FirstOrDefault()).ToList();
        destroyGO(diss);
       // destroyGO();
    }





    void destroyGO(List<Point> match)
    {

        var dis = match.GroupBy(x => new { x.a, x.b }).Select(y => y.FirstOrDefault()).ToList();
        foreach (Point p in dis)
        {
            try
            {
                GameObject dd = GameObject.Find(p.a + "_" + p.b);
                for (int i = 0; i < Veribles.ObjCount; i++) {
                    if (dd.tag == obj[i]) {
                        if (i == 0) { if (obj1m > 0) obj1m--; }
                        if (i == 1) { if (obj2m > 0) obj2m--; }
                        if (i == 2) { if (obj3m > 0) obj3m--; }
                        GameObject.Find("obj1T").GetComponent<TextMesh>().text = "" + obj1m;
                        GameObject.Find("obj2T").GetComponent<TextMesh>().text = "" + obj2m;
                        GameObject.Find("obj3T").GetComponent<TextMesh>().text = "" + obj3m;
                    }
                }

                Destroy(dd);
                Instantiate(empty, new Vector3Int(p.a, p.b, 0), Quaternion.identity);
            }
            catch { }
        }
        if (obj1m <= 0 && obj2m <= 0 && obj3m <= 0) {
            end.SetActive(true);
            GameObject.Find("LoseWin").GetComponent<TextMesh>().text = "You Won :)";
         

        }
        processEnded = false;
        match.Clear();
        //match = new List<Point>();
    }

    /*
    void ListDest() {
        try {
            foreach (Point p in match)
            {
                Destroy(GameObject.Find(p.a + "_" + p.b));
            }
        } catch (Exception e) { Debug.Log(e.Message); }

    }
    */

    


    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 60, 30), "Menu"))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        if (GUI.Button(new Rect(80, 10, 60, 30), "Restart"))
        {
            Empty_GO.emptyCount = 0;
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
}


public class Point{
    public int a { get; set; }
    public int b { get; set; }

    public Point(int a, int b) {
        this.a = a;
        this.b = b;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.name == "Start")
                {
                    Empty_GO.emptyCount = 0;
                    SceneManager.LoadScene ("Main", LoadSceneMode.Single);
                    return;
                }
                if (hit.collider.name == "Settings")
                {
                    SceneManager.LoadScene("Options", LoadSceneMode.Single);
                    return;
                }
            }
        }
    }
}

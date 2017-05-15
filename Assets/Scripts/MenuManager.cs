using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject[] credit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneLoad(string scene){
        SceneManager.LoadScene(scene);
    }

    public void Credit(bool val)
    {
        if (val)
        {
            credit[0].SetActive(true);
            credit[1].SetActive(false);
        }
        else
        {
            credit[0].SetActive(false);
            credit[1].SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Button : MonoBehaviour {
    public GameObject menucont;
    public MenuController mc;

    // Use this for initialization
    void Start () {
        menucont = GameObject.Find("MenuController");
        mc = menucont.GetComponent<MenuController>();

    }
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.Rotate(new Vector3(0,0,2));

	}
    private void OnMouseDown()
    {
        if (gameObject.name == "Play")
        {
            SceneManager.LoadScene("Game");
        }
        if (gameObject.name == "Restart")
        {
            mc.isLose = false;
        }
    }
}

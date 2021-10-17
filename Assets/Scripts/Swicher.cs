using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swicher : MonoBehaviour {
    public GameObject menuCanvas;
    public GameObject loseCanvas;
    public GameObject menucont;
    public MenuController mc;
    public Text highScore;

    // Use this for initialization
    void Start () {
        highScore.text = PlayerPrefs.GetInt("Hscore").ToString();
        Debug.Log("Current: " + PlayerPrefs.GetInt("Hscore").ToString());
        menucont = GameObject.Find("MenuController");
        mc = menucont.GetComponent<MenuController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (mc.isLose == true)
        {
            menuCanvas.gameObject.SetActive(false);
            loseCanvas.gameObject.SetActive(true);
        }
        else
        {
            menuCanvas.gameObject.SetActive(true);
            loseCanvas.gameObject.SetActive(false);
        }

    }
}

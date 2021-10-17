using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Knife_Spawn : MonoBehaviour
{

    public GameObject knife;
    public GameObject planetSystem;
    public bool isKnife;
    public bool mustNew;
    public GameObject parentObject;
    public Text CScore;
    public int currScore;
    public MenuController mc;
    public GameObject menucont;
    public float fireRate;
    private float nextTimeToFire = 0f;
    public int ammoMax;
    public int ammo;

    // Use this for initialization
    void Start()
    {
        ammo = ammoMax;
        currScore = 0;
        isKnife = true;
        menucont = GameObject.Find("MenuController");
        mc = menucont.GetComponent<MenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            isKnife = false;
            ammo -= 1;
            planetSystem.GetComponent<PlanetSystem>().AmmoUpdate();



        }
        if (isKnife == false)
        {
            Debug.Log("launch");
            GameObject childObject = Instantiate(knife, transform.position, transform.rotation);
            childObject.transform.parent = parentObject.transform;
            childObject.transform.localScale = new Vector3(1, 1, 1);
            isKnife = true;
        }
        //CScore.text = currScore.ToString();
        if(ammo < 0)
        {
            YouLoose();
        }
    }
    public void YouLoose()
    {
        Debug.Log("You Lose");
        if (currScore >= PlayerPrefs.GetInt("Hscore"))
        {
            PlayerPrefs.SetInt("Hscore", currScore);
            Debug.Log("New: " + PlayerPrefs.GetInt("Hscore").ToString());
        }
        mc.isLose = true;
        SceneManager.LoadScene("Menu");
    }
}

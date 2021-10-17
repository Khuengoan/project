using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    
    public bool isLose;

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectsWithTag("Mcont").Length == 1)
            {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
                
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

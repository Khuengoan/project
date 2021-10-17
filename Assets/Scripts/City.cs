using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class City : MonoBehaviour
{
    private SpriteResolver spriteResolver;
    private GameObject planet;
    public int WreckVariants;
    public int CityVariants;
    // Start is called before the first frame update
    void Start()
    {
        spriteResolver = gameObject.GetComponent<SpriteResolver>();
        planet = transform.parent.gameObject;

        spriteResolver.SetCategoryAndLabel("City", Random.Range(1, CityVariants + 1).ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Kill()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spriteResolver.SetCategoryAndLabel("Wreck", Random.Range(1,WreckVariants + 1).ToString()); 
        planet.GetComponent<Wood_Rotate>().cityNumber -= 1;
        planet.GetComponent<Wood_Rotate>().KillCity();

        //particle system
        GameObject ParticleSystemGameObject = gameObject.transform.Find("Particle System").gameObject;
        ParticleSystemGameObject.SetActive(true);
        ParticleSystemGameObject.transform.localPosition = new Vector3(Random.Range(-0.13f, 0.13f), 0.12f);
        var main = ParticleSystemGameObject.GetComponent<ParticleSystem>().main;
        main.startLifetime = Random.Range(0f, 1.5f);
    }
}

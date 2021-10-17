using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Knife : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    public bool onBarrel;
    public Knife_Spawn spawn;
    private bool destroy;

    // Use this for initialization
    void Start()
    {
        destroy = false;
        transform.GetComponentInChildren<Light2D>().enabled = false;
        onBarrel = false;
        spawn = GameObject.Find("SpawnPoint").GetComponent<Knife_Spawn>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && onBarrel == false)
        {
            rb.velocity = new Vector3(0, speed, 0);
            transform.GetComponentInChildren<Light2D>().enabled = true;
            transform.GetComponentInChildren<ParticleSystem>().Play();

        }
        if (transform.Find("Explosion").GetComponentInChildren<ParticleSystem>().IsAlive() == false && destroy)
        {
            Destroy(gameObject);
        }

        KnifeSelfDestroy();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("hit");
        if (other.gameObject.tag == "Barrel")
        {

            spawn.currScore++;
            gameObject.transform.SetParent(other.transform);
            rb.velocity = Vector3.zero;
            this.onBarrel = true;
            
            DestroyMissle();
        }
        if (other.gameObject.tag == "City")
        {
            spawn.currScore++;
            other.gameObject.GetComponent<City>().Kill();
            DestroyMissle();
            
        }

        //lose when knife hit itself

        /*
        if (other.gameObject.tag == "Knife")
        {
            rb.bodyType = RigidbodyType2D.Static;
            spawn.YouLoose();
        }
        */
        destroy = true;
        
    }

    private void DestroyMissle()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        transform.Find("Explosion").gameObject.SetActive(true);
        gameObject.GetComponentInChildren<Light2D>().intensity = Mathf.Lerp(7, 0, 0.1f);
    }


    //self destroy if knife went too far
    private void KnifeSelfDestroy()
    {
        if(transform.position.y > 20)
        { Destroy(gameObject); }
    }


}




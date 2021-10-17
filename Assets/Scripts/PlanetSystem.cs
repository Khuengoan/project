using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class PlanetSystem : MonoBehaviour
{
    public int totalCities;
    public bool shouldChange;
    public GameObject WarpDrive;
    public GameObject[] planetArray;
    public GameObject ResourceCounter;
    public GameObject AmmoCounter;
    public GameObject Base;
    public Light2D globalLight;
    public Light2D sun;
    public GameObject[] planetVarients;
    public Color sunColor1;
    public Color sunColor2;
    private int resource;

    void Awake()
    {
        totalCities = 0;
        shouldChange = false;
        planetArray = new GameObject[1];
        InstantiatePlanet();
    }

    void Update()
    {
        if (totalCities == 0 && shouldChange)
        {
            ChangePlanetSystem();
        }
    }
    void ChangePlanetSystem()
    {
        
        shouldChange = false;
        StartCoroutine(LerpMoveBase(2));
    }
    private void WarpDriveOn()
    {
        WarpDrive.SetActive(true);
        transform.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }
    private void WarpDriveOff()
    {
        WarpDrive.SetActive(false);
        Invoke("InstantiatePlanet", 0.5f);

    }
    private void InstantiatePlanet()
    {
        GameObject planet;
        planet = planetVarients[Random.Range(0, planetVarients.Length)];
        planetArray[0] = Instantiate(planet, transform);
        planetArray[0].transform.localPosition = Vector3.zero;
        sun.intensity = Random.Range(2f, 5f);
    }
    public void ResourceUpdate(int amount)
    {
        resource += amount;
        int ammo = Base.GetComponentInChildren<Knife_Spawn>().ammo;
        int ammoMax = Base.GetComponentInChildren<Knife_Spawn>().ammoMax;
        if (ammo < ammoMax)
        {
            Base.GetComponentInChildren<Knife_Spawn>().ammo += amount;
            AmmoUpdate();
        }
    }
    public void AmmoUpdate()
    {
        int ammo = Base.GetComponentInChildren<Knife_Spawn>().ammo;
        int ammoMax = Base.GetComponentInChildren<Knife_Spawn>().ammoMax;
        AmmoCounter.GetComponent<Text>().text = ammo.ToString();
        ResourceCounter.GetComponent<Text>().text = resource.ToString();
    }
    IEnumerator LerpWarp(float overTime)
    {
        WarpDriveOn();
        foreach (GameObject planetInArray in planetArray)
        {
            planetInArray.GetComponent<Wood_Rotate>().KillPlanet();
        }
        overTime = overTime / 2;
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            ParticleSystemRenderer[] particleSystemRenderers = WarpDrive.GetComponentsInChildren<ParticleSystemRenderer>();
            foreach (ParticleSystemRenderer particleSystemRenderer in particleSystemRenderers)
            {
                particleSystemRenderer.velocityScale = Mathf.Lerp(0, 0.08f, (Time.time - startTime) / overTime);
                globalLight.intensity = Mathf.Lerp(0.6f, 1.3f, (Time.time - startTime) / overTime);
            }
            yield return null;
        }
        startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            ParticleSystemRenderer[] particleSystemRenderers = WarpDrive.GetComponentsInChildren<ParticleSystemRenderer>();
            foreach (ParticleSystemRenderer particleSystemRenderer in particleSystemRenderers)
            {
                particleSystemRenderer.velocityScale = Mathf.Lerp(0.08f, 0, (Time.time - startTime) / overTime);
                globalLight.intensity = Mathf.Lerp(1.3f, 0.6f, (Time.time - startTime) / overTime);
            }
            yield return null;
        }

    }
    IEnumerator LerpMoveBase(float overTime)
    {
        

        Vector3 positionOG = new Vector3(0, Base.transform.position.y);
        overTime = overTime / 2;
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            //Base.transform.position = new Vector3(0, Mathf.Lerp(Base.transform.position.y, Base.transform.position.y - 1, (Time.time - startTime) / overTime));
            Base.transform.position= Vector3.Slerp(Base.transform.position, positionOG - 3 * Vector3.up, (Time.time - startTime) / overTime);
            yield return null;
        }
        yield return StartCoroutine(LerpWarp(3f));
        WarpDriveOff();
        startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            //Base.transform.position = new Vector3(0, Mathf.Lerp(Base.transform.position.y, Base.transform.position.y - 1, (Time.time - startTime) / overTime));
            Base.transform.position = Vector3.Slerp(Base.transform.position, positionOG, (Time.time - startTime) / overTime);
            yield return null;
        }

    }
}

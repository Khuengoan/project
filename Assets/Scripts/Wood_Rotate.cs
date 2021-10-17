using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class Wood_Rotate : MonoBehaviour
{
    public GameObject wood;
    public GameObject city;
    private PlanetSystem planetSystem;
    public float minSpeed;
    public float maxSpeed;
    public float minSize;
    public float maxSize;
    public int minCityNumber;
    public int maxCityNumber;
    public int cityNumber;

    private float angle;
    private int angleNumber;
    private float radius;
    private float speed;
    private float _angleNumber = 0;
    private float[] angleArray;

    private void Awake()
    {
        //Value initialize
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size);
        planetSystem = transform.parent.GetComponent<PlanetSystem>();
        radius = gameObject.GetComponent<CircleCollider2D>().radius - 0.05f;
        angleNumber = Mathf.FloorToInt((radius * transform.localScale.x * 2 * Mathf.PI) / 0.33f);
        cityNumber = Random.Range(minCityNumber, maxCityNumber);
        speed = Random.Range(minSpeed, maxSpeed);
        Debug.Log("angleNumber : " + angleNumber);
        angleArray = new float[angleNumber];

        if(cityNumber > angleNumber)
        {
            cityNumber = angleNumber-1;
        }

        //Create cities
        for (int i = 0; i < cityNumber; i++)
        {
            //angle = Random.Range(0, 2 * Mathf.PI);
            _angleNumber = Random.Range(0, angleNumber + 1);
            while (IsInArray())
            {
                _angleNumber = Random.Range(0, angleNumber + 1);
            }
            angleArray[i] = _angleNumber;
            angle = (_angleNumber / angleNumber) * 2 * Mathf.PI;
            GameObject cityTemp = Instantiate(city, transform.position, Quaternion.identity);
            cityTemp.transform.SetParent(transform);
            cityTemp.transform.localRotation = Quaternion.Euler(0, 0, -90 + angle / Mathf.PI * 180);
            cityTemp.transform.localPosition = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }

        //update planet system
        planetSystem.totalCities += cityNumber;
        planetSystem.shouldChange = true;
    }

    public void CreateKinfe()
    {

        /*
        a = Random.Range(-0.495f, 0.495f);
        c = 0.495f;
        b = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(a, 2));
        y = Mathf.Sin(a / c) * 180 / Mathf.PI;
        y = -y;
        */


        /*
        GameObject child = Instantiate(knife, transform.position, transform.rotation);
        child.transform.parent = wood.transform;
        child.transform.Translate(Vector3.forward * -a);
        child.transform.Translate(Vector3.right * -b);
        child.transform.localEulerAngles = new Vector3(0, y, 0);
        child.transform.localScale = new Vector3(2, 2, 2);
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero;
        */
    }

    private void Start()
    {
        /*  for (int i=0; i<3; i++)
            {
                CreateKinfe();
            }*/

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        wood.transform.Rotate(0, 0, speed);

    }
    public void KillCity()
    {
        planetSystem.totalCities -= 1;
        planetSystem.ResourceUpdate(1);
    }
    public void KillPlanet()
    {
        Destroy(gameObject);
    }
    private bool IsInArray()
    {
        bool returnValue = false;
        for (int n = 0; n < angleArray.Length; n++)
        {
            if (angleArray[n] == _angleNumber)
            {
                returnValue = true;
            }
        }
        return returnValue;
    }
}

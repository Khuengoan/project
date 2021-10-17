using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City_Score : MonoBehaviour
{
    // Start is called before the first frame update
    private Wood_Rotate wood_Rotate;
    void Start()
    {
        transform.parent.GetComponent<RectTransform>().localPosition = Vector3.zero;
        wood_Rotate = transform.parent.parent.GetComponent<Wood_Rotate>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Text>().text = wood_Rotate.cityNumber.ToString();
    }
}

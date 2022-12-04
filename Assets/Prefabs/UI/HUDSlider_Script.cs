using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDSlider_Script : MonoBehaviour
{
    public GameObject Player;
    public Slider SliderComponent;

    private float PoprzedniFrame;
    private int warst = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey("r")) //reset speed
        {
            SliderComponent.value = warst;
        }

        if (PoprzedniFrame != SliderComponent.value)
        {
            warst = (int)Player.GetComponent<CameraPlayer_Script>().currentLayer;
            warst = warst - 1;
            Debug.Log("-- warstw: "+warst);

            if (SliderComponent.value < warst) //nie mozna wejsc do warstwy ponizej
            {
                SliderComponent.value = warst;
            }

            PoprzedniFrame = SliderComponent.value;
        }

    }
}

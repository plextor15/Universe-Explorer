using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDSlider_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Znacznik;
    public Slider SliderComponent;
    public float SliderValue;

    private float PoprzedniFrame;
    private int warst = 0;

    private RectTransform ZnacznikTransform;

    void Start()
    {
        ZnacznikTransform = Znacznik.GetComponent<RectTransform>();
    }

    void Update()
    {
        SliderValue= SliderComponent.value;

        if (Input.GetKey("r")) //reset speed
        {
            SliderComponent.value = warst;
        }

        if (PoprzedniFrame != SliderComponent.value)
        {
            warst = (int)Player.GetComponent<CameraPlayer_Script>().currentLayer;
            warst = warst - 1;
            //Debug.Log("-- warstw: "+warst);

            if (SliderComponent.value < warst) //nie mozna wejsc do warstwy ponizej
            {
                SliderComponent.value = warst;
            }

            PoprzedniFrame = SliderComponent.value;
        }
    }

    public void UstawZnacznik(CameraPlayer_Script.Warstwy w) 
    {
        Vector3 wektor = new Vector3(ZnacznikTransform.localPosition.x, 0, ZnacznikTransform.localPosition.z);

        switch (w)
        {
            case CameraPlayer_Script.Warstwy.CelestialBody:
                ZnacznikTransform.localPosition = wektor + new Vector3(0,-225f,0);
                break;
            case CameraPlayer_Script.Warstwy.SolarSys:
                ZnacznikTransform.localPosition = wektor + new Vector3(0, -75f, 0);
                break;
            case CameraPlayer_Script.Warstwy.Stars:
                ZnacznikTransform.localPosition = wektor + new Vector3(0, 75f, 0);
                break;
            case CameraPlayer_Script.Warstwy.Galaxy:
                ZnacznikTransform.localPosition = wektor + new Vector3(0, 225f, 0);
                break;
            default: break;
        }
    }
}

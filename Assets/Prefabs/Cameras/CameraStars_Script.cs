using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using static CameraPlayer_Script;

public class CameraStars_Script : MonoBehaviour
{
    public bool CzyWarstwaStars = false;
    public GameObject Player;

    public GameObject Promien;
    //public GameObject DebugMarker;
    
    public Vector3 DoceloweMiejsce = Vector3.zero;
    public bool WDrodze = false;

    void Start()
    {
    }

    void Update()
    {
        if (Player.GetComponent<CameraPlayer_Script>().currentLayer != CameraPlayer_Script.Warstwy.Galaxy) //jesli aktuanie w Layer.Stars
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Star Level Ray Shoot");
                //DebugMarker.SetActive(true);
                Raying(true);
            }
            else
            {
                Raying(false);
                //DebugMarker.SetActive(false);
            }
        }

        if (WDrodze)
        {
            if (Player.GetComponent<CameraPlayer_Script>().currentLayer != CameraPlayer_Script.Warstwy.Stars)
            {
                Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
            }

            if (this.transform.position != DoceloweMiejsce)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, DoceloweMiejsce, 1.0f);
            }

            if (this.transform.position == DoceloweMiejsce)
            {
                WDrodze = false;
                Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.SolarSys); //Warstwy.SolarSys
            }
        }
    }

    public void Raying(bool x)
    {
        Debug.Log("Raying(): " + x);

        if (x)
        {
            Promien.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            Promien.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}

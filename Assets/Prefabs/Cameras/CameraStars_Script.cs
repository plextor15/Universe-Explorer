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
    
    public Vector3 DoceloweMiejsce = Vector3.zero;
    public bool WDrodze = false;
    public float MaxDistDelta = 1.0f;
    public float CzasDoCelu = 1.5f;

    void Start()
    {
    }

    void Update()
    {
        if (Player.GetComponent<CameraPlayer_Script>().currentLayer != CameraPlayer_Script.Warstwy.Galaxy) //jesli aktuanie w Layer.Stars
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Raying(true);
            }
            else
            {
                Raying(false);
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
                this.transform.position = Vector3.MoveTowards(transform.position, DoceloweMiejsce, MaxDistDelta * Time.deltaTime);
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
        if (x)
        {
            Promien.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            Promien.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NoStars")
        {
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Galaxy);
        }
    }
}

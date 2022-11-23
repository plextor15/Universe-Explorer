using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCelestial_Script : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "OutOfCelestial")
        {
            Debug.Log("OutOfCelestial");
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.SolarSys);
        }
    }
}

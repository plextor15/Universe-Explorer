using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGalaxy_Script : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NoStars" && Player.GetComponent<CameraPlayer_Script>().currentLayer == CameraPlayer_Script.Warstwy.Galaxy)
        {
            //Stars maja kazdy inna nazwe
            ////Debug.Log("Some Stars");
            //Player.GetComponent<CameraPlayer_Script>().Stars_Pref.GetComponent<Stars_Particles_Script>().Star_Reset();
            //Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
            //Player.GetComponent<CameraPlayer_Script>().speed *= 1000f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NoStars")
        {
            Debug.Log("No Stars");
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Galaxy);
            Player.GetComponent<CameraPlayer_Script>().Stars_Pref.GetComponent<ParticleSystem>().Clear();
            Player.GetComponent<CameraPlayer_Script>().speed /= 1000f;
        }
    }
}

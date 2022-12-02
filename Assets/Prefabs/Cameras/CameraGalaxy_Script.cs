using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGalaxy_Script : MonoBehaviour
{
    public GameObject Player;
    //public GameObject Camera_Stars;

    void Start()
    {
        
    }

    void Update()
    {
        //if (Player.GetComponent<CameraPlayer_Script>().currentLayer == CameraPlayer_Script.Warstwy.Stars)
        //{
        //    this.transform.rotation = CameraStars.transform.rotation;
        //    this.transform.position = new Vector3(CameraStars.transform.position.x / 100f, CameraStars.transform.position.y / 100f, CameraStars.transform.position.z / 100f);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NoStars" && Player.GetComponent<CameraPlayer_Script>().currentLayer == CameraPlayer_Script.Warstwy.Galaxy)
        {
            Debug.Log("Stars");
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
            Player.GetComponent<CameraPlayer_Script>().speed *= 100f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NoStars")
        {
            Debug.Log("No Stars");
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Galaxy);
            //Player.GetComponent<CameraPlayer_Script>().Stars_Pref.GetComponent<ParticleSystem>().Clear();
            Player.GetComponent<CameraPlayer_Script>().speed /= 100f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSol_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Planet;

    //public float sensitivity;
    //public float roll_rate;

    public Vector3 DoceloweMiejsce = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        if (Player.GetComponent<CameraPlayer_Script>().currentLayer == CameraPlayer_Script.Warstwy.SolarSys)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SolRaying();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ToPlanet")
        {
            //Debug.Log("ToPlanet");
            transform.position = other.transform.position;
            Planet.transform.rotation = other.transform.rotation;
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.CelestialBody);
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "OutOfSol") 
        {
            //Debug.Log("OutOfSol");
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
        }
    }

    public Vector3 SolRaying() 
    {
        Debug.Log("Sol Level Ray Shoot");

        RaycastHit hit;
        Vector3 ray_origin = transform.TransformPoint(0, 0, 0.1f);

        if (Physics.Raycast(ray_origin, this.transform.forward, out hit, 900))
        {
            Debug.Log("Sol Level Ray Hit!");
            //Debug.Log(hit.transform.name);
            Debug.Log("distance: " + hit.distance + ", poz: " + hit.point);

            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        else
        {
            return new Vector3(0,0,0);
        }
    }
}

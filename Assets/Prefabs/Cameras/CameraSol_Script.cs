using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSol_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Planet;

    public float sensitivity;
    public float roll_rate;

    private float mouse_Y; //Mouse Y
    private float mouse_X; //Mouse X

    void Start()
    {

    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse1))
        //{
        //    mouse_Y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //    mouse_X = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        //
        //    this.transform.Rotate(Vector3.up, mouse_X, Space.Self);
        //    this.transform.Rotate(Vector3.right, mouse_Y, Space.Self);
        //}

        ////Roll
        //if (Input.GetKey(KeyCode.E))
        //{
        //    this.transform.Rotate(Vector3.forward, -roll_rate * Time.deltaTime, Space.Self);
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    this.transform.Rotate(Vector3.forward, roll_rate * Time.deltaTime, Space.Self);
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    Debug.Log("Sol Level Ray Shoot");
        //
        //    RaycastHit hit;
        //    Vector3 ray_origin = transform.TransformPoint(0, 0, 0.1f);
        //
        //    if (Physics.Raycast(ray_origin, this.transform.forward, out hit, 900))
        //    {
        //        Debug.Log("Sol Level Ray Hit!");
        //        //Debug.Log(hit.transform.name);
        //        Debug.Log("distance: " + hit.distance + ", poz: " + hit.point);
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ToPlanet")
        {
            Debug.Log("ToPlanet");
            transform.position = other.transform.position;
            Planet.transform.rotation = other.transform.rotation;
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.CelestialBody);
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "OutOfSol") 
        {
            Debug.Log("OutOfSol");
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
        }
    }

    public Vector3 Raycasting() 
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

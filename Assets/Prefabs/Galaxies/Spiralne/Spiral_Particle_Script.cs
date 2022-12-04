using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Spiral_Particle_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Kamera_Stars;
    public GameObject Kamera_Galaxy;
    public GameObject Spiralna;

    //ParticleSystem.Particle p;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //public bool TrigModule = false;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void OnParticleTrigger()
    {
        if (ps == null)
        {
            Debug.LogError("Could not find a particle system");
            return;
        }

        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if (numEnter == 0)
        {
            return;
        }

        if (numEnter != 0) //gdziekolwiek jest, przechodzi na Layer.Stars
        {
            //Debug.Log("-- przechodzi na Layer.Stars --");
            Spiralna.SetActive(true);
            Spiralna.transform.rotation = Quaternion.Euler(enter[0].rotation3D);

            // OLD Prawie Dziala
            //Kamera_Stars.transform.position = Vector3.zero;
            //Kamera_Stars.transform.rotation = Kamera_Galaxy.transform.rotation;
            //Kamera_Stars.transform.Translate(Vector3.back * 700f, Space.Self);   // tyle co granica zmiany warstwy
            //Kamera_Stars.transform.rotation = Kamera_Galaxy.transform.rotation;

            Kamera_Stars.transform.position = Kamera_Galaxy.transform.InverseTransformPoint(enter[0].position);
            Kamera_Stars.transform.position *= 10f;
            Kamera_Stars.transform.rotation = Kamera_Galaxy.transform.rotation;

            Player.GetComponent<CameraPlayer_Script>().Prefab_EliptStars.SetActive(false); //tak zeby nie bylo, ze nadal jest active
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
        }

    }
}

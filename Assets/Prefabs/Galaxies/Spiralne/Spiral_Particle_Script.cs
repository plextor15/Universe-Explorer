using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
//using UnityEngine.Rendering.VirtualTexturing;
using static UnityEngine.ParticleSystem;

public class Spiral_Particle_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Kamera_Stars;
    public GameObject Kamera_Galaxy;
    public GameObject Spiralna;
    public GameObject Prefab_SpawnerGromad;

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
        //Na start ustawiane odpowiednio
        Player.GetComponent<CameraPlayer_Script>().Prefab_EliptStars.SetActive(false);
        Player.GetComponent<CameraPlayer_Script>().Prefab_SpiralStars.SetActive(true);
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

            //dziala
            var TMP = Kamera_Galaxy.transform.rotation;
            Kamera_Stars.transform.position = Vector3.zero;
            Kamera_Galaxy.transform.LookAt(enter[0].position);
            Kamera_Stars.transform.rotation = Kamera_Galaxy.transform.rotation;
            Kamera_Stars.transform.Translate(Vector3.back * (Vector3.Distance(Kamera_Galaxy.transform.position, enter[0].position) * 100f), Space.Self);   // tyle co granica zmiany warstwy
            Kamera_Stars.transform.rotation = TMP;
            Kamera_Galaxy.transform.rotation = TMP;

            Prefab_SpawnerGromad.GetComponent<SpawnerGromad_Script>().CzySpiralna = true;
            Prefab_SpawnerGromad.SetActive(true);

            Player.GetComponent<CameraPlayer_Script>().Prefab_EliptStars.SetActive(false); //tak zeby nie bylo
            Player.GetComponent<CameraPlayer_Script>().Prefab_SpiralStars.SetActive(true); //tak zeby nie bylo
            Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
        }

    }
}

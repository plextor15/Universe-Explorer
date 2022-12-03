//
//  DONT USE!!
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars_Particles_Spiral_ScriptOLD : MonoBehaviour
{
    public GameObject Player;
    public GameObject Promien;

    //ParticleSystem.Particle p;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    public GameObject CameraStars;
    public bool TrigModule = false;


    void Start()
    {
        ps.trigger.SetCollider(0, Promien.GetComponent<Collider>());
    }

    void OnEnable()
    {
        Debug.Log("-- Testowe OnEnable() --");
        ps = GetComponent<ParticleSystem>();
        Debug.Log(ps);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TrigModule = true;
        }
        //if ( !(Input.GetKeyDown(KeyCode.Mouse0)) )
        else
        {
            TrigModule = false;
        }
    }

    void OnParticleTrigger()
    {
        if (TrigModule)
        {
            Debug.Log("-- ParticleTrigger() --");

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
                Debug.Log("-- przechodzi na Layer.Stars --");
                Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
            }

            float dist = Vector3.Distance(CameraStars.transform.position, enter[0].position);
            CameraStars.GetComponent<CameraStars_Script>().MaxDistDelta = dist / CameraStars.GetComponent<CameraStars_Script>().CzasDoCelu;
            CameraStars.GetComponent<CameraStars_Script>().DoceloweMiejsce = enter[0].position;
            CameraStars.GetComponent<CameraStars_Script>().WDrodze = true;
        }
    }

    //void OnParticleCollision(GameObject other)
    //{
    //    if (other.tag == "Player")
    //        Debug.Log(other.tag);
    //}


    public void Star_Reset() 
    { 
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
    }

    public void NicNieWidac()
    {
        GetComponent<ParticleSystem>().Clear();
    }
}

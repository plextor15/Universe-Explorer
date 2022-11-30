using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars_Particles_Spiral_Script : MonoBehaviour
{
    public GameObject Promien;

    ParticleSystem.Particle p;
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
        if (Input.GetKeyDown(KeyCode.Space))
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

            CameraStars.GetComponent<CameraStars_Script>().DebugMarker.transform.position = enter[0].position;
            CameraStars.GetComponent<CameraStars_Script>().DoceloweMiejsce = transform.TransformPoint(CameraStars.GetComponent<CameraStars_Script>().DebugMarker.transform.position);
            CameraStars.GetComponent<CameraStars_Script>().WDrodze = true;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
            Debug.Log(other.tag);
    }


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

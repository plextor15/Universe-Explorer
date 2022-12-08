using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GromadaOtwarta_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Promien;

    //ParticleSystem.Particle p;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    public GameObject CameraStars;
    public bool TrigModule = false;

    //od Gromady
    //public GameObject Trig;


    void Start()
    {
        ps.trigger.SetCollider(0, Promien.GetComponent<Collider>());
    }

    void OnEnable()
    {
        //Debug.Log("-- Testowe OnEnable() --");
        ps = GetComponent<ParticleSystem>();
        //Debug.Log(ps);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TrigModule = true;
        }
        else
        {
            TrigModule = false;
        }
    }

    void OnParticleTrigger()
    {
        if (TrigModule)
        {
            //Debug.Log("-- ParticleTrigger() --");

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
                Player.GetComponent<CameraPlayer_Script>().Zmiana_Warswy(CameraPlayer_Script.Warstwy.Stars);
            }

            Vector3 partic_pos = transform.TransformPoint(enter[0].position);
            float dist = Vector3.Distance(CameraStars.transform.position, partic_pos);
            CameraStars.GetComponent<CameraStars_Script>().MaxDistDelta = dist / CameraStars.GetComponent<CameraStars_Script>().CzasDoCelu;
            CameraStars.GetComponent<CameraStars_Script>().DoceloweMiejsce = partic_pos;
            CameraStars.GetComponent<CameraStars_Script>().WDrodze = true;
        }
    }
}
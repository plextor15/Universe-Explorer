using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GromadaKulista_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject Promien;

    //ParticleSystem.Particle p;
    private ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    public GameObject CameraStars;
    public bool TrigModule = false;

    //od Gromady
    public float MaxGromadaR = 2.0f;
    private bool Niezmienione = true;

    public void Ustawianie(GameObject player, GameObject promien, GameObject camerastars) 
    {
        this.Player = player;
        this.Promien = promien;
        this.CameraStars = camerastars;
    }

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.SetCollider(0, Promien.GetComponent<Collider>());
    }

    void OnEnable()
    {
        //Debug.Log("-- Testowe OnEnable() --");
        //ps = GetComponent<ParticleSystem>();
        //Debug.Log(ps);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) TrigModule = true;
        else TrigModule = false;

        if (Niezmienione)
        {
            if (ps.main.maxParticles == ps.particleCount)
            {
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
                int ileZebranych = ps.GetParticles(particles);
                float R,AngleXZ,AngleY;
                //Debug.Log("Ile zebranych" + ileZebranych);
                //Debug.Log("od ostat FPS: "+ Time.deltaTime);
                for (int i = 0; i < ileZebranych; i++)
                {
                    R = Random.Range(0.01f,1f);
                    R = (1 / (R)) - 1;
                    R = R * MaxGromadaR;
                    AngleXZ = Random.Range(0, 360f);
                    AngleY = Random.Range(0, 360f);

                    particles[i].position = new Vector3( (R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), (R * Mathf.Sin(AngleY) * Mathf.Sin(AngleXZ)), (R * Mathf.Cos(AngleY))) + this.transform.position;
                    //Debug.Log(R+" | "+particles[i].position);
                }
                ps.SetParticles(particles, ileZebranych);
                Niezmienione = false;
            }
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

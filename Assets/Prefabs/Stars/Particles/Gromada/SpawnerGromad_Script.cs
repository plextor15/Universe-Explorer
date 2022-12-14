using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGromad_Script : MonoBehaviour
{
    public bool CzySpiralna = true;
    public int IleGromad = 10;
    public float MaxOdlegloscOdCentrum = 350f;
    public GameObject GromadaOtwarta;
    public GameObject GromadaKulista;
    public GameObject ParentOtwarte;
    public GameObject ParentKuliste;

    public GameObject Galak_Spiral;

    ////do przekazania gromadom
    //public GameObject Player;
    //public GameObject Promien;
    //public GameObject CameraStars;

    private void OnEnable()
    {
        //Random.InitState(42);
        if (CzySpiralna)
        {
            //Debug.Log("Kuliste");
            TworzenieGromad(IleGromad/2, 1, MaxOdlegloscOdCentrum);
            //Debug.Log("Otwarte");
            TworzenieGromad(IleGromad/2, 2, MaxOdlegloscOdCentrum);
            //Debug.Log("Wszystkie");
        }
        else //tylko kuliste
        {
            TworzenieGromad(IleGromad, 1, MaxOdlegloscOdCentrum / 2);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        for (var i = ParentKuliste.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(ParentKuliste.transform.GetChild(i).gameObject);
        }

        for (var i = ParentOtwarte.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(ParentOtwarte.transform.GetChild(i).gameObject);
        }
    }

    void TworzenieGromad(int ile, int rodzaj, float MaxOdleglosc) // 1-kuliste, 2-otwarte
    {
        GameObject instancja;
        for (int i = 0; i < ile; i++)
        {
            float AngleY = 0;
            if (rodzaj == 1)
            {
                GromadaKulista.SetActive(true);

                //Debug.Log("Kuliste - tworzenie");
                float R = Random.Range(0.25f, 1f) * MaxOdleglosc;
                float AngleXZ = Random.Range(0, 360f);


                //if (CzySpiralna)
                //{
                //    AngleY = Random.Range(-2f, 2f);
                //}
                //else
                //{
                //    AngleY = Random.Range(-90f, 90f);
                //}

                AngleY = Random.Range(-90f, 90f); //kuliste sferycznie wokol galaktyki

                Vector3 pozycja = new Vector3((R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), (R * Mathf.Sin(AngleY) * Mathf.Sin(AngleXZ)), (R * Mathf.Cos(AngleY)));

                instancja = Instantiate(GromadaKulista, pozycja, Quaternion.identity);
                //instancja.GetComponent<GromadaKulista_Script>().Ustawianie(Player, Promien, CameraStars);
                instancja.transform.SetParent(ParentKuliste.transform);

                GromadaKulista.SetActive(false);
                //Debug.Log(CzySpiralna + " Kulista - "+R+" | "+instancja.transform.position);
            }
            if (rodzaj == 2)
            {
                GromadaOtwarta.SetActive(true);

                //Debug.Log("Otwarte - tworzenie");
                float R = Random.Range(0.5f, 1f) * MaxOdleglosc;
                float AngleXZ = Random.Range(0, 360f);
                //AngleY = Random.Range(-2f, 2f);
                AngleY = Random.Range(-2f, 2f); //w ramionach galaktyki
                //Vector3 pozycja = new Vector3((R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), (R * Mathf.Sin(AngleY) * Mathf.Sin(AngleXZ)), (R * Mathf.Cos(AngleY))); //OLD

                //Vector3 pozycja = new Vector3((R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), 0, (R * Mathf.Cos(AngleY)));
                Vector3 pozycja = new Vector3( (R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), 0, (R * Mathf.Cos(AngleY)) ); //jak spiral sie obroci od poziomu to jest maly problem
                //pozycja = pozycja * Galak_Spiral.transform.position;


                instancja = Instantiate(GromadaOtwarta, pozycja, Quaternion.identity);
                //instancja.GetComponent<GromadaKulista_Script>().Ustawianie(Player, Promien, CameraStars);
                instancja.transform.SetParent(ParentOtwarte.transform);

                GromadaOtwarta.SetActive(false);
                //Debug.Log(CzySpiralna + " Otwarta - " + R + " | " + instancja.transform.position);
            }
            
        }
        return;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGromad_Script : MonoBehaviour
{
    public bool CzySpiralna = true;
    public int IleGromad = 10;
    public float MaxOdlegloscOdCentrum = 400f;
    public GameObject GromadaOtwarta;
    public GameObject GromadaKulista;
    public GameObject ParentOtwarte;
    public GameObject ParentKuliste;

    ////do przekazania gromadom
    //public GameObject Player;
    //public GameObject Promien;
    //public GameObject CameraStars;

    private void OnEnable()
    {
        //Random.InitState(42);
        if (CzySpiralna)
        {
            Debug.Log("Kuliste");
            TworzenieGromad(IleGromad/2, 1, MaxOdlegloscOdCentrum);
            Debug.Log("Otwarte");
            TworzenieGromad(IleGromad/2, 2, MaxOdlegloscOdCentrum);
            Debug.Log("Wszystkie");
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
            if (rodzaj == 1)
            {
                Debug.Log("Kuliste - tworzenie");
                float R = Random.Range(0.25f, 1f) * MaxOdleglosc;
                float AngleXZ = Random.Range(0, 360f);
                float AngleY = Random.Range(0, 2.7f);
                Vector3 pozycja = new Vector3((R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), (R * Mathf.Sin(AngleY) * Mathf.Sin(AngleXZ)), (R * Mathf.Cos(AngleY)));

                instancja = Instantiate(GromadaKulista, pozycja, Quaternion.identity);
                //instancja.GetComponent<GromadaKulista_Script>().Ustawianie(Player, Promien, CameraStars);
                instancja.transform.SetParent(ParentKuliste.transform);
            }
            if (rodzaj == 2)
            {
                Debug.Log("Otwarte - tworzenie");
                float R = Random.Range(0.25f, 1f) * MaxOdleglosc;
                float AngleXZ = Random.Range(0, 360f);
                float AngleY = Random.Range(0, 30f);
                Vector3 pozycja = new Vector3((R * Mathf.Sin(AngleY) * Mathf.Cos(AngleXZ)), (R * Mathf.Sin(AngleY) * Mathf.Sin(AngleXZ)), (R * Mathf.Cos(AngleY)));

                instancja = Instantiate(GromadaOtwarta, pozycja, Quaternion.identity);
                //instancja.GetComponent<GromadaKulista_Script>().Ustawianie(Player, Promien, CameraStars);
                instancja.transform.SetParent(ParentOtwarte.transform);
            }
            
        }
        return;
    }
}

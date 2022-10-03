using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZombiEnvanteri : MonoBehaviour
{
    private int[] zombiSayisi = new int[4];
    [SerializeField] GameObject[] zombiler = new GameObject[4];
    private int[] zombiFiyatlari = { 1, 3, 5, 7 };
    private GameObject[] zombiSayisiniTutanObje = new GameObject[4];

    private GameObject[] zombiButonu = new GameObject[4];
    private Image[] zombiResmi = new Image[4];
    [SerializeField] GameObject zombiCagirmaButonu;

    private bool isZombieBought = false;
    private int hangiZombiSahneyeSuruklenecek;



    void Start()
    {
        //hangi zombiden kaç tane sahip olduğumuzu bir diziye atadık
        for (int i = 0; i < 4; i++)
        {
            zombiSayisiniTutanObje[i] = (gameObject.transform.GetChild(i + 1).gameObject).transform.GetChild(1).gameObject;
            zombiSayisi[i] = int.Parse(zombiSayisiniTutanObje[i].GetComponent<TextMeshProUGUI>().text);

            zombiButonu[i] = (gameObject.transform.GetChild(i + 1).gameObject).transform.GetChild(0).gameObject;
            zombiResmi[i] = zombiButonu[i].GetComponent<Image>();
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (zombiSayisi[0] >= zombiFiyatlari[i])
            {
                zombiResmi[i].color = new Color(zombiResmi[i].color.r, zombiResmi[i].color.g, zombiResmi[i].color.b, 1f);
                zombiButonu[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                zombiResmi[i].color = new Color(zombiResmi[i].color.r, zombiResmi[i].color.g, zombiResmi[i].color.b, 0.4f);
                zombiButonu[i].GetComponent<Button>().interactable = false;
            }
        }

    }

    public void StandartZombiEldeEt(int increment)
    {
        int mevcutStandartZombiSayisi = zombiSayisi[0];
        int yeniStandartZombiSayisi = mevcutStandartZombiSayisi += increment;
        zombiSayisi[0] = yeniStandartZombiSayisi;
        zombiSayisiniTutanObje[0].GetComponent<TextMeshProUGUI>().text = yeniStandartZombiSayisi.ToString();

        HangiZombidenKacTaneAlinabilir();
    }

    private void HangiZombidenKacTaneAlinabilir()
    {
        for (int i = 0; i < 4; i++)
        {
            zombiSayisi[i] = zombiSayisi[0] / zombiFiyatlari[i];
            zombiSayisiniTutanObje[i].GetComponent<TextMeshProUGUI>().text = zombiSayisi[i].ToString();
        }
    }

    public void ZombiSatinAl(int hangiZombiAlinacak)
    {
        if (zombiButonu[hangiZombiAlinacak].GetComponent<Button>().interactable == false)
        {
            return;
        }

        if (hangiZombiAlinacak == 0)
        {
            int mevcutStandartZombiSayisi = zombiSayisi[0];
            int yeniStandartZombiSayisi = mevcutStandartZombiSayisi -= zombiFiyatlari[hangiZombiAlinacak];
            zombiSayisi[0] = yeniStandartZombiSayisi;
            zombiSayisiniTutanObje[0].GetComponent<TextMeshProUGUI>().text = yeniStandartZombiSayisi.ToString();
        }
        else
        {
            int mevcutOzelZombiSayisi = zombiSayisi[hangiZombiAlinacak];
            int yeniOzelZombiSayisi = mevcutOzelZombiSayisi - 1;

            zombiSayisi[hangiZombiAlinacak] = yeniOzelZombiSayisi;
            zombiSayisi[0] = zombiSayisi[0] - zombiFiyatlari[hangiZombiAlinacak];
            zombiSayisiniTutanObje[hangiZombiAlinacak].GetComponent<TextMeshProUGUI>().text = yeniOzelZombiSayisi.ToString();
        }

        HangiZombidenKacTaneAlinabilir();

        isZombieBought = true;
        hangiZombiSahneyeSuruklenecek = hangiZombiAlinacak;
    }


    public GameObject HangiZombiSahneyeSurukle()
    {
        if (isZombieBought)
        {
            return zombiler[hangiZombiSahneyeSuruklenecek];
        }
        return null;
    }
}

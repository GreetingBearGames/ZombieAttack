using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZombiSahneyeSurukleme : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject zombie;
    private GameObject dogurulacakZombi, dogurulacakZombiResmi;
    [SerializeField] GameObject zombidogurmaAlani;
    [SerializeField] ZombiEnvanteri envanteriTutanObje;
    [SerializeField] GameObject zombiResmi;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.GetComponent<Button>().interactable)
        {
            this.GetComponent<AudioSource>().Play();
            zombidogurmaAlani.SetActive(true);
            dogurulacakZombiResmi = Instantiate(zombiResmi, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (zombidogurmaAlani.activeInHierarchy)
        {
            dogurulacakZombiResmi.transform.position = GetMouseWorldPosition();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (zombidogurmaAlani.activeInHierarchy)
        {
            Destroy(dogurulacakZombiResmi);
            zombidogurmaAlani.SetActive(false);
            dogurulacakZombi = envanteriTutanObje.HangiZombiSahneyeSurukle();
            GameObject go = Instantiate(dogurulacakZombi, GetMouseWorldPosition(), Quaternion.identity, zombie.transform);
            //eventData.pointerDrag = go;   bu kodu neden koyduğumu anlamadım
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        return new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZombiSahneyeSurukleme : MonoBehaviour, IDragHandler
{
    public GameObject zombie;
    private GameObject dogurulacakZombi;
    [SerializeField] GameObject zombidogurmaAlani;
    [SerializeField] ZombiEnvanteri envanteriTutanObje;


    public void OnDrag(PointerEventData eventData)
    {
        if (this.GetComponent<Button>().interactable)
        {
            this.GetComponent<AudioSource>().Play();
            zombidogurmaAlani.SetActive(true);
            dogurulacakZombi = envanteriTutanObje.HangiZombiSahneyeSurukle();
            GameObject go = Instantiate(dogurulacakZombi, GetMouseWorldPosition(), Quaternion.identity);
            go.transform.parent = zombie.transform;
            eventData.pointerDrag = go;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }
}

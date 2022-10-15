using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAndDrag : MonoBehaviour, IDragHandler, IEndDragHandler {
    private GameObject zombidogurmaAlani;
    public Vector2 zombieTarget;
    public static Vector2 target;
    public bool isNewZombieCreated = false;

    private void Start() {
        zombidogurmaAlani = GameObject.Find("Zombi Dogurma Alani");
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 suruklemePozisyonu = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        bool alanIcınemiBirakti = RectTransformUtility.RectangleContainsScreenPoint(zombidogurmaAlani.GetComponent<RectTransform>(), eventData.position);
        if (alanIcınemiBirakti) {
            this.transform.position = suruklemePozisyonu;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        isNewZombieCreated = true;
        zombidogurmaAlani.SetActive(false);
        zombieTarget = transform.position;
        target = zombieTarget;
        SendToBaseZombie();
    }
    public void SendToBaseZombie() {
        float speed;
        switch (this.gameObject.tag) {
            case "StandartZombie":
                speed = this.gameObject.GetComponent<StandartZombie>().Standart.speed;
                break;
            case "TankZombie":
                speed = this.gameObject.GetComponent<TankZombie>().Tank.speed;
                break;
            case "KargaliZombie":
                speed = this.gameObject.GetComponent<KargaliZombie>().Kargali.speed;
                break;
            case "BalyozluZombie":
                speed = this.gameObject.GetComponent<BalyozluZombie>().Balyozlu.speed;
                break;
            default:
                speed = 0;
                break;
        }
        Vector2 BasePos = new Vector2(-7, 0);
        Vector2 xPos = new Vector2(BasePos.x, Random.Range(BasePos.y - 4, BasePos.y + 4));
        transform.position = xPos;
        //transform.position = Vector2.MoveTowards(xPos, zombieTarget,Time.deltaTime);
    }
}

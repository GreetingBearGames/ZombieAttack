using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StandartHuman : MonoBehaviour {
    Animator animator;
    public Humans Standart = new Humans();
    public float attack, defence, speed, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;
    void Start() {
        animator = gameObject.GetComponent<Animator>();
        Standart.attack = attack;
        Standart.defence = defence;
        Standart.speed = speed;
        Standart.range = range;
        Standart.go = go;
        Standart.tr = tr;
        Standart.hp = hp;
        Standart.zombie = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>();
        Standart.StateChanger("idle");
        Standart.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate(){
        Standart.Move();
        Standart.animParams(Standart.idleState, Standart.walkState, Standart.damageState, Standart.attackState, Standart.deathState, animator);
    }

}
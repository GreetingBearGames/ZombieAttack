using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartZombie : MonoBehaviour {
    Animator animator;

    public Zombies Standart = new Zombies();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;


    void Start() {

        animator = gameObject.GetComponent<Animator>();
        Standart.speed = speed;
        Standart.attack = attack;
        Standart.defence = defence;
        Standart.range = range;
        Standart.go = go;
        Standart.tr = tr;
        Standart.hp = hp;
        Standart.aura = false;
        Standart.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        Standart.StateChanger("idle");
        Standart.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate() {
        Debug.Log(Standart.attack);
        Standart.Move();
        Standart.animParams(Standart.idleState, Standart.walkState, Standart.damageState, Standart.attackState, Standart.deathState, animator);
    }

    
}

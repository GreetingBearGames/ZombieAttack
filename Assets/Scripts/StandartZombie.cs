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
        Standart.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        Standart.StateChanger("idle");
    }

    void FixedUpdate() {
        Standart.Move();
        Standart.animParams(Standart.idleState, Standart.walkState, Standart.damageState, Standart.attackState, Standart.deathState, animator);
    }

    
}

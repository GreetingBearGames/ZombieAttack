using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalyozluZombie : MonoBehaviour {
    Animator animator;
    public Zombies Balyozlu = new Zombies();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;

    void Start() {

        animator = gameObject.GetComponent<Animator>();
        Balyozlu.speed = speed;
        Balyozlu.attack = attack;
        Balyozlu.defence = defence;
        Balyozlu.range = range;
        Balyozlu.go = go;
        Balyozlu.tr = tr;
        Balyozlu.hp = hp;
        Balyozlu.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        Balyozlu.StateChanger("idle");
    }

    void FixedUpdate() {
        Balyozlu.Move();
        Balyozlu.animParams(Balyozlu.idleState, Balyozlu.walkState, Balyozlu.damageState, Balyozlu.attackState, Balyozlu.deathState, animator);
    }

    
}

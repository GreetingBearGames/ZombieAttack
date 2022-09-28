using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtZombie : MonoBehaviour {
    Animator animator;
    public Zombies Kurt = new Zombies();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
        Kurt.speed = speed;
        Kurt.attack = attack;
        Kurt.defence = defence;
        Kurt.range = range;
        Kurt.go = go;
        Kurt.tr = tr;
        Kurt.hp = hp;
        Kurt.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        Kurt.StateChanger("idle");
    }

    void Update() {
        Kurt.Move();
        Kurt.animParams(Kurt.idleState, Kurt.walkState, Kurt.damageState, Kurt.attackState, Kurt.deathState, animator);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterZombie : MonoBehaviour {
    public MainHP mainHP;
    public ZombilestirmeScore zombilestirmeScore;
    public ZombiEnvanteri zombiEnvanteri;
    Animator animator;
    public Zombies Main = new Zombies();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;

    void Start() {
        Main.mainHP = mainHP;
        Main.zombilestirmeScore = zombilestirmeScore;
        Main.zombiEnvanteri = zombiEnvanteri;
        animator = gameObject.GetComponent<Animator>();
        Main.speed = speed;
        Main.attack = attack;
        Main.defence = defence;
        Main.range = range;
        Main.go = go;
        Main.tr = tr;
        Main.hp = hp;
        Main.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        Main.StateChanger("idle");
    }

    void Update() {
        Main.Move();
        Main.animParams(Main.idleState, Main.walkState, Main.damageState, Main.attackState, Main.deathState, animator);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankZombie : MonoBehaviour {
    Animator animator;
    public Zombies Tank = new Zombies();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;


    void Start() {
        animator = gameObject.GetComponent<Animator>();
        Tank.speed = speed;
        Tank.attack = attack;
        Tank.defence = defence;
        Tank.range = range;
        Tank.go = go;
        Tank.tr = tr;
        Tank.hp = hp;
        Tank.aura = false;
        Tank.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        Tank.StateChanger("idle");
        Tank.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate() {
        Tank.Move();
        Tank.animParams(Tank.idleState, Tank.walkState, Tank.damageState, Tank.attackState, Tank.deathState, animator);
    }
}

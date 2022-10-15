using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHuman : MonoBehaviour {
    Animator animator;
    public Humans Ranged = new Humans();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;
    void Start() {
        animator = gameObject.GetComponent<Animator>();
        Ranged.attack = attack;
        Ranged.defence = defence;
        Ranged.speed = speed;
        Ranged.range = range;
        Ranged.go = go;
        Ranged.tr = tr;
        Ranged.hp = hp;
        Ranged.zombie = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>();
        Ranged.StateChanger("idle");
        Ranged.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate() {
        Ranged.Move();
        Ranged.animParams(Ranged.idleState, Ranged.walkState, Ranged.damageState, Ranged.attackState, Ranged.deathState, animator);
    }
}

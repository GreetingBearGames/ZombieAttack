using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UcanKacanZombie : MonoBehaviour {
    Animator animator;
    public Zombies UcanKacan = new Zombies();
    public float speed, attack, defence, range, hp;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;


    void Start() {
        animator = gameObject.GetComponent<Animator>();
        UcanKacan.speed = speed;
        UcanKacan.attack = attack;
        UcanKacan.defence = defence;
        UcanKacan.range = range;
        UcanKacan.go = go;
        UcanKacan.tr = tr;
        UcanKacan.hp = hp;
        UcanKacan.aura = false;
        UcanKacan.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
        UcanKacan.StateChanger("idle");
        UcanKacan.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void FixedUpdate() {
        UcanKacan.Move();
        UcanKacan.animParams(UcanKacan.idleState, UcanKacan.walkState, UcanKacan.damageState, UcanKacan.attackState, UcanKacan.deathState, animator);
    }
}

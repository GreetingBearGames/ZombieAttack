using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KargaliZombie : MonoBehaviour {
    Animator animator;
    public Zombies Kargali = new Zombies();
    public float speed, attack, defence, range, hp, flySpeed;
    [SerializeField]private GameObject go;
    [SerializeField]private Transform tr;
    bool isFlying;

    void Start() {
        isFlying = true;
        animator = gameObject.GetComponent<Animator>();
        Kargali.attack = attack;
        Kargali.defence = defence;
        Kargali.range = range;
        Kargali.speed = flySpeed;
        Kargali.go = go;
        Kargali.tr = tr;
        Kargali.hp = hp;
        animator.SetBool("IsFly", true);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsDamage", false);
        animator.SetBool("IsAttack", false);
        animator.SetBool("IsDeath", false);
            //Take Target Position
        Kargali.human = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();   //To access human script;
    }

    void Update() {
        if(isFlying){
            StartCoroutine(startFly());
        }
        else{
            animator.SetBool("IsFly", false);
            Kargali.speed = speed;
            Kargali.Move();
            Kargali.animParams(Kargali.idleState, Kargali.walkState, Kargali.damageState, Kargali.attackState, Kargali.deathState, animator);
        }
    }
    void Fly(Vector3 Target){
        transform.position = Vector3.MoveTowards(transform.position, Target, Kargali.speed * Time.deltaTime);
    }
    IEnumerator startFly(){
        Fly(new Vector3(0f,0f,0f));
        var time = Vector3.Distance(new Vector3(0f,0f,0f), transform.position) / (Kargali.speed * Time.deltaTime);
        yield return new WaitForSeconds(time);
        isFlying = false;
    }
}

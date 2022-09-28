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
    }

    void Update() {
        Standart.Move();
        Standart.animParams(Standart.idleState, Standart.walkState, Standart.damageState, Standart.attackState, Standart.deathState, animator);
    }
    public void StandartCreater(){
        if(!Standart.picker()){
            var hum = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();
            var zombieList = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>().ZombieList;
            GameObject gop;
            Vector3 BasePos = new Vector3(7, 0, 0);
            Vector3 xPos = new Vector3(BasePos.x, Random.Range(BasePos.y - 4, BasePos.y + 4), 0);
            if(hum.HumanList.Count < zombieList.Count){
                gop = Instantiate(hum.std, xPos, Quaternion.identity);
                gop.transform.parent = hum.transform;
            }
        }
    }
}
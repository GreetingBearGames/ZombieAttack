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
    }

    void FixedUpdate() {
        Ranged.Move();
        Ranged.animParams(Ranged.idleState, Ranged.walkState, Ranged.damageState, Ranged.attackState, Ranged.deathState, animator);
    }
    public void RangedCreater(){
        if(Ranged.picker()){
            var hum = GameObject.FindGameObjectWithTag("Human").GetComponent<Human>();
            var zombieList = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>().ZombieList;
            GameObject gop;
            Vector2 BasePos = new Vector2(7, 0);
            Vector2 xPos = new Vector2(BasePos.x, Random.Range(BasePos.y - 4, BasePos.y + 4));
            if(hum.HumanList.Count < zombieList.Count - 1 || (hum.HumanList.Count == 0 && zombieList.Count == 1)){
                gop = Instantiate(hum.std, xPos, Quaternion.identity);
                gop.transform.parent = hum.transform;
            }
        }
    }
}

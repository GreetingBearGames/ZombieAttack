using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans {
    public MainHP mainHP;
    public ZombilestirmeScore zombilestirmeScore;
    public ZombiEnvanteri zombiEnvanteri;
    public float speed, defence, attack, range, hp = 10.0f;
    public int deathNum = 0;
    public bool idleState, walkState, deathState, attackState, damageState;
    public Transform tr;
    public GameObject go;
    public Zombie zombie;
    public bool isGameOver,isnewDead = true;
    public float FindDistance(Transform a, Transform b) {
        float distance = Mathf.Sqrt(Mathf.Pow(b.position.x - a.position.x, 2) + Mathf.Pow(b.position.y - a.position.y, 2));
        return distance;
    }
    public bool IsZombieInRange(Transform ZombieTransform) {
        if (FindDistance(this.tr, ZombieTransform) < this.range) {
            return true;
        } else
            return false;
    }
    public void TransformHuman(Transform Target) {
        float a = 0f;
        if(go.tag == "RangedHuman")
            a = Mathf.Sqrt(Mathf.Pow(this.range, 2)/2);
        this.go.transform.position = Vector3.MoveTowards(this.go.transform.position, Target.position - new Vector3(a,a,0), this.speed * Time.deltaTime);
    }
    public GameObject GetClosestZombie(List<GameObject> li, GameObject zombie) {
        float min = float.MaxValue;
        GameObject focus;
        focus = zombie;
        foreach (var go in li) {
            if(go != null){
                if(FindDistance(this.tr, go.transform) < min){
                    min = FindDistance(this.tr, go.transform);
                    focus = go;
                }
            }
        }
        return focus;
    }
    public void Move() {
        var zombieList = zombie.ZombieList;
        if(zombieList.Count == 0)
            StateChanger("idle");
        foreach (var zombien in zombieList) {
            if(zombien != null){
                var zombieTransform = GetClosestZombie(zombieList, zombien).transform;
                if (zombieTransform != null) {
                    if (this.IsZombieInRange(zombieTransform)) {
                        StateChanger("attack");
                        switch (go.tag) {
                            case "StandartHuman":
                                go.transform.parent.GetComponents<AudioSource>()[0].Play();
                                break;
                            case "RangedHuman":
                                go.transform.parent.GetComponents<AudioSource>()[1].Play();
                                break;
                            default:
                                break;
                        }
                        var tagg = zombieTransform.gameObject.tag;
                        switch (tagg) {
                            case "MainCharacter":
                                isGameOver = zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.isGameOver;
                                zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.hp -= (this.attack)/(1/Time.deltaTime);
                                zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.mainHP.HpAzalt((this.attack)/(1/Time.deltaTime), isGameOver);
                                zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.damageState = true;
                                zombieTransform.gameObject.GetComponent<MainCharacterZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                if(hp <= 0 && isnewDead){
                                    zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.zombiEnvanteri.StandartZombiEldeEt(1);
                                    zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.zombilestirmeScore.ScoreArttır(deathNum);
                                    isnewDead = false;
                                }
                                break;
                            case "StandartZombie":
                                zombieTransform.gameObject.GetComponent<StandartZombie>().Standart.hp -= (this.attack )/(1/Time.deltaTime);
                                zombieTransform.gameObject.GetComponent<StandartZombie>().Standart.damageState = true;
                                zombieTransform.gameObject.GetComponent<StandartZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "TankZombie":
                                zombieTransform.gameObject.GetComponent<TankZombie>().Tank.hp -= (this.attack)/(1/Time.deltaTime);
                                zombieTransform.gameObject.GetComponent<TankZombie>().Tank.damageState = true;
                                zombieTransform.gameObject.GetComponent<TankZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "KargaliZombie":
                                zombieTransform.gameObject.GetComponent<KargaliZombie>().Kargali.hp -= (this.attack)/(1/Time.deltaTime);
                                zombieTransform.gameObject.GetComponent<KargaliZombie>().Kargali.damageState = true;
                                zombieTransform.gameObject.GetComponent<KargaliZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "BalyozluZombie":
                                zombieTransform.gameObject.GetComponent<BalyozluZombie>().Balyozlu.hp -= (this.attack)/(1/Time.deltaTime);
                                zombieTransform.gameObject.GetComponent<BalyozluZombie>().Balyozlu.damageState = true;
                                zombieTransform.gameObject.GetComponent<BalyozluZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            default:
                                break;
                        }
                    } else {
                        this.TransformHuman(zombieTransform);
                        StateChanger("walk");
                    }
                }
            }
        }
        CheckDead();
    }
    public void animParams(bool idle, bool walk, bool damage, bool attack, bool death, Animator animator) {
        animator.SetBool("IsIdle", idle);
        animator.SetBool("IsWalk", walk);
        animator.SetBool("IsDamage", damage);
        animator.SetBool("IsAttack", attack);
        animator.SetBool("IsDeath", death);
    }
    public bool CheckDead() {
        if (hp <= 0) {
            StateChanger("death");
            go.transform.parent.GetComponents<AudioSource>()[3].Play();
            deathNum++;
            return true;
        }
        return false;
    }
    public bool picker(){
        var picker = Random.Range(0,4);
            if(picker == 0)
                return true;    //ranged
            else if(picker < 4 && picker > 0)
                return false;   //std
        return false;
    }
    public void StateChanger(string state) {
        switch (state) {
            case "walk":
                walkState = true;
                idleState = false;
                damageState = false;
                deathState = false;
                attackState = false;
                break;

            case "idle":
                walkState = false;
                idleState = true;
                damageState = false;
                deathState = false;
                attackState = false;
                break;

            case "attack":
                walkState = false;
                idleState = false;
                damageState = false;
                deathState = false;
                attackState = true;
                break;

            case "death":
                walkState = false;
                idleState = false;
                damageState = false;
                deathState = true;
                attackState = false;
                break;

            case "damage":
                walkState = false;
                idleState = false;
                damageState = true;
                deathState = false;
                attackState = false;
                break;
            default:
                break;
        }
    }
}



public static class Utils {
    public static List<GameObject> GetChildren(GameObject go) {
        List<GameObject> list = new List<GameObject>();
        return GetChildrenHelper(go, list);
    }
    private static List<GameObject> GetChildrenHelper(GameObject go, List<GameObject> list) {
        if (go == null || go.transform.childCount == 0) {
            return list;
        }
        foreach (Transform t in go.transform) {
            list.Add(t.gameObject);
            GetChildrenHelper(t.gameObject, list);
        }
        return list;
    }
}
public class Human : MonoBehaviour {
    
    public List<GameObject> HumanList;
    public GameObject std, ranged;
    public StandartHuman standartHuman;
    public RangedHuman rangedHuman;
    private void Update() {
        HumanList = Utils.GetChildren(this.gameObject);
        standartHuman.StandartCreater();
        rangedHuman.RangedCreater();
        
    }
}
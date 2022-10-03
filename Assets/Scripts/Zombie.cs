using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zombies {   //Zombies class is the parent class. This includes some common instances such as speed, defence etc.
    public float speed, defence, attack, range, hp;
    public Transform tr;
    public GameObject go;
    public Human human;
    public MainHP mainHP;
    public ZombilestirmeScore zombilestirmeScore;
    public JoystickPlayerExample joystickPlayerExample;
    public ZombiEnvanteri zombiEnvanteri;
    public bool idleState, walkState, deathState, attackState, damageState, isGameOver = false;
    public float FindDistance(Transform a, Transform b) {    //Finds the distance between two points a and b
        float distance = Mathf.Sqrt(Mathf.Pow(b.position.x - a.position.x, 2) + Mathf.Pow(b.position.y - a.position.y, 2));
        return distance;
    }
    public bool IsHumanInRange(Transform humanTransform) {   //Detects if a human is in range
        if (FindDistance(this.tr, humanTransform) < this.range) {
            return true;
        } else
            return false;
    }
    public void TransformZombie(Transform Target) {  //Transforms a zombie from current pos to a target pos
        var maxDist = speed * Time.fixedDeltaTime;
        maxDist /=  go.transform.parent.GetComponent<Zombie>().ZombieList.Count();
        if(this.go.transform.position.x < Target.position.x)
            this.go.transform.eulerAngles = new Vector3(this.go.transform.eulerAngles.x, 0f, this.go.transform.eulerAngles.z);
        if(this.go.transform.position.x > Target.position.x)
            this.go.transform.eulerAngles = new Vector3(this.go.transform.eulerAngles.x, 180f, this.go.transform.eulerAngles.z);
        this.go.transform.position = Vector2.MoveTowards(this.go.transform.position, Target.position , maxDist);
    }
    public void animParams(bool idle, bool walk, bool damage, bool attack, bool death, Animator animator) {
        animator.SetBool("IsIdle", idle);
        animator.SetBool("IsWalk", walk);
        animator.SetBool("IsDamage", damage);
        animator.SetBool("IsAttack", attack);
        animator.SetBool("IsDeath", death);
    }

    public GameObject GetClosestHuman(List<GameObject> li, GameObject zombie) {
        float min = float.MaxValue;
        GameObject focus = zombie;
        foreach (var go in li) {
            if (go != null) {
                if (FindDistance(this.tr, go.transform) < min) {
                    min = FindDistance(tr.transform, go.transform);
                    focus = go;
                }
            }
        }
        return focus;
    }

    public void Move() {
        var humanList = human.HumanList;
        if (humanList.Count == 0)
            StateChanger("idle");
        foreach (var human in humanList) {
            if (human != null) {
                var humanTransform = GetClosestHuman(humanList, human).transform;
                if (humanTransform != null) {
                    if (this.IsHumanInRange(humanTransform)) {
                        StateChanger("attack");
                        switch (go.tag) {
                            case "MainCharacter":
                                go.transform.parent.GetComponents<AudioSource>()[1].Play();
                                break;
                            case "StandartZombie":
                                go.transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "TankZombie":
                                go.transform.parent.GetComponents<AudioSource>()[3].Play();
                                break;
                            case "BalyozluZombie":
                                go.transform.parent.GetComponents<AudioSource>()[4].Play();
                                break;
                            case "KargaliZombie":
                                go.transform.parent.GetComponents<AudioSource>()[5].Play();
                                break;
                            default:
                                break;
                        }

                        if (humanTransform.gameObject.tag == "StandartHuman") {
                            humanTransform.gameObject.GetComponent<StandartHuman>().Standart.hp -= (this.attack) / (1 / Time.deltaTime);
                            //humanTransform.gameObject.GetComponent<StandartHuman>().Standart.damageState = true;
                            //humanTransform.gameObject.GetComponent<StandartHuman>().transform.parent.GetComponents<AudioSource>()[0].Play();
                        } else if (humanTransform.gameObject.tag == "RangedHuman") {
                            humanTransform.gameObject.GetComponent<RangedHuman>().Ranged.hp -= (this.attack) / (1 / Time.deltaTime);
                            //humanTransform.gameObject.GetComponent<RangedHuman>().Ranged.damageState = true;
                            //humanTransform.gameObject.GetComponent<RangedHuman>().transform.parent.GetComponents<AudioSource>()[0].Play();
                        }
                    } else {
                        if(this.go.tag != "MainCharacter"){
                            this.TransformZombie(humanTransform);
                            StateChanger("walk");
                        }
                        else{
                            joystickPlayerExample.Move();
                            if(joystickPlayerExample.fixedJoystick.Direction.x < 0)
                                this.go.transform.eulerAngles = new Vector3(this.go.transform.eulerAngles.x, 180f, this.go.transform.eulerAngles.z);
                            else if(joystickPlayerExample.fixedJoystick.Direction.x > 0)
                                this.go.transform.eulerAngles = new Vector3(this.go.transform.eulerAngles.x, 0f, this.go.transform.eulerAngles.z);
                        }
                        
                    }
                }
            }
        }
        CheckDead();
    }

    public bool CheckDead() {
        if (hp <= 0) {
            go.transform.parent.GetComponents<AudioSource>()[6].Play();
            StateChanger("death");
            if (this.go.tag == "MainCharacter")
                isGameOver = true;
            return true;
        }
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
}



public class Zombie : MonoBehaviour {
    
    [System.Serializable]
    public class Inventory {
        public int count;
        public string zombieName;
    }
    public List<GameObject> ZombieList;
    public List<Inventory> zombiesInventory = new List<Inventory>(){
        new Inventory(){count = 0,zombieName = "StandartZombie"},
        new Inventory(){count = 0,zombieName = "TankZombie"},
        new Inventory(){count = 0,zombieName = "BalyozluZombie"},
        new Inventory(){count = 0,zombieName = "KargaliZombie"},
        new Inventory(){count = 0,zombieName = "MainCharacter"}
    };

    private void Update() {
        ZombieList = Utils.GetChildren(this.gameObject);
        UpdateInventory();
    }

    public void UpdateInventory() {
        foreach (var zombie in ZombieList) {
            foreach (var zombieI in zombiesInventory) {
                var tag = zombie.tag;
                zombieI.count = ZombieList.Where(zombie => zombie.tag == zombieI.zombieName).Count();
            }
        }
    }
}

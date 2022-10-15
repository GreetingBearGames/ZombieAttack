using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Humans {
    public MainHP mainHP;
    public ZombilestirmeScore zombilestirmeScore;
    public ZombiEnvanteri zombiEnvanteri;
    public float speed, defence, attack, range, hp;
    public bool idleState, walkState, deathState, attackState, damageState;
    public Vector2 screenBounds;
    public Vector3 viewPos;
    public Transform tr;
    public GameObject go;
    public Zombie zombie;
    public bool isGameOver, isnewDead = true;
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
        var maxDist = speed * Time.fixedDeltaTime;
        var zombieCount = zombie.ZombieList.Count();
        if(zombieCount != 0){
            maxDist /=  zombieCount;
            if(this.go.transform.position.x < Target.position.x)
                this.go.transform.eulerAngles = new Vector3(this.go.transform.eulerAngles.x, 0f, this.go.transform.eulerAngles.z);
            if(this.go.transform.position.x > Target.position.x)
                this.go.transform.eulerAngles = new Vector3(this.go.transform.eulerAngles.x, 180f, this.go.transform.eulerAngles.z);
            this.go.transform.position = Vector2.MoveTowards(this.go.transform.position, Target.position - new Vector3(a,a,0), maxDist);
        }
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
                        if(!go.transform.parent.GetComponents<AudioSource>()[0].isPlaying &&
                            !go.transform.parent.GetComponents<AudioSource>()[1].isPlaying
                        ){
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
                        }
                        var tagg = zombieTransform.gameObject.tag;
                        switch (tagg) {
                            case "MainCharacter":
                                isGameOver = zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.isGameOver;
                                zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.hp -= (this.attack)/(1/Time.deltaTime);
                                zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.mainHP.HpAzalt((this.attack)/(1/Time.deltaTime), isGameOver);
                                //zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.damageState = true;
                                //zombieTransform.gameObject.GetComponent<MainCharacterZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                if(hp <= 0 && isnewDead){
                                    zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.zombiEnvanteri.StandartZombiEldeEt(1);
                                    zombieTransform.gameObject.GetComponent<MainCharacterZombie>().Main.zombilestirmeScore.ScoreArttır(isnewDead);
                                    isnewDead = false;
                                }
                                break;
                            case "StandartZombie":
                                zombieTransform.gameObject.GetComponent<StandartZombie>().Standart.hp -= (this.attack )/(1/Time.deltaTime);
                                if(hp <= 0  && isnewDead){
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombiEnvanteri.StandartZombiEldeEt(1);
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombilestirmeScore.ScoreArttır(isnewDead);
                                    isnewDead = false;
                                }
                                //zombieTransform.gameObject.GetComponent<StandartZombie>().Standart.damageState = true;
                                //zombieTransform.gameObject.GetComponent<StandartZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "TankZombie":
                                zombieTransform.gameObject.GetComponent<TankZombie>().Tank.hp -= (this.attack)/(1/Time.deltaTime);
                                if(hp <= 0  && isnewDead){
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombiEnvanteri.StandartZombiEldeEt(1);
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombilestirmeScore.ScoreArttır(isnewDead);
                                    isnewDead = false;
                                }
                                //zombieTransform.gameObject.GetComponent<TankZombie>().Tank.damageState = true;
                                //zombieTransform.gameObject.GetComponent<TankZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "KargaliZombie":
                                zombieTransform.gameObject.GetComponent<KargaliZombie>().Kargali.hp -= (this.attack)/(1/Time.deltaTime);
                                if(hp <= 0  && isnewDead){
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombiEnvanteri.StandartZombiEldeEt(1);
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombilestirmeScore.ScoreArttır(isnewDead);
                                    isnewDead = false;
                                }
                                //zombieTransform.gameObject.GetComponent<KargaliZombie>().Kargali.damageState = true;
                                //zombieTransform.gameObject.GetComponent<KargaliZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
                                break;
                            case "BalyozluZombie":
                                zombieTransform.gameObject.GetComponent<BalyozluZombie>().Balyozlu.hp -= (this.attack)/(1/Time.deltaTime);
                                if(hp <= 0  && isnewDead){
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombiEnvanteri.StandartZombiEldeEt(1);
                                    zombieTransform.gameObject.transform.parent.GetChild(0).GetComponent<MainCharacterZombie>().Main.zombilestirmeScore.ScoreArttır(isnewDead);
                                    isnewDead = false;
                                }
                                //zombieTransform.gameObject.GetComponent<BalyozluZombie>().Balyozlu.damageState = true;
                                //zombieTransform.gameObject.GetComponent<BalyozluZombie>().transform.parent.GetComponents<AudioSource>()[2].Play();
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
    private void Start(){
        InvokeRepeating("Creater", 0f , 4f);
    }
    private void Update() {
        HumanList = Utils.GetChildren(this.gameObject);
    }
    void Creater(){
        var zombieList = GameObject.FindGameObjectWithTag("Zombie").GetComponent<Zombie>().ZombieList;
        GameObject gop;
        if(picker()){
            Vector2 BasePos = new Vector2(7, 0);
            Vector2 xPos = new Vector2(BasePos.x, Random.Range(BasePos.y - 4, BasePos.y + 4));
            gop = Instantiate(std, xPos, Quaternion.identity);
            gop.transform.parent = transform;
        }
        else{
            Vector2 BasePos = new Vector2(7, 0);
            Vector2 xPos = new Vector2(BasePos.x, Random.Range(BasePos.y - 4, BasePos.y + 4));
            gop = Instantiate(ranged, xPos, Quaternion.identity);
            gop.transform.parent = transform;
        }
    }
    public bool picker(){
        var picker = Random.Range(0,4);
            if(picker == 0)
                return true;    //ranged
        return false;
    }
}

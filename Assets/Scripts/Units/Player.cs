using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Unit {

    Animator animator;
    Rigidbody2D rb;
	Transform trans;
	GameObject gun;
	public UnitStats stats;
	public Weapon mainWeapon;
	public float nextShot;
	enum Attribute {FLYING, INVISBLE};
	Dictionary<Attribute, bool> dict = new Dictionary<Attribute, bool>();

    public void initializeObject()
    {
		animator = GetComponent<Animator>();
		stats = new UnitStats (this.gameObject, new List<AbstractEffect>{new SprintEffect()});
		stats.attack_rate = 100;
		trans = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
		gun = gameObject.transform.Find ("Gun").gameObject;
		if (gun != null)
			mainWeapon = gun.transform.GetComponent<Weapon> ();
		else
			Debug.LogError (gameObject.name + " is lacking a Gun child.");
		nextShot = Time.time;
		checkForComponents ();
		Debug.Log ("Player initialized correctly.");
		setPlayerStats (stats);
    }

    void FixedUpdate()
    {
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bullet"  && other.transform.GetComponent<Bullet>().bulletType == Bullet.BulletType.BULLET_ENEMY) {
			stats.hp -= 10;
			GameObject.Destroy (other.gameObject);
			//GameMaster.DeactivateCollider (other.gameObject.GetComponent<Collider2D> ());
			//GameMaster.DeactivateObject (other.gameObject);
			Debug.Log ("Unit " + this.gameObject.name + " has been hit.");
		}
	}

	public void setPlayerStats(UnitStats stats){
		this.stats = stats;
	}

	void checkForComponents()
	{
		if (animator == null) {
			Debug.LogError ("Missing Animator component during PlayerBehaviour initialization");
		}
		if (stats == null) {
			Debug.LogError ("Missing UnitStats component during PlayerBehaviour initialization");
		}
		if (trans == null) {
			Debug.LogError ("Missing Transform component during PlayerBehaviour initialization");
		}
		if (rb == null) {
			Debug.LogError ("Missing RigidBody2D component during PlayerBehaviour initialization");
		}
		if (mainWeapon == null) {
			Debug.LogError ("Missing Weapon component during PlayerBehaviour initialization");
		}
	}

	public void moveBy(Vector3 vec) {
		trans.Translate (vec);
	}

	public void setAnimatorSpeed(float speed){
		animator.speed = speed;
	}

	public void playAnimation(string name){
		animator.Play (name);
	}
}
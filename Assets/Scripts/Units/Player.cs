using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Unit {
	public float nextShot;
	public bool shooting;
	public KeyMap keymap;
	enum Attribute {FLYING, INVISBLE};
	Dictionary<Attribute, bool> dict = new Dictionary<Attribute, bool>();

    public override void initialize()
    {
		base.initialize ();
		keymap = new KeyMap ();
		nextShot = Time.time;
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bullet"  && other.transform.GetComponent<Bullet>().bulletType == Bullet.BulletType.BULLET_ENEMY) {
			Bullet bull = other.GetComponent<Bullet>();
			stats.hp -= bull.damage;
			GameObject.Destroy (other.gameObject);
			Debug.Log (gameObject.name + " has been hit by " + bull.shooter + " and has taken " + bull.damage + " damage.");
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
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Unit {
	public float nextShot;
	enum Attribute {FLYING, INVISBLE};
	Dictionary<Attribute, bool> dict = new Dictionary<Attribute, bool>();

    public override void initialize()
    {
		base.initialize ();
		nextShot = Time.time;
		Debug.Log ("Player initialized correctly.");
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
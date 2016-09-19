using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

public class Enemy : Unit {

	void Update(){
		Vector3 player = GameMaster.GM.getPlayerPosition ("Player 1");
		Vector3 moveTo = player - transform.position;
		rb.AddForce (moveTo*stats.speed*Mathf.Min(0.02f, 1/moveTo.magnitude));
		rb.angularDrag = 0;
		float rnd2 = Random.value;
		if (rnd2 > 0.99)
			mainWeapon.Shoot (gameObject, Bullet.BulletType.BULLET_ENEMY, Bullet.BulletElement.BULLET_AIR, moveTo*stats.attack_speed*0.1f);
		if (stats.hp <= 0) {
			GameMaster.DeactivateObject (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bullet"  && other.transform.GetComponent<Bullet>().bulletType == Bullet.BulletType.BULLET_PLAYER) {
			Bullet bull = other.GetComponent<Bullet>();
			stats.hp -= bull.damage;
			GameObject.Destroy (other.gameObject);
			Debug.Log (gameObject.name + " has been hit by " + bull.shooter + " and has taken " + bull.damage + " damage.");
		}
	}
}
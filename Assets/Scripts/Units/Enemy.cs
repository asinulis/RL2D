using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

public class Enemy : Unit {

	void Update(){
		Vector3 player = GameMaster.GM.playerObj.transform.position;
		Vector3 moveTo = player - transform.position;
		rb.AddForce (moveTo*stats.speed*Mathf.Min(0.02f, 1/moveTo.magnitude));
		//Vector2 rnd0 = Random.insideUnitCircle;
		//Vector3 rnd = new Vector3(rnd0.x, rnd0.y, 0);
		//this.transform.position += rnd*0.1f;
//
		float rnd2 = Random.value;
		if (rnd2 > 0.99)
			mainWeapon.Shoot (gameObject, Bullet.BulletType.BULLET_ENEMY, Bullet.BulletElement.BULLET_AIR, moveTo*stats.attack_speed*0.1f);
//
		if (stats.hp <= 0) {
			GameMaster.DeactivateObject (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bullet"  && other.transform.GetComponent<Bullet>().bulletType == Bullet.BulletType.BULLET_PLAYER) {
			stats.hp -= 10;
			GameObject.Destroy (other.gameObject);
			//GameMaster.DeactivateCollider (other.gameObject.GetComponent<Collider2D> ());
			//GameMaster.DeactivateObject (other.gameObject);
			Debug.Log ("Unit " + this.gameObject.name + " has been hit.");
		}
	}

}
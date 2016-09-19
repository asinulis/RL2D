using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

public class Enemy : Unit {

	void Update(){
		Vector3 player = GameMaster.GM.getPlayerPosition ("Player 1");
		Vector3 direction = player - transform.position; direction.Normalize ();
		//rb.AddForce (moveTo*stats.speed*Mathf.Min(0.02f, 1/moveTo.magnitude));
		//rb.angularDrag = 0;
		float rnd2 = Random.value;
		if (rnd2 > 0.99)
			mainWeapon.Shoot (gameObject, Bullet.BulletType.BULLET_ENEMY, Bullet.BulletElement.BULLET_AIR, direction*stats.attack_speed*0.1f);
		if (stats.hp <= 0) {
			GameMaster.DeactivateObject (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		GameMaster.GM.handleCollision (this, other);
	}
}
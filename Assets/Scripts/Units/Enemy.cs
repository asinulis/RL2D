using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

/*
 * A correctly set up enemy should have the following characteristics:
 * Rigidbody 2D, Animator, BoxCollider2D (feet), Enemy Script
 * 
 * The children are structured as follows:
 * Player
 * |- Gun
 * 	  |- Barrel
 * |- Trigger
 * 
 * The gun should hold the Weapon script and a sprite renderer, the barrel can be an empty object. Every trigger needs to have the 
 * LayerChangeOnTrigger script attached to it.
*/

public class Enemy : Unit {

	void Update(){
		Vector3 player = GameMaster.GM.getPlayerPosition ("Player 1");
		Vector3 direction = player - transform.position; direction.Normalize ();
		float rnd2 = Random.value;
		if (rnd2 > 0.99)
			mainWeapon.Shoot (gameObject, Bullet.BulletType.BULLET_ENEMY, direction*stats.attackSpeed*0.1f);
		if (stats.hp <= 0) {
			this.gameObject.SetActive(false);
		}
	}
}
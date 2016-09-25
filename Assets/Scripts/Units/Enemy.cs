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

	void LateUpdate(){
		
		if (stats.hp <= 0) {
			createCorpse (trans.position);
			Destroy(this.gameObject);
		}

		Vector3 playerPos = GameMaster.GM.getPlayerPosition ("Player1");
		if ((playerPos - trans.position).sqrMagnitude <= 8) {
			Vector3 direction = (playerPos - trans.position);
			direction.Normalize ();
			float rnd2 = Random.value;
			if (rnd2 > 0.99)
				mainWeapon.Shoot (gameObject, Bullet.BulletType.BULLET_ENEMY, direction * stats.attackSpeed * 0.1f);

		} else {
			//WaitForSeconds (1f);
			//yield return new WaitForSecondsRealtime(15f);
		}
	}

	internal void createCorpse(Vector3 position){
		GameObject shell = new GameObject ("Corpse"); 
		shell.SetActive (false);
		shell.transform.position = position; 
		shell.transform.parent = GameMaster.GM.transform; 
		shell.AddComponent<SpriteRenderer> (); 
		shell.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("corpse");
		shell.GetComponent<SpriteRenderer> ().sortingLayerName = "Background";
		shell.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		shell.SetActive (true);
		Destroy (this.gameObject);
	}
}
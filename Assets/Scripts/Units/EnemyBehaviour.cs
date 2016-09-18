using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {
	public UnitStats stats;
	public GameObject gun;
	public Weapon mainWeapon;

	// Use this for initialization
	void Start () {
		stats = new UnitStats (this.gameObject);
		gun = gameObject.transform.Find ("Gun").gameObject;
		if (gun != null)
			mainWeapon = gun.transform.GetComponent<Weapon> ();
		else
			Debug.LogError (gameObject.name + " is lacking a Gun child.");
		checkComponents ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 rnd0 = Random.insideUnitCircle;
		Vector3 rnd = new Vector3(rnd0.x, rnd0.y, 0);
		this.transform.position += rnd*0.1f;

		float rnd2 = Random.value;
		if (rnd2 > 0.99)
			mainWeapon.Shoot (gameObject, Bullet.BulletType.BULLET_ENEMY, Bullet.BulletElement.BULLET_AIR, Vector2.zero);

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

	void checkComponents() {
		if (stats == null)
			throw new InitializationException("Enemy stats could not be initialized.");
		else if (mainWeapon == null)
			throw new InitializationException("Enemy weapon could not be added.");
	}
}

public class InitializationException : Exception{
	
	public InitializationException(string message): base(message){}

}

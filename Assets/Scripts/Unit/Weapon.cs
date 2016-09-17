using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon : MonoBehaviour {

	public GameObject bulletPrefab;
	GameObject bullet;
	GameObject holder;
	float damage;

	void Start()
	{
		holder = this.transform.parent.gameObject;
		damage = 3f;
		checkComponents ();
	}

	public void Shoot()
	{
		Debug.Log (holder.name + " is shooting and is doing " + damage + " damage.");
		bullet = Instantiate (bulletPrefab) as GameObject; //, this.transform) as GameObject; //, holder.transform);
		bullet.transform.Translate(holder.transform.position.x, holder.transform.position.y, 0f);
		//if (bullet == null) {
		//	Debug.LogError ("Could not find the bullet prefab");
		//}
		//GameObject rocket = Instantiate (bullet, bullet.transform) as GameObject;
		//rocket.transform.position = holder.transform.position;
		//Debug.Log (holder.transform.position.x + "/" + holder.transform.position.y);
		//Vector2 direction = new Vector2(holder.transform.position.x, holder.transform.position.y) + Vector2.up;
		//rocket.transform.Translate(1f, 0.0f, 0.0f);
		//rocket.SetActive (false);
	}

	void checkComponents(){
		if (holder == null) {
			Debug.LogError ("Weapon holder is null.");
		}
		if (bulletPrefab == null) {
			Debug.LogError ("BulletPrefab for weapon is not set.");
		}
	}
}

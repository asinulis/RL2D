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
		GameMaster.GM.CreateBullet (holder.transform.position);
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

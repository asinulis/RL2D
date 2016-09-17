using UnityEngine;
using System.Collections;

public class Weapon {

	GameObject holder;
	float damage;

	public Weapon(GameObject holder, float damage)
	{
		this.holder = holder;
		this.damage = damage;
	}

	public void Shoot()
	{
		Debug.Log (holder.name + " is shooting and is doing " + damage + " damage.");
		GameObject rocket = (GameObject)GameObject.Instantiate (GameObject.Find ("Bullet"), GameObject.Find("Bullets").transform);
		rocket.transform.position = holder.transform.position;
		//rocket.SetActive (false);
	}
}

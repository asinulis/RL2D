using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {
	public int damage;
	public Dictionary<Elements, int> ammunition = new Dictionary<Elements, int>();
	public Elements element	= Elements.ELEMENT_AIR;

	void Start(){
		damage = 10;
		ammunition.Add (Elements.ELEMENT_AIR, 300);
		ammunition.Add (Elements.ELEMENT_EARTH, 300);
		ammunition.Add (Elements.ELEMENT_FIRE, 300);
		ammunition.Add (Elements.ELEMENT_WATER, 300);
	}

	public void Shoot(GameObject shooter, Bullet.BulletType type, Vector2 direction)
	{
		if (ammunition[element] > 0) {
			direction.Normalize ();
			GameObject barrel = shooter.GetComponent<Unit> ().firePoint;
			GameObject bullet = GameMaster.GM.createBullet (barrel.transform.position);
			Bullet bulletScript	= bullet.transform.GetComponent<Bullet> ();

			bulletScript.bulletType = type;
			bulletScript.bulletElement = element;
			bulletScript.damage = damage + shooter.GetComponent<Unit> ().stats.damage;
			bulletScript.shooter = shooter.name;
			bulletScript.destination = direction;

			switch (element) {
			case Elements.ELEMENT_AIR:
				bullet.GetComponent<SpriteRenderer> ().color = Color.green;
				break;
			case Elements.ELEMENT_EARTH:
				bullet.GetComponent<SpriteRenderer> ().color = Color.yellow;
				break;
			case Elements.ELEMENT_FIRE:
				bullet.GetComponent<SpriteRenderer> ().color = Color.red;
				break;
			case Elements.ELEMENT_WATER:
				bullet.GetComponent<SpriteRenderer> ().color = Color.blue;
				break;
			}

			ammunition [element]--;
		}
	}

	public void Reload()
	{
		Debug.Log ("Reloading gun.");
	}

	public void changeWeapon(bool up)
	{
		switch (element) {
		case Elements.ELEMENT_AIR:
			if (up)
				element = Elements.ELEMENT_EARTH;
			else
				element = Elements.ELEMENT_WATER;
			break;
		case Elements.ELEMENT_EARTH:
			if (up)
				element = Elements.ELEMENT_FIRE;
			else
				element = Elements.ELEMENT_AIR;
			break;
		case Elements.ELEMENT_FIRE:
			if (up)
				element = Elements.ELEMENT_WATER;
			else
				element = Elements.ELEMENT_EARTH;
			break;
		case Elements.ELEMENT_WATER:
			if (up)
				element = Elements.ELEMENT_AIR;
			else
				element = Elements.ELEMENT_FIRE;
			break;
		}
	}
}

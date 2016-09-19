using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	int damage;
	int ammunition;
	Bullet.BulletElement element;

	void Start(){
		damage = 3;
	}

	public void Shoot(GameObject shooter, Bullet.BulletType type, Bullet.BulletElement element, Vector2 direction)
	{
		direction.Normalize ();
		GameObject bullet =	GameMaster.GM.createBullet (shooter.transform.FindChild("Gun").transform.position);
		Bullet bull = bullet.transform.GetComponent<Bullet> ();

		bull.bulletType = type;
		bull.bulletElement = element;
		bull.damage = damage + shooter.GetComponent<Unit>().stats.damage;
		bull.shooter = shooter.name;
		bull.destination = direction;

		if (element == Bullet.BulletElement.BULLET_AIR)
			bullet.GetComponent<SpriteRenderer> ().color = Color.green;
		else if (element == Bullet.BulletElement.BULLET_WATER)
			bullet.GetComponent<SpriteRenderer> ().color = Color.blue;
		else if (element == Bullet.BulletElement.BULLET_EARTH)
			bullet.GetComponent<SpriteRenderer> ().color = Color.yellow;
		else if (element == Bullet.BulletElement.BULLET_FIRE)
			bullet.GetComponent<SpriteRenderer> ().color = Color.red;
		//bullet.GetComponent<Rigidbody2D> ().velocity = ;
		bullet.GetComponent<Rigidbody2D>().AddForce(direction*shooter.GetComponent<Unit>().stats.attack_speed);
	}

	public void Reload()
	{
		Debug.Log ("Reloading gun.");
	}
}

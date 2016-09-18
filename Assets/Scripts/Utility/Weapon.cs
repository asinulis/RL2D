using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	float damage;
	int ammunition;
	Bullet.BulletElement element;

	void Start(){
		damage = 3f;
		element = Bullet.BulletElement.BULLET_AIR;
	}

	public void Shoot(GameObject shooter, Bullet.BulletType type, Bullet.BulletElement element)
	{
		GameObject bullet =	GameMaster.GM.CreateBullet (shooter.transform.position, type, element);

		bullet.transform.GetComponent<Bullet> ().bulletType = type;
		bullet.transform.GetComponent<Bullet> ().bulletElement = element;
		if (element == Bullet.BulletElement.BULLET_AIR)
			bullet.GetComponent<SpriteRenderer> ().color = Color.green;
		else if (element == Bullet.BulletElement.BULLET_WATER)
			bullet.GetComponent<SpriteRenderer> ().color = Color.blue;
		else if (element == Bullet.BulletElement.BULLET_EARTH)
			bullet.GetComponent<SpriteRenderer> ().color = Color.yellow;
		else if (element == Bullet.BulletElement.BULLET_FIRE)
			bullet.GetComponent<SpriteRenderer> ().color = Color.red;
		
	}

	public void Reload()
	{
		Debug.Log ("Reloading gun.");
	}
}

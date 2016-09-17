using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	Weapon mainWeapon;
	public float damage;

	// Use this for initialization
	void Start () {
		mainWeapon = new Weapon (this.gameObject, damage); 
	}
	
	// Update is called once per frame
	void Update () {
		//int rnd = Random.Range (1, 1000);
		//if (rnd > 990) {
			//mainWeapon.Shoot ();
		//}
	}
}

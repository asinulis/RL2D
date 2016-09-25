using UnityEngine;
using System.Collections;

public class Hitable : RLObject {

	public void OnCollisionEnter2D(Collision2D other){
		if (this.name == "Bullet" && other.transform.GetComponent<Environment>() != null) {
			Bullet bull = GetComponent<Bullet> ();
			bull.leaveShellCasingOnGround ();
			if(GameMaster.logMessages) GameMaster.LogMsg (this.name + " has collided with " + other.gameObject.name, "OnCollisionEnter2D");
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : RLObject {
	protected Animator animator;
	protected Rigidbody2D rb;
	protected Transform trans;
	public UnitStats stats;
	public GameObject gun;
	public Weapon mainWeapon;

	public override void initialize(){
		base.initialize ();

		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
		trans = GetComponent<Transform> ();
		stats = new UnitStats (this.gameObject, new List<AbstractEffect>{new SprintEffect()});
		gun = gameObject.transform.Find ("Gun").gameObject;
		if (gun != null)
			mainWeapon = gun.transform.GetComponent<Weapon> ();
		else
			throw new InitializationException(gameObject.name + " is lacking a Gun child.");
		setUnitStats (stats);
		checkComponents ();
	}

	protected override void checkComponents(){
		base.checkComponents ();
		if (animator == null) {
			throw new InitializationException("Missing Animator component during initialization of Unit " + gameObject.name);
		}
		if (stats == null) {
			throw new InitializationException ("Missing Stats component during initialization of Unit" + gameObject.name);
		}
		if (trans == null) {
			throw new InitializationException ("Missing transform component during initialization of Unit" + gameObject.name);
		}
		if (rb == null) {
			throw new InitializationException ("Missing Rigidbody2D component during initialization of Unit" + gameObject.name);
		}
		if (mainWeapon == null) {
			throw new InitializationException ("Missing Weapon component during initialization of Unit" + gameObject.name);
		}
	}

	public void setUnitStats(UnitStats stats){
		this.stats = stats;
	}
}

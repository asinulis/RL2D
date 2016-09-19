using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats {
	public const int 	BASE_HP = 400;
	public const float 	BASE_SPEED = 2f;
	public const float 	BASE_ATTACK_RATE = 2f;
	public const float 	BASE_ATTACK_SPEED = 35f;
	public const int 	BASE_DAMAGE = 10;

	GameObject playerObject;

	public float hp;
	public float speed;
	public float attack_rate;
	public float attack_speed;
	public float luck;
	public int damage;
	List<AbstractEffect> triggerEffects = new List<AbstractEffect>();
	AbstractEffect currentTriggerEffect;
	List<AbstractEffect> activeEffects = new List<AbstractEffect> ();

	public UnitStats(GameObject playerObject, List<AbstractEffect> start_effects = null){
		this.playerObject = playerObject;
		if (start_effects != null) {
			foreach (AbstractEffect effect in start_effects) {
				addNewEffect (effect);
			}
		}
		updateStats ();
	}

	public void addNewEffect(AbstractEffect effect){
		if (effect.Type == EffectType.USE_ON_TRIGGER) {
			triggerEffects.Add (effect);
			if (currentTriggerEffect == null)
				currentTriggerEffect = effect;
		} else {
			addActiveEffect (effect);
		}
	}

	void addActiveEffect(AbstractEffect effect) {
		if (!activeEffects.Contains (effect)) {
			activeEffects.Add (effect);
			updateStats ();
		}
	}

	void removeActiveEffect(AbstractEffect effect) {
		if (activeEffects.Remove (effect)) {
			updateStats ();
		}
	}

	public void updateStats(){
		hp = BASE_HP;
		speed = BASE_SPEED;
		attack_rate = BASE_ATTACK_RATE;
		attack_speed = BASE_ATTACK_SPEED;
		damage = BASE_DAMAGE;
		foreach (AbstractEffect effect in activeEffects) {
			effect.apply (this, playerObject);
		}
	}

	public void triggerEffect (){
		if (currentTriggerEffect != null)
			addActiveEffect (currentTriggerEffect);
	}

	public void untriggerEffect(){
		if (currentTriggerEffect != null)
			removeActiveEffect (currentTriggerEffect);
	}
}

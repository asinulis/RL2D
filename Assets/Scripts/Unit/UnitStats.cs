using System.Collections.Generic;
using UnityEngine;

public class UnitStats {
	public const int BASE_HP = 100;
	public const float BASE_SPEED = 0.7f;
	public const float BASE_ATTACK_RATE = 0.3f;
	public const float BASE_ATTACK_SPEED = 0.6f;

	GameObject playerObject;
	public int hp;
	public float Speed;
	float attack_rate;
	float attack_speed;
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
		Speed = BASE_SPEED;
		attack_rate = BASE_ATTACK_RATE;
		attack_speed = BASE_ATTACK_SPEED;
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

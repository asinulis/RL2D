using UnityEngine;

public abstract class StatEffect : AbstractEffect {
	public override void apply(PlayerStats stats, GameObject player){
		applyStatChange (stats);
	}

	protected abstract void applyStatChange(PlayerStats stats);
}

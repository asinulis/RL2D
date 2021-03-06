﻿using UnityEngine;

public abstract class StatEffect : AbstractEffect {
	public override void apply(UnitStats stats, GameObject player)
	{
		applyStatChange (stats);
	}

	protected abstract void applyStatChange(UnitStats stats);
}

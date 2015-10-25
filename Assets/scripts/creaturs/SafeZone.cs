using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SafeZone:MonoBehaviour, ICreature
{
	public ECreature CreatureType()
	{
		return ECreature.SafeZone;
	}

	public void OnTriggerEnter2D(Collider2D colliderParam)
	{
		var pony = colliderParam.GetComponent<Pony>();
		if(pony != null) {
			pony.ResolveCommand(ECommands.BreakLink);
		}
	}

	public void ResolveCommand(ECommands commands, params object[] list) { }

	public Transform GetTransform()
	{
		return transform;
	}
}
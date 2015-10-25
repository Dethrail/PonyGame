using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RedPill:MonoBehaviour, ICreature
{
	public float Timer = 10f;
	private float _timer = 0;
	public ECreature CreatureType()
	{
		return ECreature.RedPill;
	}

	public void OnTriggerEnter2D(Collider2D colliderParam)
	{
		var dog = colliderParam.GetComponent<Dog>();
		if(dog != null) {
			Game.Instance.RedPilPicked();
			Destroy(gameObject);
		}
	}

	public void ResolveCommand(ECommands commands, params object[] list) { }

	public Transform GetTransform()
	{
		return transform;
	}

	private void Update()
	{
		_timer += Time.deltaTime;
		if (_timer > Timer) {
			Destroy(gameObject);
		}
	}
}
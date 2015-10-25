using System.Collections.Generic;
using UnityEngine;

public class Dog:MonoBehaviour, ICreature
{
	[HideInInspector]
	public bool Selected = false;
	[HideInInspector]
	public float Speed;
	public List<ICreature> Tail = new List<ICreature>();

	public bool IsMoving { get; private set; }
	public Vector3 TargetLocation { get; private set; }

	public ECreature CreatureType()
	{
		return ECreature.Dog;
	}

	public void OnTriggerEnter2D(Collider2D colliderParam)
	{
		var pony = colliderParam.GetComponent<Pony>();
		if(pony != null) {
			if(pony.IsFollowing) {
				return;
			}
			ICreature creature = pony;
			if(creature.CreatureType() == ECreature.Pony) {
				ResolveCommand(ECommands.Link, creature);
			}
		}
	}

	public void MoveTo(Vector2 targetLocation)
	{
		IsMoving = true;
		TargetLocation = targetLocation;
	}

	public void Update()
	{
		if(IsMoving) {
			if((transform.position - TargetLocation).sqrMagnitude > 0.01f) {
				transform.position = transform.position + ((TargetLocation - transform.position).normalized * Speed * Time.deltaTime);
			} else {
				IsMoving = false;
			}
		}
	}

	public void ResolveCommand(ECommands commands, params object[] list)
	{
		switch(commands) {
			case ECommands.Move: {
				if(Selected) {
					TargetLocation = (Vector2)list[0];
					IsMoving = true;
				}
				break;
			}
			case ECommands.Select: {
				Selected = (bool)list[0];
				break;
			}

			case ECommands.Link: {
				ICreature component = (ICreature)list[0];
				if(!(Tail.Contains(component))) {
					Tail.Add(component);
					component.ResolveCommand(ECommands.Link, this);
				}
				if(Tail.Count > 5) {
					Game.Instance.CreateBonus();
				}
				break;
			}
			case ECommands.BreakLink: {
				foreach(ICreature pony in Tail) {
					pony.ResolveCommand(ECommands.BreakLink);
				}
				Tail.Clear();
				//ICreature component = (ICreature)list[0];
				//if(Tail.Contains(component)) {
				//	Tail.Remove(component);
				//	component.ResolveCommand(ECommand.Unlink);
				//}
				break;
			}
		}
	}

	public Transform GetTransform()
	{
		return transform;
	}
}
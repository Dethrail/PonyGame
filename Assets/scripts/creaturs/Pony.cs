using UnityEngine;

public class Pony:MonoBehaviour, ICreature
{
	[HideInInspector]
	public Transform FollowTarget;
	[HideInInspector]
	public float Speed;
	public ICreature Head;
	public bool IsFollowing
	{
		get { return Head != null; }
	}

	public ECreature CreatureType()
	{
		return ECreature.Pony;
	}

	public Transform GetTransform()
	{
		return transform;
	}

	public void ResolveCommand(ECommands commands, params object[] list)
	{
		switch(commands) {
			case ECommands.Link: {
				FollowTarget = ((Dog)list[0]).transform;
				Head = (ICreature)list[0];
				break;
			}
			case ECommands.Select: {
				Debug.Log("Cant select pony");
				break;
			}
			case ECommands.BreakLink: {
				FollowTarget = null;
				Head = null;
				if(Game.Instance != null) { // spike for pass test (BreakLinkTest)
					Game.Instance.CollectPony(this);
					Destroy(gameObject);
				}
				break;
			}
		}
	}

	private void FixedUpdate()
	{
		if(FollowTarget != null) {
			var dt = ((FollowTarget.position - transform.position).normalized * Speed * Time.deltaTime);
			transform.Translate(dt);
		}
	}
}
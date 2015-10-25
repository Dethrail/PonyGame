using UnityEngine;
using System.Collections;

//public interface IInputHandler
//{
//	bool HandleInput(InputEvent ev, ICreature target);
//}

public enum InputEventType:byte
{
	Idle,
	Click, // scroll, hold...
}

public struct InputEvent
{
	public InputEventType Type;
	public Vector2 Origin;
}

internal interface IInput
{
	InputEvent GetEvent();
}

public class InputManager:MonoBehaviour
{
	public static InputManager Instance;
	private IInput _impl;
	//public IInputHandler CurrentTarget = null;
	public Vector2 CurPosition;

	private void Awake()
	{
		Instance = this;
#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
		//_impl = new TouchInput(); //TODO: implement
#else
		_impl = new MouseInput();
#endif
	}


	private void Update()
	{
		InputEvent ev = _impl.GetEvent();
		if(ev.Type == InputEventType.Click) {
			//Debug.Log("click pos=" + ev.Origin);
			Ray ray = Camera.main.ScreenPointToRay(ev.Origin);
			RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
			bool hasSomeOrder = false;
			RaycastHit2D moveHit = default(RaycastHit2D);
			foreach(RaycastHit2D hit in hits) {
				var creature = hit.collider.gameObject.GetComponent<ICreature>();
				if(creature != null) {
					if(creature.CreatureType() == ECreature.Dog) {
						Game.Instance.Fermer.Select(creature);
						hasSomeOrder = true;
					}
				} else {
					moveHit = hit;
				}
			}

			if(!hasSomeOrder && moveHit != default(RaycastHit2D)) {
				foreach(var dog in Game.Instance.Fermer.Dogs) {
					dog.ResolveCommand(ECommands.Move, moveHit.point);
				}
			}
		}
	}
}
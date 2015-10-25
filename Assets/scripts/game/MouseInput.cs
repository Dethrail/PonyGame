using UnityEngine;

internal class MouseInput:IInput
{

	public const float ClickConst = 0.2f;

	private float _clickDuration;
	private bool _trackClick;

	public InputEvent GetEvent()
	{
		var rect = new Rect(0, 0, Screen.width, Screen.height);
		bool currState = Input.GetMouseButton(0);
		Vector2 currPos = Input.mousePosition;
		
		InputEvent ret = new InputEvent() { Type = InputEventType.Idle, Origin = Vector2.zero };
		if(currState && !_trackClick) {
			_trackClick = true;
			_clickDuration = 0;
		}
		if(currState) {
			_clickDuration += Time.deltaTime;
		} else {

			if(_trackClick && _clickDuration < ClickConst && rect.Contains(currPos)) {
				_trackClick = false;
				_clickDuration = 0;

				ret = new InputEvent() { Type = InputEventType.Click, Origin = currPos };
				return ret;
			} else {
				_trackClick = false;
			}
		}
		return ret;
	}
}
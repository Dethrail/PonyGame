using JetBrains.Annotations;
using UnityEngine;

public interface ICreature
{
	ECreature CreatureType();
	Transform GetTransform();
	void ResolveCommand(ECommands commands, params object[] list);
}

public enum ECreature
{
	Dog,
	Pony,
	SafeZone,
	RedPill,
	Count,
}
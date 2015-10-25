using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Fermer
{
	//public static Fermer Instance;
	public List<ICreature> Dogs;
	public List<ICreature> Ponies;

	public Fermer(List<ICreature> dogs, List<ICreature> ponies)
	{
		//if(Instance == null) {
		//	Instance = this;
		//} else {
		//	throw new Exception("Can't reset fermer instance");
		//}
		Dogs = dogs;
		Ponies = ponies;
	}

	public void Select(ICreature target)
	{
		foreach(var dog in Dogs) {
			dog.ResolveCommand(ECommands.Select, dog == target);
		}
	}
}
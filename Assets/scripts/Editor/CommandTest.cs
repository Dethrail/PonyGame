//Read root/README.md

//using System.Collections.Generic;
//using System.Threading;
//using UnityEngine;
//using System.Collections;
//using NUnit.Framework;

//[TestFixture]
//[Category("Unity3d tests")]
//public class CommandTest
//{
//	private Fermer _fermer;
//	private Dog _dog1;
//	private Dog _dog2;
//	private Dog _dog3;

//	private Pony _pony1;
//	private Pony _pony2;
//	private Pony _pony3;
//	private Pony _pony4;
//	private List<ICreature> _dogs;
//	private List<ICreature> _ponyies;

//	[SetUp]
//	public void Setup()
//	{
//		GameObject go = new GameObject("dog1");
//		_dog1 = go.AddComponent<Dog>();
//		go = new GameObject("dog2");
//		_dog2 = go.AddComponent<Dog>();
//		go = new GameObject("dog3");
//		_dog3 = go.AddComponent<Dog>();

//		go = new GameObject("pony1");
//		_pony1 = go.AddComponent<Pony>();
//		go = new GameObject("pony2");
//		_pony2 = go.AddComponent<Pony>();
//		go = new GameObject("pony3");
//		_pony3 = go.AddComponent<Pony>();
//		go = new GameObject("pony4");
//		_pony4 = go.AddComponent<Pony>();
//		_dogs = new List<ICreature>() { _dog1, _dog2, _dog3 };
//		_ponyies = new List<ICreature>() { _pony1, _pony2, _pony3, _pony4 };
//		_fermer = new Fermer(_dogs, _ponyies);
//	}

//	[Test]
//	[Category("three dogs")]
//	public void DogCountControll()
//	{
//		Assert.AreEqual(3, _fermer.Dogs.Count);
//	}

//	[Test]
//	[Category("SelectionTest")]
//	public void SelectionTest()
//	{
//		_fermer.Select(_dog3);
//		_fermer.Select(_dog2);
//		_fermer.Select(_dog1);

//		Assert.IsTrue(_dog1.Selected && !_dog2.Selected && !_dog3.Selected);
//	}

//	[Test]
//	[Category("MoveTest")]
//	public void MoveTest()
//	{
//		_fermer.Select(_dog1);
//		Vector3 targetPos = new Vector3(1, 2, 3);
//		foreach(var creature in _fermer.Dogs) {
//			creature.ResolveCommand(ECommands.Move, targetPos);
//		}
//		Vector3 startPos = _dog1.transform.position;
//		_dog1.Speed = 100f;
//		_dog1.Update(); // very small dif in timestamp, very small move :)
//		Assert.IsTrue((startPos - targetPos).sqrMagnitude > (_dog1.transform.position - targetPos).sqrMagnitude); //distance check
//		Assert.AreNotEqual(startPos.x, _dog1.transform.position.x);
//		Assert.AreNotEqual(startPos.y, _dog1.transform.position.y);
//		Assert.AreNotEqual(startPos.z, _dog1.transform.position.z);
//		//Debug.Log((startPos - targetPos).sqrMagnitude - (_dog1.transform.position - targetPos).sqrMagnitude);
//	}

//	[Test]
//	[Category("LinkPoniesToDog")]
//	public void LinkPoniesToDog()
//	{
//		//Debug.Log(_dog1.gameObject == null);
//		ICreature creature = (_dog1);
//		creature.ResolveCommand(ECommands.Link, _pony1);
//		creature = (_dog2);
//		creature.ResolveCommand(ECommands.Link, _pony2);
//		Assert.IsTrue(_dog1.Tail.Count == 1 && _dog2.Tail.Count == 1 && (Dog)(_pony1.Head) == _dog1 && (Dog)(_pony2.Head) == _dog2); // link check
//	}

//	[Test]
//	[Category("BreakLinkTest")]
//	public void BreakLinkTest()
//	{
//		//Debug.Log(_dog1.gameObject == null);
//		ICreature creature = (_dog1);
//		creature.ResolveCommand(ECommands.Link, _pony1);
//		creature.ResolveCommand(ECommands.Link, _pony2);
//		creature.ResolveCommand(ECommands.BreakLink);
//		Assert.IsTrue(_dog1.Tail.Count == 0 && !_pony1.IsFollowing && !_pony1.IsFollowing ); // link check
//	}

//	[TearDown]
//	public void DestroyDogs()
//	{
//		foreach(var dog in _dogs) {
//			GameObject.DestroyImmediate(dog.GetTransform().gameObject);
//		}

//		foreach(var pony in _ponyies) {
//			GameObject.DestroyImmediate(pony.GetTransform().gameObject);
//		}
//	}
//}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game:MonoBehaviour
{
	private static Game _instance = null;
	public static Game Instance
	{
		get { return _instance; }
		private set
		{
			if(_instance == null) {
				_instance = value;
			} else {
				Debug.LogError("Can't reset Game Instance");
			}
		}
	}

	public bool GameStarted = false;
	public Dog PrefabDog;
	public Pony PrefabPony;
	public RedPill PrefabPil;
	public Fermer Fermer;
	public BoxCollider2D Backplate;
	public Transform Grass;

	public Text Timer;
	private float _timeFromstart;

	public int DogCount;
	public int PonyCount;
	private Vector3 _lowerLeft;
	private Vector3 _upperRight;

	private const float Bound = 0.5f;
	private const float DogsSpeed = 1.5f;
	private const float PoniesSpeed = 1.0f;

	private void DestroyObjects()
	{
		foreach(var dog in Fermer.Dogs) {
			Destroy(dog.GetTransform().gameObject);
		}
		foreach(var pony in Fermer.Ponies) {
			Destroy(pony.GetTransform().gameObject);
		}

		var pils = FindObjectsOfType<RedPill>().ToArray();
		foreach (var redPill in pils) {
			Destroy(redPill.gameObject);
		}
	}
	public void RestartGame()
	{
		GameStarted = false;
		DestroyObjects();
		StartGame(null);
	}

	public void StartGame(Button startButton)
	{
		if(GameStarted) {
			RestartGame();
			return;
		}

		GameStarted = true;
		_timeFromstart = 0;
		Time.timeScale = 1; // unpause game

		if(startButton != null) {
			startButton.GetComponentInChildren<Text>().text = "Restart";
		}
		_lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		_upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		_upperRight.y -= 1f; // for top buttons
		Backplate.size = _upperRight - _lowerLeft;
		Grass.localScale = _upperRight - _lowerLeft;

		var dogs = new Dog[DogCount];
		for(int i = 0; i < DogCount; i++) {
			dogs[i] = Instantiate(PrefabDog.gameObject).GetComponent<Dog>();
			dogs[i].Speed = DogsSpeed;
			dogs[i].transform.position = new Vector2(Random.Range(_lowerLeft.x + Bound, _upperRight.x - Bound), Random.Range(_lowerLeft.y + Bound, _upperRight.y - Bound));
		}

		var ponies = new Pony[PonyCount];
		for(int i = 0; i < PonyCount; i++) {
			ponies[i] = Instantiate(PrefabPony.gameObject).GetComponent<Pony>();
			ponies[i].Speed = PoniesSpeed;
			ponies[i].transform.position = new Vector2(Random.Range(_lowerLeft.x + Bound, _upperRight.x - Bound), Random.Range(_lowerLeft.y + Bound, _upperRight.y - Bound));
		}

		var iDogs = new List<ICreature>(dogs);
		var iPonies = new List<ICreature>(ponies);
		Fermer = new Fermer(iDogs, iPonies);
	}

	private void Awake()
	{
		Instance = this;
	}

	public void CreateBonus()
	{
		var pil = Instantiate(PrefabPil.gameObject).GetComponent<RedPill>();
		pil.transform.position = new Vector2(Random.Range(_lowerLeft.x + Bound, _upperRight.x - Bound), Random.Range(_lowerLeft.y + Bound, _upperRight.y - Bound));
	}

	public void CollectPony(ICreature pony)
	{
		Fermer.Ponies.Remove(pony);
		if (Fermer.Ponies.Count == 0) {
			GameStarted = false;
			DestroyObjects();
			Time.timeScale = 0;// pause game
		}
	}

	private void Update()
	{
		if(GameStarted) {
			_timeFromstart += Time.deltaTime;
			Timer.text = "Time: " + _timeFromstart;
		}
	}

	public void RedPilPicked()
	{
		_timeFromstart = 0;
	}
}

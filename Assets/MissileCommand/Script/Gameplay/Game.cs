using DrewBuchanan.Model;
using System.Collections.Generic;
using UnityEngine;

namespace MissileCommand.Gameplay
{
	public class Game : Singleton<Game>
	{
		public List<City> cities;
		UI.LossScreen lossScreen;
		
		public float spawnRate;
		public int enemiesToSpawnMin = 1;
		public int enemiesToSpawnMax = 3;
		float counter = 0;

		public SimpleJSON.JSONNode config;
		public string filePath;
		public TextAsset defaultConfig;

		void Awake ()
		{
			filePath = Application.persistentDataPath + "/MCConfig.t13";
			System.IO.FileInfo info = new System.IO.FileInfo (filePath);
			if (info != null && info.Exists == true)
			{
				config = SimpleJSON.JSON.Parse (System.IO.File.ReadAllText (filePath));
			}
			else
			{
				config = SimpleJSON.JSON.Parse (defaultConfig.text);
			}
		}

		void Start ()
		{
			lossScreen = GetComponent<UI.LossScreen> ();
			enemiesToSpawnMin = config ["EnemySpawnMin"].AsInt;
			enemiesToSpawnMax = config ["EnemySpawnMax"].AsInt + 1;
			spawnRate = config ["EnemySpawnRate"].AsFloat;
		}

		void Update ()
		{
			if (AllCitiesDead ())
			{
				Lose ();
			}
			else
			{
				counter += Time.deltaTime;
				if (counter > spawnRate)
				{
					int to = Random.Range (enemiesToSpawnMin, enemiesToSpawnMax);
					for (int i = 0; i < to; i++)
					{
						SpawnEnemy ();
					}
					counter = 0;
				}
			}
		}

		private void SpawnEnemy ()
		{
			GameObject enemy = ObjectPool.Instance.GetObjectOfType ("Enemy", false); ;
			enemy.transform.position = transform.position;
			enemy.GetComponent<Enemy> ().direction = (Vector2)(enemy.transform.position - GetTargetCity ().position) * -1;
		}

		private bool AllCitiesDead ()
		{
			bool all = true;
			for (int i = 0; i < cities.Count; i++)
			{
				if (cities [i].health > 1)
				{
					all = false;
					break;
				}
			}

			return all;
		}

		private void Lose ()
		{
			Time.timeScale = 0;
			lossScreen.lossScreen.SetActive (true);
		}

		public Transform GetTargetCity ()
		{
			List<City> live = new List<City> ();
			for (int i = 0; i < cities.Count; i++)
			{
				if (cities [i].health > 1)
				{
					live.Add (cities [i]);
				}
			}

			return live [Random.Range (0, live.Count)].transform;
		}
	}
}
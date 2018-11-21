using UnityEngine;
using System.Collections;
using DrewBuchanan.Model;

namespace FlappyClone.Gameplay
{
	public class ObstacleGenerator : MonoBehaviour
	{
		bool genning;
		public bool isPaused;

		void Start ()
		{
			genning = true;
			Player.onPlayerDiedE += Player_onPlayerDiedE;
			StartCoroutine (GenerateObstacle ());
		}

		void OnDisable ()
		{
			Player.onPlayerDiedE -= Player_onPlayerDiedE;
		}

		IEnumerator GenerateObstacle ()
		{
			while (genning)
			{
				GameObject newObs = ObjectPool.Instance.GetObjectOfType ("Obstacle", false);
				newObs.GetComponent<Obstacle> ().generator = this;
				float wait = Random.Range (1.5f, 2.5f);
				float elapsed = 0;
				while (elapsed < wait)
				{
					if (genning)
					{
						elapsed += Time.deltaTime;
					}
					yield return null;
				}
			}
		}

		public void Pause ()
		{
			genning = false;
			isPaused = true;
		}

		public void Unpause ()
		{
			genning = true;
			isPaused = false;
		}

		void Player_onPlayerDiedE ()
		{
			genning = false;
		}
	}
}
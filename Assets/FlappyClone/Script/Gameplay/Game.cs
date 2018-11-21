using UnityEngine;

namespace FlappyClone.Gameplay
{
	public class Game : MonoBehaviour
	{
		public delegate void ScoreChangedHandler (int score);
		public static event ScoreChangedHandler onScoreChangedE;

		public delegate void GameStartedHandler ();
		public static event GameStartedHandler onGameStartedE;

		public delegate void GameEndedHandler ();
		public static event GameEndedHandler onGameEndedE;

		int _score;
		public int score
		{
			get { return _score; }
			set
			{
				_score = value;
				if (onScoreChangedE != null)
				{
					onScoreChangedE (_score);
				}
			}
		}
		int bonus;

		void Start ()
		{
			score = 0;
		}

		void OnEnable ()
		{
			if (onGameStartedE != null)
			{
				onGameStartedE ();
			}

			Obstacle.onObstaclePassedE += Obstacle_onObstaclePassedE;
			Player.onPlayerDiedE += Player_onPlayerDiedE;
		}

		private void Update ()
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
			}
		}

		void OnDisable ()
		{
			Obstacle.onObstaclePassedE -= Obstacle_onObstaclePassedE;
			Player.onPlayerDiedE -= Player_onPlayerDiedE;
		}

		void Obstacle_onObstaclePassedE ()
		{
			score++;
		}

		void Player_onPlayerDiedE ()
		{
			if (onGameEndedE != null)
			{
				onGameEndedE ();
			}
		}
	}
}
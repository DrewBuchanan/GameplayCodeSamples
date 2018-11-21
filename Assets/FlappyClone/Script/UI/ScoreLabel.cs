using UnityEngine;
using UnityEngine.UI;

namespace FlappyClone.UI
{
	public class ScoreLabel : MonoBehaviour
	{
		private Text label;

		private void Awake ()
		{
			label = GetComponent<Text> ();
		}

		void OnEnable ()
		{
			Gameplay.Game.onScoreChangedE += Game_onScoreChangedE;
		}

		private void OnDisable ()
		{
			Gameplay.Game.onScoreChangedE -= Game_onScoreChangedE;
		}

		private void Game_onScoreChangedE (int score)
		{
			label.text = "Score: " + score.ToString ();
		}
	}
}
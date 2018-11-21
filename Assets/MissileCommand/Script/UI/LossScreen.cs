using UnityEngine;
using UnityEngine.SceneManagement;

namespace MissileCommand.UI
{
	public class LossScreen : MonoBehaviour
	{
		public GameObject lossScreen;

		public void RestartLevel ()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
}
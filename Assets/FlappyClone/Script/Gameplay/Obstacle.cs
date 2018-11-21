using DrewBuchanan.Model;
using UnityEngine;

namespace FlappyClone.Gameplay
{
	public class Obstacle : MonoBehaviour
	{
		public delegate void PassedObstacleHandler ();
		public static event PassedObstacleHandler onObstaclePassedE;

		public ObstacleGenerator generator;
		private bool hasEntered;
		public Vector3 velocity;
		Transform cacheTrans;
		private static Vector3 initPosition = new Vector3 (8, 0, 0);
		private static Vector3 offScreenPosition = new Vector3 (-9999, 0, 0);

		void Awake ()
		{
			name = "Obstacle";
			cacheTrans = GetComponent<Transform> ();
		}

		void OnEnable ()
		{
			hasEntered = false;
			cacheTrans.position = initPosition + (Vector3.up * Random.Range (-3, 3));
		}

		void OnDisable ()
		{
			cacheTrans.position = offScreenPosition;
		}

		void Update ()
		{
			if (!Player.hasDied && !generator.isPaused)
			{
				cacheTrans.Translate (velocity * Time.smoothDeltaTime);
			}

			if (!IsVisible ())
			{
				Deactivate ();
			}
		}

		bool IsVisible ()
		{
			bool rtrn = true;
			if (transform.position.x < -20)
			{
				rtrn = false;
			}

			return rtrn;
		}

		void Deactivate ()
		{
			ObjectPool.Instance.PoolObject (gameObject);
		}

		void OnTriggerEnter (Collider other)
		{
			if (!hasEntered && other.name == "Player")
			{
				if (onObstaclePassedE != null)
				{
					onObstaclePassedE ();
				}

				hasEntered = true;
			}
		}
	}
}
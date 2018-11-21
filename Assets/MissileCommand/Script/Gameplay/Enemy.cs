using UnityEngine;

namespace MissileCommand.Gameplay
{
	public class Enemy : MonoBehaviour
	{
		public Vector2 direction;
		public float speed;

		void Start ()
		{
			speed = Game.Instance.config ["EnemySpeed"].AsFloat;
		}

		void Update ()
		{
			transform.localPosition += (Vector3)direction * speed * Time.deltaTime;
		}

		void OnTriggerEnter (Collider other)
		{
			if (!other.GetComponent<Enemy> ())
			{
				Destroy (this.gameObject);
			}

			if (other.GetComponent<Bullet> ())
			{
				if (!other.GetComponent<Bullet> ().exploding)
				{
					Destroy (other.gameObject);
				}
			}
		}
	}
}
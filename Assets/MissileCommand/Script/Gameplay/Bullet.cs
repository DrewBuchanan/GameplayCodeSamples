using DrewBuchanan.Model;
using System.Collections;
using UnityEngine;

namespace MissileCommand.Gameplay
{
	public class Bullet : MonoBehaviour
	{
		public Vector2 direction;
		public Vector2 explodePos;
		public float speed;
		public bool exploding = false;
		public float distanceFromClick = 0.5f;
		public float maxExplosionRadius = 4.0f;
		public float explosionDuration = 1;

		void Update ()
		{
			if (!exploding)
			{
				transform.position += ((Vector3)direction * speed) * Time.deltaTime;
				if (Vector2.Distance ((Vector2)transform.position, explodePos) < distanceFromClick)
				{
					StartCoroutine (Explode ());
				}
			}
		}

		IEnumerator Explode ()
		{
			exploding = true;
			Transform child = transform.GetChild (0);
			SphereCollider collider = GetComponent<SphereCollider> ();
			GetComponent<MeshFilter> ().mesh = null;
			float elapsed = 0;
			float dur = explosionDuration;
			while (elapsed < dur)
			{
				float rad = Mathf.Lerp (0, maxExplosionRadius, elapsed / dur);
				if (rad > 0.5f)
				{
					collider.radius = rad;
				}

				child.transform.localScale = Vector3.one * 2 * rad;
				elapsed += Time.deltaTime;
				yield return null;
			}
			exploding = false;
			child.transform.localScale = Vector3.one * 0.25f;
			collider.radius = 0.5f;
			ObjectPool.Instance.PoolObject (gameObject);
		}
	}
}
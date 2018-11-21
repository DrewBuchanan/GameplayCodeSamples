using System.Collections;
using UnityEngine;

namespace FlappyClone.Gameplay
{
	[RequireComponent (typeof (Rigidbody))]
	public class Player : MonoBehaviour
	{
		public static bool hasDied;
		public bool isPaused;

		public delegate void PlayerDeathHandler ();
		public static event PlayerDeathHandler onPlayerDiedE;

		public delegate void PlayerDeathAnimationHandler ();
		public static event PlayerDeathAnimationHandler onDeathAnimFinishedE;

		public delegate void PlayerMeshHandler (string name);
		public static event PlayerMeshHandler onPlayerMeshChangedE;

		public Vector3 jumpForce;
		public ParticleSystem impactParticle;

		private Transform cacheTrans;
		private Rigidbody cacheRigidBody;
		private Transform meshTrans;

		private Vector3 cacheVel;

		void Awake ()
		{
			hasDied = false;
			cacheTrans = GetComponent<Transform> ();
			cacheRigidBody = GetComponent<Rigidbody> ();
			meshTrans = cacheTrans;
		}

		void Update ()
		{
			if (!hasDied && !isPaused)
			{
				if (Input.GetKeyDown ("space") || (Input.touchCount == 1 && Input.touches [0].phase == TouchPhase.Began))
				{
					// TODO: Play "Flap" sound here
					cacheRigidBody.velocity = Vector3.zero;
					cacheRigidBody.AddForce (jumpForce);
				}
				if (Mathf.Abs (cacheTrans.position.y) >= 6f)
				{
					// TODO: Play "Flew too high" sound here
					Die ();
				}
			}
			meshTrans.localRotation = Quaternion.Euler (new Vector3 (0, 0, cacheRigidBody.velocity.y * 3));
		}

		void OnTriggerEnter (Collider other)
		{
			if (!hasDied)
			{
				if (other.name == "Wall" || other.name == "Floor")
				{
					if (other.name == "Wall")
					{
						// TODO: Play "collided with obstacle" sound here
					}
					else if (other.name == "Floor")
					{
						// TODO: Play "collided with floor" sound here
					}
					Die ();
				}
			}
		}

		void Die ()
		{
			if (impactParticle != null)
			{
				// TODO: Play "explode/poof" sound here
				impactParticle.Play ();
			}
			if (impactParticle != null)
			{
				impactParticle.Play ();
			}

			hasDied = true;
			StartCoroutine (DeathAnim ());
		}

		IEnumerator DeathAnim ()
		{
			if (onPlayerDiedE != null)
			{
				onPlayerDiedE ();
			}

			yield return new WaitForSeconds (1.5f);
			if (onDeathAnimFinishedE != null)
			{
				onDeathAnimFinishedE ();
			}
		}

		public void Pause ()
		{
			enabled = false;
			isPaused = true;
			cacheVel = cacheRigidBody.velocity;
			cacheRigidBody.velocity = Vector3.zero;
			cacheRigidBody.useGravity = false;
		}

		public void Unpause ()
		{
			enabled = true;
			isPaused = false;
			cacheRigidBody.velocity = cacheVel;
			cacheRigidBody.useGravity = true;
		}
	}
}
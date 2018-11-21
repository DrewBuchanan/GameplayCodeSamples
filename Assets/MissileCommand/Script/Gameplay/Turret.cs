using DrewBuchanan.Model;
using UnityEngine;

namespace MissileCommand.Gameplay
{
	public class Turret : MonoBehaviour
	{
		public GameObject bulletPrefab;

		private void Update ()
		{
			if (Input.GetMouseButtonUp (0))
			{
				Fire (Input.mousePosition);
			}
		}

		private void Fire (Vector2 pos)
		{
			GameObject bullet = ObjectPool.Instance.GetObjectOfType ("Bullet", false);
			bullet.transform.position = transform.position;
			Bullet component = bullet.GetComponent<Bullet> ();
			component.direction = ((Camera.main.ScreenToWorldPoint (new Vector3 (pos.x, pos.y, 5.7f)) - transform.position));
			component.explodePos = Camera.main.ScreenToWorldPoint (new Vector3 (pos.x, pos.y, 5.7f));
		}
	}
}
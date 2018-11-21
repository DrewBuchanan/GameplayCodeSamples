using UnityEngine;
using UnityEngine.UI;
using DrewBuchanan.Model;

namespace MissileCommand.Gameplay
{
	public class City : MonoBehaviour
	{
		public int health;
		public int maxHealth;
		public int collisionDamage;
		public Slider healthSlider;
		public Game game;

		void Start ()
		{
			maxHealth = Game.Instance.config ["CityMaxHealth"].AsInt;
			collisionDamage = Game.Instance.config ["CityCollisionDamage"].AsInt;
			healthSlider.minValue = 0;
			healthSlider.maxValue = maxHealth;
		}

		void Update ()
		{
			healthSlider.value = health;
		}

		void OnTriggerEnter (Collider other)
		{
			ObjectPool.Instance.PoolObject (other.gameObject);
			health -= collisionDamage;
		}
	}
}
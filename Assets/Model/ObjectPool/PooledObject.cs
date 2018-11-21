using UnityEngine;

namespace DrewBuchanan.Model
{
	[System.Serializable]
	public class PooledObject
	{
		public string objectName;
		public GameObject obj;
		public int amountToBuffer;
	}
}
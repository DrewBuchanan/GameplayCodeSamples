using System.Collections.Generic;
using UnityEngine;

namespace DrewBuchanan.Model
{
	public class ObjectPool : Singleton<ObjectPool>
	{
		public List<PooledObject> objects;
		protected virtual List<GameObject> [] pooledObjects { get; set; }
		protected virtual Transform containerObject { get; set; }

		public virtual void Awake ()
		{
			containerObject = new GameObject ("PooledObjectContainer").transform;
			pooledObjects = new List<GameObject> [objects.Count];
			for (int i = 0; i < objects.Count; i++)
			{
				pooledObjects [i] = new List<GameObject> ();
				List<GameObject> buffer = new List<GameObject> ();
				for (int j = 0; j < objects [i].amountToBuffer; j++)
				{
					buffer.Add (GetObjectOfType (objects [i].objectName, false));
				}
				for (int j = 0; j < buffer.Count; j++)
				{
					PoolObject (buffer [j]);
				}
			}
		}

		public virtual GameObject GetObjectOfType (string objectName, bool onlyPooled)
		{
			for (int i = 0; i < objects.Count; i++)
			{
				if (objects [i].objectName == objectName)
				{
					if (pooledObjects [i].Count > 0)
					{
						GameObject pooledObj = pooledObjects [i] [0];
						pooledObjects [i].RemoveAt (0);
						pooledObj.transform.parent = null;
						pooledObj.SetActive (true);
						return pooledObj;
					}
					else if (!onlyPooled)
					{
						GameObject newObj = (GameObject)Instantiate (objects [i].obj);
						newObj.name = objectName;
						return newObj;
					}

					break;
				}
			}
			Debug.LogError ("OBJECT NOT FOUND OF NAME " + objectName);
			return null;
		}

		public virtual void PoolObject (GameObject obj)
		{
			for (int i = 0; i < objects.Count; i++)
			{
				if (objects [i].objectName == obj.name)
				{
					obj.SetActive (false);
					obj.transform.parent = containerObject;
					pooledObjects [i].Add (obj);
					return;
				}
			}
		}
	}
}
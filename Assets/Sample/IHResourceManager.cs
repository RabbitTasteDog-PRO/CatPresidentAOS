using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace HOW
{

	public enum eGlobalResource 
	{
		NetworkTouchBlock = 0, 
		TouchBlock, 
		ViewTransferMain,
		ViewTransferBlackInOut,
		MessageBar,
		COUNT
	}

	public partial class IHResourceManager : MonoBehaviour {


		public Dictionary <string, GameObject> dicGlobalResources;





		void Awake()
		{
			DontDestroyOnLoad( gameObject );
		}



		public GameObject GetGlobalResource ( string _key )
		{
			if (dicGlobalResources.ContainsKey (_key)) 
			{
				GameObject prefab = dicGlobalResources [_key];
				GameObject o = Instantiate (prefab, this.transform);
				o.SetActive (false);
				return o;
			}
			return null;
		}




		public GameObject InstantiateGameObjectResource ( string _path, Transform _root )
		{
			GameObject prefab = Resources.Load (_path) as GameObject;
			if (prefab != null) {
				GameObject o = Instantiate (prefab, _root) as GameObject;
				return o;
			}
			return null;
		}
	}
}


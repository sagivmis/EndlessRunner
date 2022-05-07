using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Account : MonoBehaviour
{
	[DllImport("__Internal")] private static extern string WalletAddress();

	void Start()
	{

    }

}
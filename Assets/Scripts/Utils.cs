using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	/// <summary>
	/// Gets the random enum.
	/// </summary>
	/// <returns>The random enum.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetRandomEnum<T>()
	{
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
		return V;
	}
	
		 
	
}

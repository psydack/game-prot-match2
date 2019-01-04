using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {
	
	/// <summary>
	/// The instance.
	/// </summary>
	public static InputControl instance;

	/// <summary>
	/// fire when touch and when is touching
	/// </summary>
	public Touched touched;
	public delegate void Touched(int ID, Vector2 position);
	
	/// <summary>
	/// fire when touch is out
	/// </summary>
	public TouchedUp touchedUp;
	public delegate void TouchedUp(int ID, Vector2 position);
	
	/// <summary>
	/// fire when touch is touched first time (began)
	/// </summary>
	public TouchedDown touchedDown;
	public delegate void TouchedDown(int ID, Vector2 position);	

	/// <summary>
	/// The touch drag.
	/// </summary>
	public TouchDrag touchDrag;
	public delegate void TouchDrag(int ID, Vector2 position);
	
	// Use this for initialization
	void Awake () 
	{
		if( !instance ) instance = this;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		#if UNITY_EDITOR
		CheckPC();
		
		#elif UNITY_ANDROID
		CheckMobile();

		#elif UNITY_IOS
		CheckMobile();

		#elif UNITY_WP8 
		CheckMobile();

		#else 
		CheckPC();
		
		#endif
	}
	
	/// <summary>
	/// Checks the P.
	/// </summary>
	void CheckPC()
	{
		if(Input.GetMouseButton(0))
		{
			if(touched != null)
			{
				touched(0, Input.mousePosition);
			}

			if(touchDrag != null)
			{
				touchDrag(0, Input.mousePosition);
			}
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			if(touchedDown != null)
			{
				touchedDown(0, Input.mousePosition);
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(touchedUp != null)
			{
				touchedUp(0, Input.mousePosition);
			}
		}
	}
	
	#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 
	/// <summary>
	/// Checks the mobile.
	/// </summary>
	void CheckMobile()
	{
		if(Input.touchCount > 0)
		{	
			foreach(Touch touch in Input.touches)
			{
				if((touch.phase == TouchPhase.Stationary) || (touch.phase == TouchPhase.Moved))
				{
					if(touchDrag != null) 
					{
						touchDrag(touch.fingerId, touch.position);
					}

					if(touched != null) touched(touch.fingerId, touch.position);

				}
				if(touch.phase == TouchPhase.Began)
				{
					if(touchedDown != null) touchedDown(touch.fingerId, touch.position);
				}
				else if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
				{
					if(touchedUp != null) touchedUp(touch.fingerId, touch.position);
				}
			}
		}
	}
	#endif
	
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickList : MonoBehaviour {
	
	/// <summary>
	/// The instance.
	/// </summary>
	public static BrickList instance;
	
	///
	public enum BRICK_TYPE
	{
		NONE,
		RED,
		BLUE,
		GREEN,
		PINK,
		ORANGE,
		BROWN,
		YELLOW
	}
	
	//
	public Sprite gemRed;
	public Sprite gemBlue;
	public Sprite gemGreen;
	public Sprite gemPink;
	public Sprite gemOrange;
	public Sprite gemBrown;
	public Sprite gemYellow;
	
	
	
	/// <summary>
	/// Use for intialization
	/// </summary>
	void Awake () {
		if( !instance ) instance = this;
	}
	
	/// <summary>
	/// Gets the type of the sprite by.
	/// </summary>
	/// <returns>The sprite by type.</returns>
	/// <param name="tipo">Tipo.</param>
	public Sprite GetSpriteByType ( BrickList.BRICK_TYPE tipo ) 
	{
		Sprite spr = null;
		switch( tipo )
		{
			case BRICK_TYPE.RED:
				spr = gemRed;
				break;
			case BRICK_TYPE.GREEN:
				spr = gemGreen;
				break;
			case BRICK_TYPE.BLUE:
				spr = gemBlue;
				break;
			case BRICK_TYPE.PINK:
				spr = gemPink;	
				break;
			case BRICK_TYPE.ORANGE:
				spr = gemOrange;
				break;
			case BRICK_TYPE.BROWN:
				spr = gemBrown;
				break;
			case BRICK_TYPE.YELLOW:
				spr = gemYellow;
				break;
		}
		
		return spr;
	}
	
	public static bool CheckIsNULL( ref List<GemBrick> listGems, GemBrick gem)
	{
		if( gem.brickType != BrickList.BRICK_TYPE.NONE )
		{
			listGems.Add( gem );
			return true;
		}
		
		return false;
	}
}

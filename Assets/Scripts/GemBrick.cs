using UnityEngine;
using System.Collections;

public class GemBrick : MonoBehaviour {
	
	/// <summary>
	/// The type of the brick.
	/// </summary>
	public BrickList.BRICK_TYPE brickType = BrickList.BRICK_TYPE.GREEN;

	public int rowID = 0;
	public int colID = 0;

	/// <summary>
	/// The collider
	/// </summary>
	BoxCollider2D col;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		col = GetComponent<BoxCollider2D>();
		InputControl.instance.touchedDown += TouchedDown;
	}
	
	/// <summary>
	/// Toucheds down.
	/// </summary>
	/// <param name="ID">I.</param>
	/// <param name="position">Position.</param>
	void TouchedDown (int ID, Vector2 position) 
	{
//		if( !GameControl.instance.canUpdate ) return;
		
		Vector3 _pos = Camera.main.ScreenToWorldPoint( position );
		_pos.z = 0;
		
		if( col.bounds.Contains( _pos ) )
		{
//			GameControl.instance.ChangeFaixas( botaoID );
			if( this.brickType == BrickList.BRICK_TYPE.NONE ) GemManager.instance.StartCheckGems( this );
			//print ( this.colID + " " + this.rowID);
		}
	}
	
	
	/// <summary>
	/// Changes the type.
	/// </summary>
	/// <param name="tipo">Tipo.</param>
	public void ChangeType( BrickList.BRICK_TYPE tipo )
	{
		brickType = tipo;
		transform.GetComponent<SpriteRenderer>().sprite = BrickList.instance.GetSpriteByType( tipo );		
	}
	
	public void SetColRow( int _col, int _row )
	{
		this.rowID = _row;
		this.colID = _col;
	}
}

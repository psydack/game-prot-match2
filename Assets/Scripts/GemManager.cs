using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GemManager : MonoBehaviour {
	
	public static GemManager instance;
	
	/// <summary>
	/// The gem container.
	/// </summary>
	public Transform gemContainer;
	
	/// <summary>
	/// The gem brick.
	/// </summary>
	public GameObject gemBrick;
	
	/// <summary>
	/// The gems.
	/// </summary>
	public List<List<GemBrick>> gems = new List<List<GemBrick>>();
	
	
	/// <summary>
	/// Initialize
	/// </summary>
	void Awake()
	{
		if( !instance ) instance = this;
	}
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		float _width = 4;
		float _height = 3;
		Transform t;
		Vector3 v3;
		
		//loop to create gems
		for( float y = -_height, rowID = 0; y < _height; y += .6f, rowID++)
		{
			//create a container for row
			GameObject _go = new GameObject("Gem Row: " + rowID.ToString() );
			_go.transform.parent = gemContainer;
			//create list
			gems.Add ( new List<GemBrick>() );
			
			//loop row, it means columns
			for( float x = -_width, colID = 0; x < _width; x += .6f, colID++)
			{
				//positionate 
				v3 = new Vector3( x, y, 0 );
				t = ( Instantiate( gemBrick, v3, Quaternion.identity ) as GameObject ).transform;
				t.parent = _go.transform;
				t.name = "Brick : " +  rowID.ToString() + " " + colID.ToString();
				t.GetComponent<GemBrick>().SetColRow( (int)colID, (int)rowID );
				
				//just a randomize
				if( Random.Range(0,100) > 80 ) t.GetComponent<GemBrick>().ChangeType( BrickList.BRICK_TYPE.NONE );
				else t.GetComponent<GemBrick>().ChangeType( Utils.GetRandomEnum<BrickList.BRICK_TYPE>() );
				//
				gems[(int)rowID].Add( t.GetComponent<GemBrick>() );
				
			}
		} 
	}
	

	public void StartCheckGems( GemBrick gem )
	{
		FloodFill( gem );
	}
	
	
	void FloodFill( GemBrick node )
	{
		List<GemBrick> gemList = new List<GemBrick>();
		if( node.brickType != BrickList.BRICK_TYPE.NONE ) return;
		int x = 0, y = 0;
		
		//RIGHT CHECK
		for( x = node.colID; x < gems[node.rowID].Count; x++ )
		{
			if( BrickList.CheckIsNULL( ref gemList, gems[node.rowID][x] ) )  break;
		}
		
		//LEFT CHECK
		for( x = node.colID; x >= 0; x-- )
		{
			if( BrickList.CheckIsNULL( ref gemList, gems[node.rowID][x] ) )  break;
		}
		
		//TOP CHECK
		for( y = node.rowID; y < gems.Count; y++ )
		{
			if( BrickList.CheckIsNULL( ref gemList, gems[y][node.colID] ) )  break;
		}
		
		//BOTTOM CHECK
		for( y = node.rowID; y >= 0; y-- )
		{
			if( BrickList.CheckIsNULL( ref gemList, gems[y][node.colID] ) )  break;
		}
		
		//FINISH CHECK
		FinishCheckGem(gemList);
	}
	
	void FinishCheckGem(List<GemBrick> gemList)
	{
		if( gemList.Count < 2 ) return;
		
		gemList = gemList.FindAll( o => o.brickType != BrickList.BRICK_TYPE.NONE );
		for( int i = 0; i < gemList.Count; i++ ) CheckGemLoop( gemList, i );
	}
	
	public void CheckGemLoop( List<GemBrick> gemList, int gemID)
	{
		bool match = false;
		for( int i = gemID; i < gemList.Count; i++ )
		{
			//dont check itself
			if( i == gemID ) continue;
			if( gemList[i].brickType == gemList[gemID].brickType )
			{
				match = true;
				gemList[i].ChangeType( BrickList.BRICK_TYPE.NONE );
			}
		}
		
		if( match ) gemList[gemID].ChangeType( BrickList.BRICK_TYPE.NONE );
	} 
	
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour {

	public static GameObject healthbarPrefab;
	public static GameObject unitPrefab;

	public Color32 firstTeamPrimaryColor;
	public Color32 firstTeamPSecondaryColor;

	public Color32 secondTeamPrimaryColor;
	public Color32 secondTeamSecondaryColor;

	public Vector3 beginSpawnRegion;
	public Vector3 endSpawnRegion;

	private float spawnNew = 0;

	private struct GameUnit {
		public Unit unit;
		public HealthBar3D healthBar;

		public GameUnit( Unit u, HealthBar3D h ) {
			this.unit = u;
			this.healthBar = h;
		}

		public GameUnit( Unit u ) {
			this.unit = u;
			this.healthBar = Instantiate<GameObject>( healthbarPrefab ).GetComponent<HealthBar3D>();
			this.healthBar.unit = u;
		}

		public GameUnit( GameObject go ) {
			this.unit = go.GetComponent<Unit>();
			this.healthBar = Instantiate<GameObject>( healthbarPrefab ).GetComponent<HealthBar3D>();
			this.healthBar.unit = this.unit;
		}
	}

	public PlayerController player;
	private Dictionary<byte, IList<GameUnit>> units;

	private void Start( ) {
		GameController.healthbarPrefab = Resources.Load<GameObject>( "Prefabs/HealthBar3D" );
		GameController.unitPrefab = Resources.Load<GameObject>( "Prefabs/Unit" );

		this.units = new Dictionary<byte, IList<GameUnit>> {
			[0] = new List<GameUnit> { new GameUnit( this.player.unit ) },
			[1] = new List<GameUnit>()
		};

		this.units[0][0].healthBar.healthColor = this.firstTeamPrimaryColor;
		this.units[0][0].healthBar.backColor = this.firstTeamPSecondaryColor;
	}

	private void FixedUpdate( ) {
		if ( this.spawnNew <= 0 ) {
			this.spawnNew = 3;

			Vector3 newPosition;
			newPosition.x = Random.Range( this.beginSpawnRegion.x, this.endSpawnRegion.x );
			newPosition.y = Random.Range( this.beginSpawnRegion.y, this.endSpawnRegion.y );
			newPosition.z = Random.Range( this.beginSpawnRegion.z, this.endSpawnRegion.z );

			GameUnit newEnemy = new GameUnit( Instantiate<GameObject>( unitPrefab, newPosition, Quaternion.identity ) );
			newEnemy.unit.maxHealth = 25;
			newEnemy.unit.currHealth = 25;
			newEnemy.unit.teamID = 1;

			newEnemy.healthBar.healthColor = this.secondTeamPrimaryColor;
			newEnemy.healthBar.backColor = this.secondTeamSecondaryColor;

			this.units[1].Add( newEnemy );
			Debug.Log( "New enemy!" );
		} else {
			this.spawnNew -= Time.fixedDeltaTime;
		}

		// clean up dead enemy units
	}

}

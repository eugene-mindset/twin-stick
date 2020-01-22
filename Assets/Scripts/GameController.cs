using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour {

	public static GameObject healthbarPrefab;
	public static GameObject unitPrefab;
	public static Bullet bulletPrefab;

	public Color32 firstTeamPrimaryColor;
	public Color32 firstTeamPSecondaryColor;

	public Color32 secondTeamPrimaryColor;
	public Color32 secondTeamSecondaryColor;

	public Vector3 beginSpawnRegion;
	public Vector3 endSpawnRegion;

	private float spawnNew = 0;

	public Vector3 cameraOffset;

	private struct GameUnit {
		public Unit unit;
		public HealthBar3D healthBar;
		public IUnitController controller;

		public GameUnit( Unit u, HealthBar3D h, IUnitController c ) {
			this.unit = u;
			this.healthBar = h;
			this.controller = c;
		}

		public GameUnit( Unit u ) {
			this.unit = u;
			this.healthBar = Instantiate<GameObject>( healthbarPrefab ).GetComponent<HealthBar3D>();
			this.healthBar.unit = u;
			this.controller = null;
		}

		public GameUnit( GameObject go ) {
			this.unit = go.GetComponent<Unit>();
			this.healthBar = Instantiate<GameObject>( healthbarPrefab ).GetComponent<HealthBar3D>();
			this.healthBar.unit = this.unit;
			this.controller = null;
		}
	}

	public PlayerController player;
	private Dictionary<byte, IList<GameUnit>> units;

	private void Start( ) {
		GameController.healthbarPrefab = Resources.Load<GameObject>( "Prefabs/HealthBar3D" );
		GameController.unitPrefab = Resources.Load<GameObject>( "Prefabs/Unit" );
		GameController.bulletPrefab = Resources.Load<Bullet>( "Prefabs/Bullet" );

		EnemyAI.gameControl = this;

		this.units = new Dictionary<byte, IList<GameUnit>> {
			[0] = new List<GameUnit> { new GameUnit( this.player.unit ) },
			[1] = new List<GameUnit>()
		};

		this.units[0][0].healthBar.healthColor = this.firstTeamPrimaryColor;
		this.units[0][0].healthBar.backColor = this.firstTeamPSecondaryColor;
	}

	private void FixedUpdate( ) {
		if ( this.spawnNew <= 0 ) {
			this.spawnNew = 5;

			Vector3 newPosition;
			newPosition.x = Random.Range( this.beginSpawnRegion.x, this.endSpawnRegion.x );
			newPosition.y = Random.Range( this.beginSpawnRegion.y, this.endSpawnRegion.y );
			newPosition.z = Random.Range( this.beginSpawnRegion.z, this.endSpawnRegion.z );

			GameUnit newEnemy = new GameUnit( Instantiate<GameObject>( unitPrefab, newPosition, Quaternion.identity ) );
			newEnemy.unit.maxHealth = 25;
			newEnemy.unit.currHealth = 25;

			newEnemy.unit.teamID = 1;

			newEnemy.unit.terminalSpeedWhileMoving = 4;
			newEnemy.unit.terminalSpeedWhileAiming = 3;
			newEnemy.unit.rotateSpeedWhileAiming = 0.1f;
			newEnemy.unit.rotateSpeedWhileMoving = 0.05f;

			newEnemy.unit.primarySpeed = 15;
			newEnemy.unit.primaryLifetime = 1;
			newEnemy.unit.primaryCooldown = 1;
			newEnemy.unit.primaryWait = 1;
			newEnemy.unit.primaryDamage = 1;
			newEnemy.unit.primaryBulletPrefab = bulletPrefab;

			newEnemy.healthBar.healthColor = this.secondTeamPrimaryColor;
			newEnemy.healthBar.backColor = this.secondTeamSecondaryColor;

			newEnemy.controller = newEnemy.unit.gameObject.AddComponent<EnemyAI>();
			newEnemy.controller.Unit = newEnemy.unit;

			this.units[1].Add( newEnemy );
			Debug.Log( "New enemy!" );
		} else {
			this.spawnNew -= Time.fixedDeltaTime;
		}

		// clean up dead enemy units
	}

	private void LateUpdate( ) {
		Camera.main.transform.position = this.player.unit.transform.position + this.cameraOffset;
	}

	public Unit GetPlayer( ) {
		return this.player.unit;
	}
}

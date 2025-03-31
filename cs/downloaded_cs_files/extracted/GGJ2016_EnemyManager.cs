  using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class EnemyManager : MonoBehaviour {
    public GameObject tile;

    private const int WIDTH = 17;
    private const int HEIGHT = 17;
    private const float TEMPLE_TILE_SIZE = 2.19f;

    public Tile[,] Tiles;
    private EnemySpawn enemySpawn;
	private Coroutine spawnCoroutine;

    private int beginnerPool = 3;

    private void Start(){
        enemySpawn = FindObjectOfType<EnemySpawn>();

        Tiles = new Tile[WIDTH,HEIGHT];

        for(int y = 0; y < HEIGHT; y++){
            for(int x = 0; x < WIDTH; x++){
                Tiles[x, y] = new Tile(x, y);
            }
        }

        int centerX = (int)Math.Floor(WIDTH/2.0f);
        int centerY = (int)Math.Floor(HEIGHT/2.0f);
        FloodTile(Tiles[centerX, centerY], 0, new List<Tile>());

		//DebugDrawTiles();

		StartCoroutine(LevelTransition(1));
	}

    private void DebugDrawTiles(){
        foreach(Tile tile in Flatten(Tiles)){
            GameObject go = Instantiate(this.tile);
            go.GetComponentInChildren<TextMesh>().text = tile.X + "," + tile.Y + ": " + tile.Score;
            go.transform.position = GetWorldLocationOfTile(tile.X, tile.Y);
        }
    }

	public void MoveEnemyToNewPosition(Enemy enemy){
        Tile tileOfEnemy = Flatten(Tiles).FirstOrDefault(t => t.enemy == enemy);

        if(tileOfEnemy == null){
            return;
        }

        Tile newTile = GetNewTilePosition(tileOfEnemy);

		if (newTile != null){
			//Calculate new direction of enemy
			enemy._direction = (GetWorldLocationOfTile(newTile.X, newTile.Y) - enemy.transform.position).normalized;
			enemy._direction.y = 0;
			enemy._lookRotation = Quaternion.LookRotation(enemy._direction);

			tileOfEnemy.enemy = null;
            newTile.enemy = enemy;
            enemy.Move(GetWorldLocationOfTile(newTile.X, newTile.Y));

            if(newTile.X == WIDTH /2 && newTile.Y == HEIGHT / 2){
                PlayerManager.instance.LowerExperience();
                newTile.enemy = null;
            }
        }
    }

    private Tile GetNewTilePosition(Tile currentTile){
        // find neighbours
        List<Tile> neighbours = GetNeighborsOfTile(currentTile);

        Tile newTile = null;
        neighbours.Sort((x, y) => x.Score - y.Score);
        foreach(Tile neighbour in neighbours){
            if(neighbour.HasEnemy() == false && currentTile.Score > neighbour.Score){
                newTile = neighbour;
            }
        }

        return newTile;
    }

    public IEnumerator SpawnEnemies(){
		PlayerManager playerManager = FindObjectOfType<PlayerManager>();
		while (true){

            int amount = UnityEngine.Random.Range(1, 1);

            for(int i = 0; i < amount; i++){
                Enemy enemy = EnemySpawn.instance.CreateEnemy();

                bool hasEnemy = true;
                while(hasEnemy){
                    Vector2 tile = RandomStartTile();
                    if(Tiles[(int)tile.x, (int)tile.y].HasEnemy() == false){
                        Tiles[(int)tile.x, (int)tile.y].enemy = enemy;
                        Vector3 position = GetWorldLocationOfTile((int)tile.x, (int)tile.y);
                        enemy.Spawn(position);
                        hasEnemy = false;
                    }
                }
            }

            int noobPool = Math.Max(beginnerPool--, 0);
            yield return new WaitForSeconds(Math.Max(0.6f, 5.8f - (playerManager.CurrentLevel / 1.2f) + noobPool));
        }

    }

    public Vector3 GetWorldLocationOfTile(int x, int y){
        int xMod = Math.Abs(x - WIDTH / 2);
        int yMod = Math.Abs(y - HEIGHT / 2);
        int biggestAxis = xMod > yMod ? xMod : yMod;
        int level = WIDTH / 2 - biggestAxis;
        float height = level * 1.5f;
        return new Vector3(x * TEMPLE_TILE_SIZE - (TEMPLE_TILE_SIZE * (WIDTH/2)), height, y * TEMPLE_TILE_SIZE - (TEMPLE_TILE_SIZE * (HEIGHT/2)));
    }

    private Vector2 RandomStartTile(){
        int x = 0;
        int y = 0;

        int min = WIDTH / 2 + PlayerManager.instance.CurrentLevel;
        switch((int)(UnityEngine.Random.value * (beginnerPool > 0 ? 2 : 4))){
            case 0:
                x = OuterRim;
                y = UnityEngine.Random.Range(min, OuterRim);
                break;
            case 1:
                x = UnityEngine.Random.Range(min, OuterRim);
                y = OuterRim;
                break;
            case 2:
                x = min;
                y = UnityEngine.Random.Range(min, OuterRim);
                break;
            case 3:
                x = UnityEngine.Random.Range(min, OuterRim);
                y = min;
                break;
        }

        return new Vector2(x, y);

    }

    private int OuterRim {
        get {
            return WIDTH / 2 - PlayerManager.instance.CurrentLevel;
        }
    }

    public IEnumerable<T> Flatten<T>(T[,] map) {
        for (int row = 0; row < map.GetLength(0); row++) {
            for (int col = 0; col < map.GetLength(1); col++) {
                yield return map[row,col];
            }
        }
    }

    private void FloodTile(Tile tile, int score, List<Tile> doneTiles){
        List<Tile> neighbours = GetNeighborsOfTile(tile);

        if(tile.Score > score && (tile.X != (WIDTH / 2) || tile.Y <= (HEIGHT / 2))){
	        tile.Score = score;
        } else {
            return;
        }

		if (tile.X != (WIDTH / 2) || tile.Y <= (HEIGHT / 2)) {
			tile.Score = score;
		}
        doneTiles.Add(tile);

        foreach(Tile neighbourTile in neighbours){
                
            FloodTile(neighbourTile, score+1, doneTiles);
        }
    }

    public List<Tile> GetNeighborsOfTile(Tile tile) {
        List<Tile> neighbours = new List<Tile>();

        if(tile.X > 0) // left
            neighbours.Add(Tiles[tile.X - 1, tile.Y]);

        if(tile.X < WIDTH - 1) // right
            neighbours.Add(Tiles[tile.X + 1, tile.Y]);

        if(tile.Y > 0) // down
            neighbours.Add(Tiles[tile.X, tile.Y - 1]);

        if(tile.Y < HEIGHT - 1) // up
            neighbours.Add(Tiles[tile.X, tile.Y + 1]);

        return neighbours;
    }

	public void PauseSpawning() {
		StopCoroutine(spawnCoroutine);
	}
	public void ContinueSpawning() {
		StartCoroutine(LevelTransition(3));
	}

    public void WhipeEnemies(){
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = enemies.Length - 1; i >= 0 ; i--) {
            enemies[i].Kill(false, false);
        }
	}

    public Tile GetTileOfEnemy(Enemy enemy){
        return Flatten(Tiles).FirstOrDefault(t => t.enemy == enemy);
    }

	public IEnumerator LevelTransition(int time) {
		yield return new WaitForSeconds(time);
		spawnCoroutine = StartCoroutine(SpawnEnemies());
	}
}

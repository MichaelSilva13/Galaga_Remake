using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float spawnRate;
    public Dictionary<String, int> clonesPresent = new Dictionary<string, int>();

    [SerializeField] private PathCreator[] _paths;

    [SerializeField] private int SpawnAmmount;
    [SerializeField] private int _maxEnemy = 6;
    [SerializeField] private int levelOption = 0;
    private GridMvt _gridMvt;
    public int maxEnemyIndex = 1;
    public bool gameON;

    public int _enemyAmmount;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemyPrefab in _enemyPrefabs)
        {
            GameObjectPoolController.AddEntry(enemyPrefab.name, enemyPrefab, 3, 100);
            clonesPresent.Add(enemyPrefab.name, 0);
        }
        GameObjectPoolController.AddEntry("explosion", _explosion, 10, 100);
        _paths = GameObject.FindObjectsOfType<PathCreator>();
        _gridMvt = FindObjectOfType<GridMvt>();
    }

    public void StartGame()
    {
        foreach (GameObject enemyPrefab in _enemyPrefabs)
        {
            GameObjectPoolController.AddEntry(enemyPrefab.name, enemyPrefab, 3, 100);
            
        }
        GameObjectPoolController.AddEntry("explosion", _explosion, 10, 100);
        _paths = GameObject.FindObjectsOfType<PathCreator>();
        _enemyAmmount = 0;
        maxEnemyIndex = 1;
        _maxEnemy = 6;
        levelOption = 0;
        spawnRate = 4.5f;
        gameON = true;
        StartCoroutine(spawnStartTest());
    }

    public void LevelUp()
    {
        switch (levelOption)
        {
            case 0:
                SpawnAmmount++;
                _maxEnemy += 3;
                break;
            case 1:
                if(spawnRate - 1 > 1)
                    spawnRate-=.5f;
                break;
            default:
                if (maxEnemyIndex < _enemyPrefabs.Length)
                {
                    maxEnemyIndex++;
                }
                else
                {
                    SpawnAmmount++;
                    _maxEnemy += 3;
                }
                break;
        }

        levelOption = (levelOption + 1) % 3;
    }

    IEnumerator spawnStartTest()
    {
        while (gameON)
        {
            if (_enemyAmmount < _maxEnemy)
            {
                int index = Random.Range(0, _paths.Length);
                int currentAmmount = _enemyAmmount;
                for (int i = currentAmmount; i < currentAmmount + SpawnAmmount && _enemyAmmount < _maxEnemy; i++)
                {
                    int enemyI = Random.Range(0, maxEnemyIndex);
                    EnemyMvt prefabSelected = _enemyPrefabs[enemyI].GetComponent<EnemyMvt>();
                    while (clonesPresent[prefabSelected.name] > prefabSelected.maxClones)
                    {
                        enemyI = Random.Range(0, maxEnemyIndex);
                        prefabSelected = _enemyPrefabs[enemyI].GetComponent<EnemyMvt>();
                    }
                    GameObject enemy = GameObjectPoolController.Dequeue(_enemyPrefabs[enemyI].name).gameObject;
                    enemy.GetComponent<SpriteRenderer>().enabled = true;
                    clonesPresent[prefabSelected.name]++;
                    EnemyMvt mvt = enemy.GetComponent<EnemyMvt>();
                    mvt.enabled = true;

                    enemy.SetActive(true);
                    enemy.GetComponent<Collider2D>().enabled = true;
                    mvt.MainPath = _paths[index];
                    enemy.transform.position = mvt.MainPath.path.GetPointAtDistance(0);
                    mvt.EnableValues();
                    _enemyAmmount++;
                    yield return new WaitForSeconds(0.2f);
                }
            }

            
            yield return new WaitForSeconds(spawnRate);
        }
    }
}

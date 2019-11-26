using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrid : MonoBehaviour
{
    private Dictionary<int, List<Transform>> _gridPositions = new Dictionary<int, List<Transform>>();

    private Dictionary<Transform, bool> _availableSpot = new Dictionary<Transform, bool>();

    [SerializeField] private GameObject _enemyMissile;
    // Start is called before the first frame update
    void Awake()
    {
        GameObjectPoolController.AddEntry("EnemyProjectile", _enemyMissile, 10, 100);
        for (int i = 0; i < transform.childCount; i++)
        {
            _gridPositions.Add(i, new List<Transform>());
            Transform child = transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                _gridPositions[i].Add(child.GetChild(j));
                _availableSpot.Add(child.GetChild(j), false);
            }
        }
    }

    public void FreeSpace(Transform pos)
    {
        _availableSpot[pos] = false;
    }

    public Transform GetAvailableSpot(int minRow, int maxRow)
    {
        bool available = true;
        Transform pos = transform;
        if (maxRow > transform.childCount)
            maxRow = transform.childCount - 1;
        while (available)
        {
            int index = Random.Range(minRow, maxRow);
            int transformIndex = Random.Range(0, _gridPositions[index].Count);
            pos = _gridPositions[index][transformIndex];
            available = _availableSpot[pos];
        }

        _availableSpot[pos] = true;
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

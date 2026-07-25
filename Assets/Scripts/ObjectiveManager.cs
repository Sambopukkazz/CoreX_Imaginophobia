using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject autophobiaPrefab;
    [SerializeField] private GameObject scopophobiaPrefab;
    [SerializeField] private List<GameObject> spawnPos;

    static int stage;
    static int repairedObjectCount;
    static int item;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAutophobia(Vector2 pos) {
        Instantiate(autophobiaPrefab, pos, Quaternion.identity);
    }

    public void SpawnScopophobia(Vector2 pos) {
        Instantiate(autophobiaPrefab, pos, Quaternion.identity);
    }
}

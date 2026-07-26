using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject autophobiaPrefab;
    [SerializeField] private GameObject scopophobiaPrefab;
    [SerializeField] private List<GameObject> spawnPos;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject collision;

    private static int[] repairedObj = new int[16];
    private int lastRepairedCount;

    public static int stage;
    public static int repairedObjectCount;
    public static int itemCount;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (repairedObjectCount > lastRepairedCount) {
            if(playerCollision.collidedObj.name == "Elec Hitbox")
            lastRepairedCount = repairedObjectCount;
        }
        
    }

    public void SpawnAutophobia(Vector2 pos) {
        Instantiate(autophobiaPrefab, pos, Quaternion.identity);
    }

    public void SpawnScopophobia(Vector2 pos) {
        Instantiate(autophobiaPrefab, pos, Quaternion.identity);
    }
}

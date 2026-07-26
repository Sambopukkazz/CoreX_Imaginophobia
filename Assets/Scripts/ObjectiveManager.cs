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

    private int lastRepairedCount;
    private bool spanwed;

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
            
            lastRepairedCount = repairedObjectCount;
        }
        if(repairedObjectCount == 3 && SceneManager.GetActiveScene().name == "Sewer" && !spanwed) {
            SpawnAutophobia(new Vector2(-8f, -0.5f));
            spanwed = true;
        }
        else if (repairedObjectCount == 4 && SceneManager.GetActiveScene().name == "ER2" && !spanwed) {
            SpawnScopophobia(new Vector2(-40f, -0.5f));
            spanwed = true;
        }
        else if (repairedObjectCount == 8 && SceneManager.GetActiveScene().name == "Storage" && !spanwed) {
            SpawnAutophobia(new Vector2(-16f, -0.5f));
            spanwed = true;
        }
        else if (repairedObjectCount == 8 && SceneManager.GetActiveScene().name == "ER3" && !spanwed && ObjectiveManager.itemCount == 2) {
            SpawnScopophobia(new Vector2(-40f, -0.5f));
            spanwed = true;
        }
        else if (repairedObjectCount == 9 && SceneManager.GetActiveScene().name == "Sewer" && !spanwed && ObjectiveManager.itemCount == 2) {
            SpawnScopophobia(new Vector2(-50f, -0.5f));
            spanwed = true;
        }
    }

    public void SpawnAutophobia(Vector2 pos) {
        Instantiate(autophobiaPrefab, pos, Quaternion.identity);
    }

    public void SpawnScopophobia(Vector2 pos) {
        Instantiate(scopophobiaPrefab, pos, Quaternion.identity);
    }
}

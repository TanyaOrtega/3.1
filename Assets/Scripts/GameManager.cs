using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gruntPrefab;

    private JohnMovement john;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        john = FindFirstObjectByType<JohnMovement>();
        RespawnGrunt();
    }

    void Update()
    {
        if (john == null)
            john = FindFirstObjectByType<JohnMovement>();

        if (ScoreManager.score >= 150)
        {
            LoadNextLevel();
        }
    }

    public void RespawnGrunt()
    {
        if (john == null)
            john = FindFirstObjectByType<JohnMovement>();

        if (gruntPrefab == null)
        {
            Debug.LogError("⚠️ No hay prefab de Grunt asignado en el GameManager!");
            return;
        }

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("⚠️ No se encontró la cámara principal!");
            return;
        }

        Vector3 camMin = cam.ViewportToWorldPoint(new Vector3(0.1f, 0.25f, 0));
        Vector3 camMax = cam.ViewportToWorldPoint(new Vector3(0.9f, 0.75f, 0));

        Vector3 spawnPosition = Vector3.zero;

        if (john != null)
        {
            float offsetX = Random.Range(2f, 3f);
            if (Random.value > 0.5f) offsetX *= -1;

            spawnPosition = john.transform.position + new Vector3(offsetX, 0, 0);

            spawnPosition.x = Mathf.Clamp(spawnPosition.x, camMin.x, camMax.x);
            spawnPosition.y = Mathf.Clamp(john.transform.position.y, camMin.y, camMax.y);
            spawnPosition.z = -1f;
        }

        GameObject newGrunt = Instantiate(gruntPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"✅ Nuevo Grunt generado en: {spawnPosition}");

        GruntScript gruntScript = newGrunt.GetComponent<GruntScript>();
        if (gruntScript != null)
            gruntScript.John = john;
    }


    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            ScoreManager.ResetScore();
        }
        else
        {
            Debug.Log("🎉 ¡Felicidades! No hay más niveles.");
        }
    }
}

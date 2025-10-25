using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class FirestoreManager : MonoBehaviour
{
    FirebaseFirestore db;

    void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    public void SaveScore(string playerName, int score)
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "player", playerName },
            { "score", score },
            { "level", SceneManager.GetActiveScene().buildIndex },
            { "timestamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
        };

        db.Collection("scores").AddAsync(data).ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                Debug.Log("✅ Puntaje guardado: " + score);
            }
            else
            {
                Debug.LogError("❌ Error guardando puntaje: " + task.Exception);
            }
        });
    }
}

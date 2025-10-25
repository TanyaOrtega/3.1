using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseInitializer : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("✅ Firebase listo!");
            }
            else
            {
                Debug.LogError("❌ No se pudieron resolver las dependencias de Firebase: " + task.Result);
            }
        });
    }
}



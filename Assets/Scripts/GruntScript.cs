using System.Collections;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public JohnMovement John;

    private float LastShoot;
    private int Health = 3;
    private bool isDead = false;

    private void Update()
    {
        if (isDead || John == null) return;

        Vector3 direction = John.transform.position - transform.position;

  
        transform.localScale = new Vector3(direction.x >= 0 ? 1 : -1, 1, 1);

    
        if (Mathf.Abs(direction.x) < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        if (BulletPrefab == null) return;

        Vector3 direction = transform.localScale.x == 1 ? Vector3.right : Vector3.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        if (isDead) return; 
        Health--;

        if (Health <= 0)
        {
            isDead = true;
            ScoreManager.AddScore(10);

            FirestoreManager firestore = FindFirstObjectByType<FirestoreManager>();
            if (firestore != null)
            {
                firestore.SaveScore("John", ScoreManager.score);
            }

            Debug.Log("💀 Grunt eliminado. Respawn en 1.5s...");
            StartCoroutine(RespawnAndDestroy());
        }
    }

    private IEnumerator RespawnAndDestroy()
    {
        yield return new WaitForSeconds(1.5f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RespawnGrunt();
        }
        else
        {
            Debug.LogWarning("⚠️ GameManager no encontrado.");
        }

        Destroy(gameObject); 
    }
}

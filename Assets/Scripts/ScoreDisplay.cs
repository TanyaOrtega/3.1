using UnityEngine;
using TMPro;  

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (scoreText != null)
            scoreText.text = "Puntaje: " + ScoreManager.score.ToString();
    }

}

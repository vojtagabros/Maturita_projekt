using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayInfo : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject FailCanvas;
    public GameObject SuccesCanvas;

    [Header("Success screen texts")]
    public TextMeshProUGUI successStatsText;
    public TextMeshProUGUI successScoreText;
    public TextMeshProUGUI successGradeText;

    [Header("Fail screen texts")]
    public TextMeshProUGUI failStatsText;
    public TextMeshProUGUI failScoreText;
    public TextMeshProUGUI failGradeText;

    void Start()
    {
        FailCanvas.SetActive(false);
        SuccesCanvas.SetActive(false);

        int score = CalculateScore();
        string grade = GetGrade(score);

        if (GameData.Escaped)
        {
            SuccesCanvas.SetActive(true);
            successStatsText.text = BuildSuccessStats();
            successScoreText.text = score + " / 1000";
            successGradeText.text = grade;
        }
        else
        {
            FailCanvas.SetActive(true);
            failStatsText.text = BuildFailStats();
            failScoreText.text = score + " / 1000";
            failGradeText.text = grade;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // -------------------------------------------------------------------------

    private string BuildSuccessStats()
    {
        string time = GameData.SurvivalTime.ToString("F1") + "s";
        string detected = GameData.PlayerSeen ? "Ano  (-100 bodů)" : "Ne  (+100 bodů)";
        string help = GameData.HelpCalled == 0
            ? "Ne"
            : "Ano, v " + GameData.HelpCalled.ToString("F1") + "s  (+75 bodů)";
        string timeBonus = Mathf.Clamp(300 - Mathf.FloorToInt(GameData.SurvivalTime) * 2, 0, 300).ToString();

        string result = "";
        if (GameData.FoughtAttacker && GameData.FightWon)
            result = "\nSouboj s útočníkem: Vyhráli jste! ✓  (-200 bodů)";

        return
            "Čas úniku:       " + time + "  (bonus " + timeBonus + " bodů)\n" +
            "Detekován:       " + detected + "\n" +
            "Volal pomoc:     " + help +
            result;
    }

    private string BuildFailStats()
    {
        string time = GameData.SurvivalTime.ToString("F1") + "s";
        int survivalBonus = Mathf.Clamp(Mathf.FloorToInt(GameData.SurvivalTime) * 2, 0, 200);

        string result = "";
        if (GameData.FoughtAttacker && !GameData.FightWon)
            result = "\nSouboj s útočníkem: Prohráli jste.";

        return
            "Přežil:          " + time + "  (bonus " + survivalBonus + " bodů)\n" +
            result;
    }

    private int CalculateScore()
    {
        if (GameData.Escaped)
        {
            int score = GameData.FoughtAttacker ? 300 : 500;
            score += Mathf.Clamp(300 - Mathf.FloorToInt(GameData.SurvivalTime) * 2, 0, 300);
            if (!GameData.PlayerSeen) score += 100;
            if (GameData.HelpCalled != 0) score += 75;
            return score;
        }
        else
        {
            int score = 0;
            score += Mathf.Clamp(Mathf.FloorToInt(GameData.SurvivalTime) * 2, 0, 200);
            return score;
        }
    }

    private string GetGrade(int score)
    {
        if (score >= 850) return "A";
        if (score >= 700) return "B";
        if (score >= 500) return "C";
        if (score >= 300) return "D";
        return "F";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] public List<PlayerInfo> scores = new List<PlayerInfo>();
    public Text HighScore;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetString("leaderboard").Equals(null))
        {
            for (int i = 1; i <= 5; i++)
            {
                AddScore(new PlayerInfo("AAA", i * 100));
            }
            HighScores highScores = new HighScores {publicscores = scores};
            string table = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("leaderboard", table);
        }
        else
        {
            HighScores highScores = JsonUtility.FromJson<HighScores>(PlayerPrefs.GetString("leaderboard"));
            scores = highScores.publicscores;
            HighScore.text = $"{scores[0].score:000,000}";
        }

        
    }

    public void AddScore(PlayerInfo newPlayer)
    {
        scores.Add(newPlayer);
        scores.Sort((x, y) => -x.score.CompareTo(y.score));
        HighScore.text = $"{scores[0].score:000,000}";
        HighScores highScores = new HighScores {publicscores = scores};
        string table = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("leaderboard", table);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Serializable] private class HighScores
    {
        public List<PlayerInfo> publicscores;

    }
}

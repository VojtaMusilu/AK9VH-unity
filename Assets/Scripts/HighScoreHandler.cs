using System.Collections.Generic;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    List<HighScoreElement> highscoreList = new List<HighScoreElement>();
    [SerializeField] int maxCount = 10;
    [SerializeField] private string filename;

	public delegate void OnHighScoreListChanged(List<HighScoreElement> list);
	public static event OnHighScoreListChanged onHighScoreListChanged;

    private void Start()
    {
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighScoreElement>(filename);
        while (highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }

		if(onHighScoreListChanged != null){onHighScoreListChanged.Invoke(highscoreList);}
    }

    private void SaveHighScores()
    {
        FileHandler.SaveToJSON<HighScoreElement>(highscoreList, filename);
    }

    public void AddHighScore(HighScoreElement element)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= highscoreList.Count || element.score > highscoreList[i].score)
            {
                highscoreList.Insert(i, element);
                while (highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(maxCount);
                }
                SaveHighScores();
				if(onHighScoreListChanged != null){onHighScoreListChanged.Invoke(highscoreList);}
                break;
            }
        }
    }
}
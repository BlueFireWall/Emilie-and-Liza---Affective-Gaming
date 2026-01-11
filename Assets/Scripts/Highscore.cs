using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public int level, rows, points;
    public Text highscoreText, levelText, rowText;

    void Start()
    {
        level = 0;
        rows = 0;
        points = 0;

        GetComponent<Movement>().SetNewSpeed();
        TransmitToUI();
    }

    public void AddPointsForLines(int lines)
    {
        if (lines > 0)
        {
            switch (lines)
            {
                case 1: points += 40 * (level + 1); break;
                case 2: points += 100 * (level + 1); break;
                case 3: points += 300 * (level + 1); break;
                case 4: points += 1200 * (level + 1); break;
            }

            rows += lines;
            level = rows / 10;

            GetComponent<Movement>().SetNewSpeed();
            TransmitToUI();
        }
    }

    public void AddPointsForCubes()
    {
        points += 4;
        TransmitToUI();
    }

    public void TransmitToUI()
    {
        highscoreText.text = points.ToString();
        levelText.text = level.ToString();
        rowText.text = rows.ToString();
    }
}

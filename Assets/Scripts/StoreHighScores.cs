using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class StoreHighScores : MonoBehaviour {

  private const string urlBase = "http://dreamlo.com/lb/";

  private List<PlayerInfo> highScores = new List<PlayerInfo>();

  [SerializeField]
  private InputField nameEntry;
  [SerializeField]
  private Text topScoresText;

  public void HighScoreEntry() {
    StoreHighScore(nameEntry.text, 123, "normal");
    GetHighScores();
  }

  private void StoreHighScore(string name, int score, string text) {
    string url = urlBase + HighScoresAPIKey.privateKey +  "/add/" + name + "/" + score + "/0/" + text;
    GetResponse(url);
  }

  private void GetHighScores() {
    string url = urlBase + HighScoresAPIKey.privateKey +  "/json/10/";
    GetResponse(url, true);
  }

  private void GetResponse(string url, bool json = false) {
    WebRequest wrGETURL;
    wrGETURL = WebRequest.Create(url);

    Stream objStream;
    objStream = wrGETURL.GetResponse().GetResponseStream();
    StreamReader objReader = new StreamReader(objStream);


    string sLine = "";
    int i = 0;

    while (sLine != null) {
      i++;
      sLine = objReader.ReadLine();
      if (sLine != null) {
        if (json) {
          sLine = sLine.Substring(36);
          sLine = sLine.Substring(0, sLine.Length - 4);
          string[] splitLine = Regex.Split(sLine, @"(?=,{)");
          foreach (string line in splitLine) {
            string _line = line;
            if (_line[0] == ',') {
              _line = _line.Substring(1);
            }
            var jsonObj = JsonUtility.FromJson<PlayerInfo>(_line);
            highScores.Add(jsonObj);
          }
        }
        Debug.Log(i + " : " + sLine);
      }
    }

    if (json) {
      DisplayTopScores();
    }
  }

  private void DisplayTopScores() {
    topScoresText.text = "";
    foreach (PlayerInfo info in highScores) {
      topScoresText.text += info.name;
      topScoresText.text += info.score;
      topScoresText.text += info.text;
      topScoresText.text += "\n";
      Debug.Log(info.name);
      Debug.Log(info.score);
    }
  }

}

[Serializable]
public class PlayerInfo {
  public string name;
  public int score;
  public string text;
}

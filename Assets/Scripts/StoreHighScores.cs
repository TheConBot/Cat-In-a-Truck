using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class StoreHighScores : MonoBehaviour {

  private const string urlBase = "http://dreamlo.com/lb/";

  private List<PlayerInfo> highScores = new List<PlayerInfo>();

  private void Start() {
    StoreHighScore("nathan", 420, "normal");

    GetHighScores();
  }

  private void StoreHighScore(string name, int score, string text) {
    string url = urlBase + HighScoresAPIKey.privateKey +  "/add/" + name + "/" + score + "/0/" + text;
    GetResponse(url);
  }

  private void GetHighScores() {
    string url = urlBase + HighScoresAPIKey.privateKey +  "/json/10/";
    GetResponse(url);
  }

  private void GetResponse(string url) {
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
        Debug.Log(i + " : " + sLine);
        highScores.Add(JsonUtility.FromJson<PlayerInfo>(sLine));
      }
    }
  }

}

public class PlayerInfo {
  public string name;
  public int score;
  public string text;

  public PlayerInfo(string _name, int _score, string _text) {
    name = _name;
    score = _score;
    text = _text;
  }
}

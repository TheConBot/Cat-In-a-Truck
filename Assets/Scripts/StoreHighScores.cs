using System;
using System.Net;
using System.IO;
using UnityEngine;

public class StoreHighScores : MonoBehaviour {

  private const string urlBase = "http://dreamlo.com/lb/";

  private void Start() {
    StoreHighScore("nathan", 420);
  }

  private void StoreHighScore(string name, int score) {
    string url = urlBase + HighScoresAPIKey.privateKey +  "/add/" + name + "/" + score;
    WebRequest wrGETURL;
    wrGETURL = WebRequest.Create(url);
  }

}

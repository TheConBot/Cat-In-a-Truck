using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class HighScoresManager : MonoBehaviour {

    public const string API_URL_BASE = "http://dreamlo.com/lb/";

    public List<PlayerInfo> GetHighScores() {
        var highScores = new List<PlayerInfo>();
        //Only get the top 10
        string url = API_URL_BASE + HighScoresAPIKey.privateKey + "/json/10/";

        Stream objStream = WebRequest.Create(url).GetResponse().GetResponseStream();
        StreamReader objReader = new StreamReader(objStream);

        string sLine = "";
        int i = 0;

        while (sLine != null) {
            i++;
            sLine = objReader.ReadLine();
            if (sLine != null) {
                if (highScores != null) {
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
            }
        }
        return highScores;
    }

    public void PostHighScore(string url) {
        WebRequest.Create(url).GetResponse();
    }
}

[Serializable]
public class PlayerInfo {
    public string name;
    public int score;
    public string text;
}

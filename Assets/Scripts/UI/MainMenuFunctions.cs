using UnityEngine;

public class MainMenuFunctions : BaseMenuFuctions {

    override public void LoadSceneFromButton(int sceneIndex) {
        Manager.Instance.SetNewRecipies();
        base.LoadSceneFromButton(sceneIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

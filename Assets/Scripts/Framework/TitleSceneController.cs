using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneController : SingletonBehaviour<TitleSceneController> {

    [SerializeField]
    private Image title;
    [SerializeField]
    private RectTransform[] UserInterfaces;

    void Start()
    {
        UserInterfaces = FindObjectOfType<Canvas>().GetComponentsInChildren<RectTransform>();
        float x = Screen.width / title.rectTransform.sizeDelta.x;
        float y = Screen.height / title.rectTransform.sizeDelta.y;
        for (int i = 0; i < UserInterfaces.Length; ++i)
        {
            UserInterfaces[i].sizeDelta = new Vector2(UserInterfaces[i].sizeDelta.x * x, UserInterfaces[i].sizeDelta.y * y);
        }
    }

    public void onButtonClicked(Button button)
    {
        switch (button.gameObject.name)
        {
            case "StartGameButton":
                SceneManager.LoadScene(1);
                break;
        }
    }
}

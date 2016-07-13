using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUIManager : SingletonBehaviour<InGameUIManager> {

    public Canvas InGameCanvas = null;
    public Canvas MenuCanvas = null;
    [SerializeField]
    private Text[] combinationUI = new Text[6];
    [SerializeField]
    private Text costText, progressText;
    [SerializeField]
    private GameObject HPbarPrefab;
    [SerializeField]
    private GameObject[] HPbars;

    void Awake()
    {
        Button[] buttons = InGameCanvas.GetComponentsInChildren<Button>();
        if (GameStateManager.Inst().getState() == State.INGAME)
        {
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].interactable = true;
            }
            MenuCanvas.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].interactable = false;
            }
            MenuCanvas.gameObject.SetActive(true);
        }
    }

    public void OnStateChanged(State state)
    {
        Button[] buttons = InGameCanvas.GetComponentsInChildren<Button>();
        if (state == State.INGAME)
        {
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].interactable = true;
            }
            MenuCanvas.gameObject.SetActive(false);
        }
        else
        {
            for (int i=0; i<buttons.Length; ++i)
            {
                buttons[i].interactable = false;
            }
            MenuCanvas.gameObject.SetActive(true);
        }
    }

    public void combinationTextUpdate()
    {
        string str = InGameSystemManager.Inst().getCombination();
        for (int i = 0; i < str.Length; ++i)
        {
            combinationUI[i].text = str[i].ToString();
        }
        for (int i = str.Length; i<6; ++i)
        {
            combinationUI[i].text = "";
        }
    }

    public void costTextUpdate()
    {
        costText.text = InGameSystemManager.Inst().getCost().ToString() + " / " + InGameSystemManager.Inst().getMaxCost().ToString();
    }

    public void progressTextUpdate()
    {
        progressText.text = "다음 마을까지 " + InGameSystemManager.Inst().getProgress().ToString() + "/" + InGameSystemManager.Inst().getDistance().ToString() + " M";
    }

    public void HPbarUpdate()
    {
        for(int i=0; i<HPbars.Length; ++i)
        {
            Destroy(HPbars[i]);
        }
        ObjectUnit[] units = GameObject.FindObjectsOfType<ObjectUnit>();
        HPbars = new GameObject[units.Length];
        for (int i = 0; i < units.Length; ++i)
        {
            HPbars[i] = Instantiate(HPbarPrefab);
            HPbars[i].transform.parent = InGameCanvas.transform;
            Vector2 CanPos = Camera.main.WorldToScreenPoint(units[i].transform.position);
            HPbars[i].GetComponent<Image>().rectTransform.position = CanPos + new Vector2(-40, units[i].GetComponent<SpriteRenderer>().bounds.size.y * 20);
            HPbars[i].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(80 * units[i].getHP() / units[i].getMaxHP(), 8);
            Debug.Log(units[i].GetComponent<SpriteRenderer>().bounds.size.y);
        }
    }
}

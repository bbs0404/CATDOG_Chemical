using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUIManager : SingletonBehaviour<InGameUIManager> {

    public Canvas InGameCanvas = null;
    public Canvas MenuCanvas = null;
    [SerializeField]
    private Image[] combinationUI = new Image[6];
    [SerializeField]
    private Sprite[] combinationSprite = new Sprite[5];
    [SerializeField]
    private Text costText, progressText;
    [SerializeField]
    private GameObject progressIndicator;
    [SerializeField]
    private Vector2 progressIndicatorRange;
    [SerializeField]
    private GameObject backgroundImage;
    [SerializeField]
    private float backgroundImageWidth = 5120f;
    [SerializeField]
    private GameObject HPbarPrefab;
    [SerializeField]
    private float HPbarWidth = 130f;
    [SerializeField]
    private ObjectHPBar[] HPbars;
    [SerializeField]
    private GameObject entity;
    [SerializeField]
    private Vector2[] mobPosition = new Vector2[4];

    [SerializeField]
    private int progress, distance;

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
            combinationUI[i].enabled = true;
            switch(str[i]) {
                case 'C': combinationUI[i].sprite = combinationSprite[0]; break;
                case 'O': combinationUI[i].sprite = combinationSprite[1]; break;
                case 'H': combinationUI[i].sprite = combinationSprite[2]; break;
                case 'S': combinationUI[i].sprite = combinationSprite[3]; break;
                case 'P': combinationUI[i].sprite = combinationSprite[4]; break;
            }
        }
        for (int i = str.Length; i<6; ++i)
        {
            combinationUI[i].enabled = false;
        }
    }

    public void costTextUpdate()
    {
        costText.text = InGameSystemManager.Inst().getCost().ToString() + " / " + InGameSystemManager.Inst().getMaxCost().ToString();
    }

    public void progressUpdate(int progress, float delay = 0)
    {
        StartCoroutine(move(progress, delay));
    }

    public void HPbarUpdate()
    {
        HPbars = FindObjectsOfType<ObjectHPBar>();
        foreach (var i in HPbars)
        {
            var p = i.transform.parent.gameObject.GetComponent<ObjectUnit>();
            i.SetValue(p.getHP(), p.getMaxHP());
        }
    }

    private IEnumerator move(int progress_, float delay) 
    {
        int post_progress = progress;
        progress = progress_;
        distance = InGameSystemManager.Inst().getDistance();
        Vector3 p;

        for ( float t = 0; t < delay; t += GameTime.deltaTime ) {
            float current = (post_progress * (delay - t) + progress * t) / delay;
            progressText.text = string.Format("다음 마을까지 {0,2}/{1,2} M",
                Mathf.RoundToInt(current * 10), distance * 10);
            p = progressIndicator.transform.localPosition;
            p.x = progressIndicatorRange.x + (current / distance) * 
                (progressIndicatorRange.y - progressIndicatorRange.x);
            progressIndicator.transform.localPosition = p;
            backgroundImage.transform.localPosition -= new Vector3(GameTime.deltaTime * 400f, 0, 0);
            if ( backgroundImage.transform.localPosition.x <= -backgroundImageWidth )
                backgroundImage.transform.localPosition += new Vector3(backgroundImageWidth, 0, 0);
            yield return null;
        }

        progress = InGameSystemManager.Inst().getProgress();
        distance = InGameSystemManager.Inst().getDistance();

        progressText.text = string.Format("다음 마을까지 {0,2}/{1,2} M", progress * 10, distance * 10);
        p = progressIndicator.transform.localPosition;
        p.x = progressIndicatorRange.x + ((float)progress / distance) *
            (progressIndicatorRange.y - progressIndicatorRange.x);
        progressIndicator.transform.localPosition = p;
    }

    public GameObject GenerateMob(GameObject prefab, int number) {
        GameObject mob = Instantiate(prefab);
        mob.transform.SetParent(entity.transform, false);
        mob.transform.localPosition = mobPosition[number];

        GameObject hpbar = Instantiate(HPbarPrefab);
        hpbar.transform.SetParent(mob.transform, false);
        hpbar.transform.localPosition = new Vector2(0, mob.GetComponent<RectTransform>().rect.height);

        return mob;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HPbar: MonoBehaviour
{
    [SerializeField]
    private float width = 130f;
    [SerializeField]
    private RectTransform face;
    [SerializeField]
    private Text text;

    private int hp, maxhp;

    void Update() { }

    public int SetValue(int hp, int maxhp) {
        this.maxhp = maxhp;
        return SetValue(hp);
    }

    public int SetValue(int hp) {
        if (this.hp != hp) {
            int r = this.hp - hp;
            this.hp = hp;
            face.sizeDelta = new Vector2(width * ((float)hp / maxhp), face.sizeDelta.y);
            text.text = string.Format("{0} / {1}", hp, maxhp);
            return r;
        }
        return 0;
    }
}

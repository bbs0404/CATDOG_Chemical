using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {
    [SerializeField]
    private ObjectUnit unit;
    void Start()
    {
        unit = GetComponentInParent<ObjectUnit>();
    }
    void Update () {
            if (unit.getStatusEffect() == StatusEffect.None)
            {
                GetComponent<Image>().sprite = null;
                GetComponent<Image>().color = new Color(1, 1, 1, 0);
                unit.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                GetComponent<Image>().sprite = InGameSystemManager.Inst().getStatusSprites()[(int)unit.getStatusEffect() - 1];
                if (unit.getStatusEffect() == StatusEffect.Frostbite)
                    unit.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1, 1);
                else if (unit.getStatusEffect() == StatusEffect.Burn)
                    unit.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f, 1);
                else if (unit.getStatusEffect() == StatusEffect.Corrosion)
                    unit.GetComponent<Image>().color = new Color(0.5f, 1, 0.5f, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
	}
}

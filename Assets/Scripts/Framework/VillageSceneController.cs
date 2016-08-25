using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VillageSceneController : MonoBehaviour {

    [SerializeField]
    Image background;

    [SerializeField]
    Sprite[] backgroundImages;

    [SerializeField]
    Canvas BagCanvas;
    [SerializeField]
    Text[] items = new Text[3];
    [SerializeField]
    GameObject textPrefab;
	void Start () {
        background.sprite = backgroundImages[InGameSystemManager.villageNum % 4];
	}

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void OnBagButtonClicked()
    {
        items[0].text = InventoryManager.proton.ToString();
        items[1].text = InventoryManager.neutron.ToString();
        items[2].text = InventoryManager.electron.ToString();
        BagCanvas.gameObject.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        BagCanvas.gameObject.SetActive(false);
    }

    public void OnShopButtonClicked()
    {
        GameObject text = Instantiate(textPrefab);
        text.transform.parent = GameObject.FindObjectOfType<Canvas>().transform;
        text.GetComponent<RectTransform>().localPosition = new Vector3(-155,60,0);
        text.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}

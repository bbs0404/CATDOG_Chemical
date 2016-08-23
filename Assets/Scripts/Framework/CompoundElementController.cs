using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompoundElementController : MonoBehaviour {

	// 1 is Hydrogen.
	[SerializeField]
	private int ElementNumber;

	[SerializeField]
	private Image ElementImage;
	[SerializeField]
	private Button SelectButton;
	[SerializeField]
	private Button CompoundButton;
	[SerializeField]
	private Button ExitButton;
	[SerializeField]
	private Text ProtonNumberLabel;
	[SerializeField]
	private Text NeutronNumberLabel;
	[SerializeField]
	private Text ElectronNumberLabel;
	[SerializeField]
	private Text Status;

	private InventoryManager inventoryManager;

	/* *
	 * Utility Functions BEGIN
	 * */

	// Support 1~118
	public static int GetPeriodByElementNumber(int ElementNumber) {
		if (ElementNumber < 1 || ElementNumber > 118) {
			throw new System.ArgumentOutOfRangeException ("ElementNumber is not valid. (1 ~ 118)", "ElementNumber");
		}

		// First element number of each periods.
		int[] FirstElementNumbers = new int[] { 1, 3, 11, 19, 37, 55, 87, int.MaxValue };

		for (int i = 0; i < FirstElementNumbers.Length; i++) {
			if (ElementNumber < FirstElementNumbers[i]) {
				return i;
			}
		}

		throw new System.Exception ("Unknown exception");
	}

	// Support 1~54
	public static int GetGroupByElementNumber(int ElementNumber) {
		if (ElementNumber < 1 || ElementNumber > 54) {
			throw new System.ArgumentOutOfRangeException ("ElementNumber is not valid. (1 ~ 54)", "ElementNumber");
		}

		// First element number of each periods.
		int[] FirstElementNumbers = new int[] { 1, 3, 11, 19, 37, 55 };
		for (int i = 0; i < FirstElementNumbers.Length; i++) {
			if (ElementNumber < FirstElementNumbers[i]) {
				// i period.
				return ElementNumber - FirstElementNumbers[i - 1] + 1;
			}
		}

		throw new System.Exception ("Unknown exception");
	}

	public static int CalculateProtonNumber(int ElementNumber) {
		return ElementNumber;
	}

	public static int CalculateNeutronNumber(int ElementNumber) {
		if (ElementNumber == 1) {
			return 0;
		} else if (2 <= ElementNumber && ElementNumber <= 16 || ElementNumber == 20) {
			return ElementNumber;
		} else if (ElementNumber == 17) {
			return 18;
		} else if (ElementNumber == 18) {
			return 22;
		} else if (ElementNumber == 19) {
			return 20;
		} else {
			throw new System.ArgumentException ("ElementNumber is not valid", "ElementNumber");
		}
	}

	public static int CalculateElectronNumber(int ElementNumber) {
		int period = GetPeriodByElementNumber (ElementNumber);
		int group = GetGroupByElementNumber (ElementNumber);

		switch (period) {
		case 1:
			return (int)System.Math.Round(ElementNumber + 100 + group * 7 + ElementNumber * 1.3);
		case 2:
			return (int)System.Math.Round(ElementNumber + 150 + group * 13 + ElementNumber * 1.7);
		case 3:
			return (int)System.Math.Round(ElementNumber + 200 + group * 17 + ElementNumber * 1.9);
		case 4:
			return (int)System.Math.Round(ElementNumber + 350 + group * 19 + ElementNumber * 2.3);
		default:
			throw new System.ArgumentException ("Element number is not valid", "ElementNumber");
		}
	}

	// Returns 0.0 ~ 1.0
	public static double CalculateCompoundSuccessProbability(int ElementNumber) {
		int period = GetPeriodByElementNumber (ElementNumber);

		switch (period) {
		case 1:
			return (100.0 - ElementNumber * 2.11) / 100.0;
		case 2:
			return (95.0 - ElementNumber * 2.33) / 100.0;
		case 3:
			return (90.0 - ElementNumber * 2.57) / 100.0;
		case 4:
			return (85.0 - ElementNumber * 2.77) / 100.0;
		default:
			throw new System.ArgumentException ("Element number is not valid", "ElementNumber");
		}
	}
		
	/* *
	 * Utilitiy Functions END
	 * */


	// TODO: Remove. Temp function.
	public static int tmp = 0;
	public void foo() {
		SetElementNumber (++tmp);
		inventoryManager.setProton (inventoryManager.getProton () + 2);
		inventoryManager.setNeutron (inventoryManager.getNeutron () + 2);
		inventoryManager.setElectron (inventoryManager.getElectron () + 100);
	}


	private bool IsCompoundable() {
		int requireProtonNum = CalculateProtonNumber(ElementNumber);
		int requireNeutronNum = CalculateNeutronNumber (ElementNumber);
		int requireElectronNum = CalculateElectronNumber (ElementNumber);
		return (
		    inventoryManager.getProton () >= requireProtonNum &&
		    inventoryManager.getNeutron () >= requireNeutronNum &&
		    inventoryManager.getElectron () >= requireElectronNum &&
			!(PlayerManager.Inst ().Element [this.ElementNumber - 1])
		);
	}


	private void UpdateLabels() {
		// Set numbers.
		int requireProtonNum = CalculateProtonNumber(ElementNumber);
		ProtonNumberLabel.text = string.Format("X {0}", requireProtonNum);
		ProtonNumberLabel.color = inventoryManager.getProton () < requireProtonNum ? Color.red : Color.white;

		int requireNeutronNum = CalculateNeutronNumber (ElementNumber);
		NeutronNumberLabel.text = string.Format("X {0}", requireNeutronNum);
		NeutronNumberLabel.color = inventoryManager.getNeutron () < requireNeutronNum ? Color.red : Color.white;

		int requireElectronNum = CalculateElectronNumber (ElementNumber);
		ElectronNumberLabel.text = string.Format("{0} elec", requireElectronNum);
		ElectronNumberLabel.color = inventoryManager.getElectron () < requireElectronNum ? Color.red : Color.white;

		// Enable/Disable Compound Button
		if (IsCompoundable()) {

			CompoundButton.enabled = true;
			Color c2 = CompoundButton.image.color;
			c2.a = 1.0f;
			CompoundButton.image.color = c2;
		} else {
			CompoundButton.enabled = false;
			Color c2 = CompoundButton.image.color;
			c2.a = 0.1f;
			CompoundButton.image.color = c2;
		}
	}

	public void OnClickCompoundButton() {
		if (!IsCompoundable ()) {
			return;
		}

		// Using
		int requireProtonNum = CalculateProtonNumber(ElementNumber);
		int requireNeutronNum = CalculateNeutronNumber (ElementNumber);
		int requireElectronNum = CalculateElectronNumber (ElementNumber);
		inventoryManager.setProton (inventoryManager.getProton () - requireProtonNum);
		inventoryManager.setNeutron (inventoryManager.getNeutron () - requireNeutronNum);
		inventoryManager.setElectron (inventoryManager.getElectron () - requireElectronNum);

		string msg = string.Format ("양성자 {0}개, 중성자 {1}개, 전자 {2}개 소모", requireProtonNum, requireNeutronNum, requireElectronNum);

		// Lotto!
		double prob = CalculateCompoundSuccessProbability (this.ElementNumber);
		if (Random.Range (0.0f, 1.0f) <= prob) {
			// Success
			msg = string.Format("조합 성공! - {0}", msg);

			// Unlock
			PlayerManager.Inst ().Element [this.ElementNumber - 1] = true;
		} else {
			// Fail
			msg = string.Format("조합 실패! - {0}", msg);
		}
		Status.text = msg;
		UpdateLabels ();
	}

	public void SetElementNumber(int ElementNumber) {
		this.ElementNumber = ElementNumber;

		// Change Element Image
		Texture2D texture = Resources.Load (string.Format ("ElementImages/{0}", ElementNumber)) as Texture2D;
		ElementImage.sprite = Sprite.Create(texture, new Rect(0, 0, 320, 180), new Vector2(0.5f, 0.5f));
		Color c = ElementImage.color;
		c.a = 1.0f;
		ElementImage.color = c;

		UpdateLabels ();
	}

	// Use this for initialization
	void Start () {
		inventoryManager = InventoryManager.Inst ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

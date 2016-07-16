using System.Collections.Generic;

public class ElementManager : SingletonBehaviour<ElementManager> {
	private Dictionary<char, Element> elements;

	private ElementManager() {
		this.elements = new Dictionary<char, Element> ();

		this.elements.Add ('C', new Element ('C', 10, 2));
		this.elements.Add ('H', new Element ('H', 10, 1));
		this.elements.Add ('O', new Element ('O', 10, 1));
	}

	public Element getElement(char symbol) {
		return elements[symbol];
	}
}
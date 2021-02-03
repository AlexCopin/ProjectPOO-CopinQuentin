using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class Rune
{
    // Vars
    int _id;
    float _price;
    UI.Shape _shape;
    UI.Size _size;
    UI.RColor _rColor;
    Color _color;
    // Properties
    public int Id { get => _id; set => _id = value; }
    public float Price => _price;
    public UI.Shape Shape => _shape;
    public UI.Size Size => _size;
    public UI.RColor RColor => _rColor;
    public Color Color => _color;
    // Init
    public Rune(UI.Shape shape, UI.Size size, UI.RColor rColor)
    {
        _price = ((int) shape * 10) * UI.PriceFactor + Random.Range(1, 9);
        _shape = shape;
        _size = size;
        _rColor = rColor;
        switch (rColor)
        {
            case UI.RColor.White:
                _color = Color.white;
                break;
            case UI.RColor.Black:
                _color = Color.black;
                break;
            case UI.RColor.Red:
                _color = Color.red;
                break;
            case UI.RColor.Green:
                _color = Color.green;
                break;
            case UI.RColor.Blue:
                _color = Color.blue;
                break;
        }
    }
}
class UI : MonoBehaviour
{
    // UI
    GameObject log, details;
    Text recipe, price;
    Button craftButton, clearCraftButton, clearResultButton, curButton, prevButton, nextButton;
    Dropdown shapeDrop, sizeDrop, colorDrop;
    // Dropdowns
    Shape shape;
    Size size;
    RColor rColor;
    // Enums
    public enum Shape
    {
        Triangle = 3,
        Quad = 4,
        Pentagon = 5,
        Hexagon = 6,
        Heptagon = 7,
        Octagon = 8
    }
    public enum Size
    {
        Small,
        Medium,
        Big
    }
    public enum RColor
    {
        White,
        Black,
        Red,
        Green,
        Blue
    }
    // Lists
    List<Rune> runes = new List<Rune>();
    // Vars
    int curId = -1, navId = -1;
    Rune curRune, lastRune;
    // Properties
    public static float PriceFactor { get; private set; }
    // Methods
    void Craft()
    {
        Rune rune = new Rune(shape, size, rColor);
        curId++;
        navId = curId;
        rune.Id = curId;
        runes.Add(rune);
        curRune = runes[curId];
        Log(rune);
        StartCoroutine(LogCoroutine());
    }
    IEnumerator LogCoroutine()
    {
        log.SetActive(true);
        yield return new WaitForSeconds(4);
        log.SetActive(false);
    }
    void Log(Rune rune)
    {
        Debug.Log("<b>Rune #" + rune.Id + "</b> - Price: " + rune.Price + ", Shape: " + rune.Shape + ", Size: " + rune.Size + ", Color: " +
            string.Format("<color=#{0:x2}{1:x2}{2:x2}>{3}</color>", (byte) rune.Color.r * 255, (byte) rune.Color.g * 255, (byte) rune.Color.b * 255, rune.RColor));
    }
    void Clear(string arg)
    {
        switch (arg)
        {
            case "craft":
                shapeDrop.value = 0;
                sizeDrop.value = 0;
                colorDrop.value = 0;
                break;
            case "result":
                runes.Clear();
                curId = -1;
                break;
        }
    }
    void Show(string arg)
    {
        switch (arg)
        {
            case "cur":
                navId = curId;
                break;
            case "prev":
                if (navId > 0)
                    navId--;
                break;
            case "next":
                if (navId < runes.Count - 1)
                    navId++;
                break;
        }
        curRune = runes[navId];
    }
    void Awake()
    {
        // Logs
        log = GameObject.Find("Log");
        log.SetActive(false);
        details = GameObject.Find("Details");
        // Texts
        recipe = GameObject.Find("Recipe").GetComponent<Text>();
        price = GameObject.Find("Price").GetComponent<Text>();
        // Buttons
        craftButton = GameObject.Find("Button_Craft").GetComponent<Button>();
        clearCraftButton = GameObject.Find("Button_Clear_Craft").GetComponent<Button>();
        clearResultButton = GameObject.Find("Button_Clear_Result").GetComponent<Button>();
        curButton = GameObject.Find("Button_Cur").GetComponent<Button>();
        prevButton = GameObject.Find("Button_Prev").GetComponent<Button>();
        nextButton = GameObject.Find("Button_Next").GetComponent<Button>();
        craftButton.onClick.AddListener(Craft);
        clearCraftButton.onClick.AddListener(delegate { Clear("craft"); });
        clearResultButton.onClick.AddListener(delegate { Clear("result"); });
        curButton.onClick.AddListener(delegate { Show("cur"); });
        prevButton.onClick.AddListener(delegate { Show("prev"); });
        nextButton.onClick.AddListener(delegate { Show("next"); });
        // Dropdowns
        shapeDrop = GameObject.Find("Dropdown0").GetComponent<Dropdown>();
        sizeDrop = GameObject.Find("Dropdown1").GetComponent<Dropdown>();
        colorDrop = GameObject.Find("Dropdown2").GetComponent<Dropdown>();
    }
    void Update()
    {
        // Logs
        details.SetActive(runes.Count > 0 ? true : false);
        if (runes.Count > 0)
            details.GetComponent<Text>().text = string.Format("<b>Rune #{0}</b>\n-\nPrice: {1}\nShape: {2}\nSize: {3}\nColor: {4}",
                curRune.Id, curRune.Price, curRune.Shape, curRune.Size, string.Format("<color=#{0:x2}{1:x2}{2:x2}>{3}</color>",
                    (byte) curRune.Color.r * 255, (byte) curRune.Color.g * 255, (byte) curRune.Color.b * 255, curRune.RColor));
        // Dropdowns
        if (shapeDrop.value != 0)
            shape = (Shape) System.Enum.Parse(typeof(Shape), shapeDrop.options[shapeDrop.value].text);
        if (colorDrop.value != 0)
            rColor = (RColor) System.Enum.Parse(typeof(RColor), colorDrop.options[colorDrop.value].text);
        if (sizeDrop.value != 0)
        {
            size = (Size) System.Enum.Parse(typeof(Size), sizeDrop.options[sizeDrop.value].text);
            switch (size)
            {
                case Size.Small:
                    PriceFactor = .5F;
                    break;
                case Size.Medium:
                    PriceFactor = 1;
                    break;
                case Size.Big:
                    PriceFactor = 1.5F;
                    break;
            }
        }
        // Texts
        recipe.text = (shapeDrop.value != 0 ? shapeDrop.options[shapeDrop.value].text : "?") + " - " +
            (sizeDrop.value != 0 ? sizeDrop.options[sizeDrop.value].text : "?") + " - " +
            (colorDrop.value != 0 ? colorDrop.options[colorDrop.value].text : "?");
        price.text = shapeDrop.value != 0 && sizeDrop.value != 0 ?
            "Estimated Price: " + ((int) shape * 10) * PriceFactor + "-" + (((int) shape * 10) * PriceFactor + 5) + " (or more with luck)" : "Estimated Price: ?";
        // Buttons
        craftButton.GetComponent<Image>().color = shapeDrop.value != 0 && sizeDrop.value != 0 && colorDrop.value != 0 ? Color.white : Color.gray;
        clearCraftButton.GetComponent<Image>().color = shapeDrop.value != 0 || sizeDrop.value != 0 || colorDrop.value != 0 ? Color.white : Color.gray;
        clearResultButton.GetComponent<Image>().color = runes.Count > 0 ? Color.white : Color.gray;
        curButton.GetComponent<Image>().color = runes.Count > 1 ? (navId != runes.Count - 1 ? Color.white : Color.gray) : Color.gray;
        prevButton.GetComponent<Image>().color = runes.Count == 0 ? Color.gray : (navId == 0 ? Color.gray : Color.white);
        nextButton.GetComponent<Image>().color = runes.Count == 0 ? Color.gray : (navId == runes.Count - 1 ? Color.gray : Color.white);
        craftButton.enabled = shapeDrop.value != 0 && sizeDrop.value != 0 && colorDrop.value != 0 ? true : false;
        clearCraftButton.enabled = shapeDrop.value != 0 || sizeDrop.value != 0 || colorDrop.value != 0 ? true : false;
        clearResultButton.enabled = runes.Count > 0 ? true : false;
        curButton.enabled = runes.Count > 1 ? (navId != runes.Count - 1 ? true : false) : false;
        prevButton.enabled = runes.Count != 0 ? (navId != 0 ? true : false) : false;
        nextButton.enabled = runes.Count != 0 ? (navId != runes.Count - 1 ? true : false) : false;
    }
}
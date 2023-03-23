using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{

    public static int floorSize;
    public static string seed;

    [SerializeField]
    ToggleGroup toggleGroup;
    [SerializeField]
    TextMeshProUGUI seedText;

    public void GetFloorSizeAndSeed()
    {
        string name = toggleGroup.GetFirstActiveToggle().name;
        if (name == "Small")
        {
            floorSize = 0;
        }
        else if (name == "Medium")
        {
            floorSize = 1;
        }
        else if (name == "Large")
        {
            floorSize = 2;
        }
        seed = seedText.text;
    }

}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SimulateManager : MonoBehaviour
{

    public string apiUrl = "https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa";
    public GameObject TemplateObj;
    public GameObject TopTemplate;
    public GameObject HolderSecond;
    public Transform Holder;
    private string dataText;
    private PlayerData data;

    private TextMeshProUGUI TemplateText;


    private GameObject topObj;
    private GameObject obj;

    private void Start()
    {
        StartCoroutine(GetDataFromAPI());
    }

    IEnumerator GetDataFromAPI()
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            dataText = www.downloadHandler.text;
            Debug.Log("Response : \n" + dataText);
            data = JsonUtility.FromJson<PlayerData>(dataText);
            Debug.Log("Length of Items : " + data.metadata.privateData);


            topObj = Instantiate(TopTemplate, Holder);
            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.yellow;
            TemplateText.text ="Name: " +  data.record.playerName.ToString();

            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.yellow;
            TemplateText.text = "Level: " + data.record.level.ToString();

            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.yellow;
            TemplateText.text ="Health: " + data.record.health.ToString();

            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.yellow;
            TemplateText.text = "Position: " + data.record.position.ToString();


            for(int i = 0; i < data.record.inventory.Count; i++)
            {

                topObj = Instantiate(TopTemplate, Holder);
                obj = Instantiate(TemplateObj, topObj.transform);
                TemplateText = obj.GetComponent<TextMeshProUGUI>();
                TemplateText.color = Color.white;
                TemplateText.text = "Inventory " + i;

                obj = Instantiate(TemplateObj, topObj.transform);
                TemplateText = obj.GetComponent<TextMeshProUGUI>();
                TemplateText.color = Color.white;
                TemplateText.text = "Type :" + data.record.inventory[i].itemName.ToString();

                obj = Instantiate(TemplateObj, topObj.transform);
                TemplateText = obj.GetComponent<TextMeshProUGUI>();
                TemplateText.color = Color.white;
                TemplateText.text = "Quantity :" + data.record.inventory[i].quantity.ToString();

                obj = Instantiate(TemplateObj, topObj.transform);
                TemplateText = obj.GetComponent<TextMeshProUGUI>();
                TemplateText.color = Color.white;
                TemplateText.text = "Weight :" + data.record.inventory[i].weight.ToString();

            }

            topObj = Instantiate(TopTemplate, HolderSecond.transform);
            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.red;
            TemplateText.text = "Meta Data";

            RectTransform rectObj = topObj.GetComponent<RectTransform>();
            rectObj.sizeDelta = new Vector2(150, 200);


            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.red;
            TemplateText.text = "Id :" + data.metadata.id.ToString();

            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.red;
            TemplateText.text = "Private :" + data.metadata.privateData.ToString();

            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.red;
            TemplateText.text = "CreatedAt :" + data.metadata.createdAt.ToString();

            obj = Instantiate(TemplateObj, topObj.transform);
            TemplateText = obj.GetComponent<TextMeshProUGUI>();
            TemplateText.color = Color.red;
            TemplateText.text = "name :" + data.metadata.id.ToString();


        }
    }

}

[System.Serializable]
public class recordItem
{
    private string playerName;
    private int level;
    private float health;
    private Position position;
}

[System.Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class PlayerRecord
{
    public string playerName;
    public int level;
    public float health;
    public Position position;
    public List<InventoryItem> inventory;
}

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
    public int weight;
}

[System.Serializable]
public class Metadata
{
    public string id;
    public bool privateData;
    public string createdAt;
    public string name;
}

[System.Serializable]
public class PlayerData
{
    public PlayerRecord record;
    public Metadata metadata;
}

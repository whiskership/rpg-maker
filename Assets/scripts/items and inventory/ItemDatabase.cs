using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    List<Item> database = new List<Item>();
    JsonData itemData;

    void Awake() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ItemDatabase.json"));
        ConstructItemDatabase();
    }

    //adds items from json file to database list
    void ConstructItemDatabase() {
        for (int i = 0; i < itemData.Count; i++) {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"], itemData[i]["slug"].ToString()));
        }
    }

    public Item FetchItemByID(int id) {
        if (id < database.Count) {
            return database[id];
        }
        else {
            Debug.LogError("item not in databade, out of range");
            return null;
        }
    }
}

public class Item {
    public int ID { get; set; }    
    public string Title { get; set; }
    public int Value { get; set; }
    public string Slug { get; set; }

    public Item(int id, string title, int value, string slug) {
        ID = id;
        Title = title;
        Value = value;
        Slug = slug;
    }
}
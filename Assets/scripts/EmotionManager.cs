using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour {
    public static EmotionManager Instance { get; set; }
    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public enum EmotionSprites {
        Exclamation
    }

    static SpriteRenderer spriteRend;
    void Start() {
        spriteRend = Instance.GetComponent<SpriteRenderer>();
    }

    public Sprite[] emotionSprites;
    public static void SetEmotion(Transform onWho, EmotionSprites emotion) {
        SpriteRenderer spriteRend = Instance.GetComponent<SpriteRenderer>();

        switch (emotion) {
            case EmotionSprites.Exclamation:
            spriteRend.enabled = true;
            spriteRend.sprite = Instance.emotionSprites[0];
            //could add a sound effect too.
            break;
        }

        Instance.transform.position = new Vector3(onWho.position.x, onWho.position.y + 16, 0);
    }

    public static void SetEmotion(Vector2 where, EmotionSprites emotion) {

        switch (emotion) {
            case EmotionSprites.Exclamation:
            spriteRend.sprite = Instance.emotionSprites[0];
            break;
        }

        Instance.transform.position = new Vector3(where.x, where.y, 0);
    }

    public static void HideEmotion() {
        spriteRend.enabled = false;
    }
}

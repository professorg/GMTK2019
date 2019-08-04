using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStateBlock : MonoBehaviour
{
    public bool startEnabled;
    public Sprite onSprite, offSprite;

    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        isOn = startEnabled;
        GetComponent<SpriteRenderer>().sprite = (isOn ? onSprite : offSprite);
        GetComponent<Collider2D>().enabled = isOn;
    }

    public void Toggle()
    {
        isOn = !isOn;
        GetComponent<SpriteRenderer>().sprite = (isOn ? onSprite : offSprite);
        GetComponent<Collider2D>().enabled = isOn;
    }

}

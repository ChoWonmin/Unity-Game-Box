using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Star : MonoBehaviour
{
    private Tilemap tileMap;
    // Start is called before the first frame update
    void Awake()
    {
        tileMap = GetComponent<Tilemap>();
        // Twinkle();
    }

    private void Update()
    {
    }

    private void Twinkle()
    {
        Debug.Log("Twinkle");
        if (tileMap.color.a == 1)
        {
            tileMap.color = new Vector4(1, 1, 1, 0);
        } else
        {
            tileMap.color = new Vector4(1, 1, 1, 1);
        }
        
        Invoke("Twinkle", 1);
        
    }

}

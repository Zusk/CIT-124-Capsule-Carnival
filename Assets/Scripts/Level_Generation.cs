using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generation : MonoBehaviour
{

    public GameObject decoPrefab;
    public GameObject prefab;
    public GameObject player;
    public Texture2D levelImage;
    public float prefabSize = 0.25f;
    public float backgroundDist = 0.125f;
    public List<Color> groundColors;
    private bool checkColor(Color pixColor)
    {
        foreach (Color color in groundColors)
        {
            if (pixColor.Equals(color))
            {
                return true;
            }
        }
        return false;
    }
    private void ColorChange(Renderer renderer, Color pixelColor)
    {
        var tempMaterial = new Material(renderer.sharedMaterial)
        {
            color = pixelColor
        };
        renderer.sharedMaterial = tempMaterial;
    }
    public void newer_Map_Logic()
    {
        //get the width and height of the image
        int width = levelImage.width;
        //int height = levelImage.height;

        //get the pixels from the image
        Color[] pixels = levelImage.GetPixels();

        //iterate through the pixels
        for (int i = 0; i < pixels.Length; i++)
        {
            //get the current pixel
            Color pixel = pixels[i];

            //get the x and y position of the pixel
            int x = i % width;
            int y = i / width;

            if (pixel.Equals(Color.red))
            {
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                Transform tile = Instantiate(player, spawnPos, Quaternion.identity).transform;
                tile.SetParent(transform);
            }
            else if (checkColor(pixel))
            {
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                Transform tile = Instantiate(prefab, spawnPos, Quaternion.identity).transform;
                ColorChange(tile.GetComponent<Renderer>(), pixel);
                tile.SetParent(transform);
            }
            else
            {
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, backgroundDist);
                Transform tile = Instantiate(decoPrefab, spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0))).transform;
                ColorChange(tile.GetComponent<Renderer>(), pixel);
                tile.SetParent(transform);
            }

            //instantiate a tile at the pixel's position
            //GameObject tile = Instantiate(prefab, new Vector3(x * prefabSize, y * prefabSize, 0), Quaternion.identity);
        }
    }
    public void new_Map_Logic()
    {
        for (int x = 0; x < levelImage.width; x++)
        {
            for (int y = 0; y < levelImage.height; y++)
            {
                // Get the pixel color at this position
                Color pixelColor = levelImage.GetPixel(x, y);
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);


                // If the pixel is black, spawn a prefab
                if (pixelColor.Equals(Color.red))
                {
                    Debug.Log("Player");
                    spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                    Transform tile = Instantiate(player, spawnPos, Quaternion.identity).transform;
                    tile.SetParent(transform);
                }
                else if (checkColor(pixelColor))
                {
                    Debug.Log("Ground");
                    spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                    Transform tile = Instantiate(prefab, spawnPos, Quaternion.identity).transform;
                    ColorChange(tile.GetComponent<Renderer>(), pixelColor);
                    tile.SetParent(transform);
                }
                else
                {
                    Debug.Log("Deco");
                    Debug.Log("Color: " + pixelColor);
                    Debug.Log(groundColors[0] + " " + groundColors[1]);
                    spawnPos = new Vector3(x * prefabSize, y * prefabSize, backgroundDist);
                    Transform tile = Instantiate(decoPrefab, spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0))).transform;
                    ColorChange(tile.GetComponent<Renderer>(), pixelColor);
                    tile.SetParent(transform);
                }
            }
        }
    }

}
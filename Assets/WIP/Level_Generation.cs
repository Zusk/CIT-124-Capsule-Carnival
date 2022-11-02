using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generation : MonoBehaviour
{

    public GameObject decoPrefab;
    public GameObject prefab;
    public GameObject player;
    public GameObject coin;
    public Texture2D levelImage;
    public Texture2D levelColorImage;
    public Transform environmentHolder;
    public float prefabSize = 0.25f;
    public float backgroundDist = 0.125f;
    public List<Color> levelGenColors;
    public bool generate_Walls = true;
    private bool checkColor(Color pixColor)
    {
        foreach (Color color in levelGenColors)
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
    public void Map_Logic()
    {
        //get the width and height of the image
        int width = levelImage.width;
        //int height = levelImage.height;

        //get the pixels from the image
        Color[] pixels = levelImage.GetPixels();
        Color[] colorPixels = levelColorImage.GetPixels();
        //iterate through the pixels
        for (int i = 0; i < pixels.Length; i++)
        {
            //get the current pixel
            Color pixel = pixels[i];
            Debug.Log("Pixel: " + pixel.r + ", " + pixel.g + ", " + pixel.b + ", " + pixel.a);
            //get the x and y position of the pixel, as we are iterating through the entire image.
            int x = i % width;
            int y = i / width;

            //Do things if the pixel is read, which is what we are using for our spawn position right now.
            if (pixel.Equals(Color.red))
            {
                //Gets a position to place the player based on our current pixels iteration
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                //Instantiates the player prefab there
                Transform tile = Instantiate(player, spawnPos, Quaternion.identity).transform;
                //Sets the player's parent
                tile.SetParent(transform);
                //This creates a wall if the bool 'generate walls' is active.
                if (generate_Walls)
                {
                    //Defines a "spawnPos", which is a vector 3. Give it a location based on our current pixel in the ref image
                    spawnPos = new Vector3(x * prefabSize, y * prefabSize, backgroundDist);
                    //Place the wall prefab, 
                    tile = Instantiate(decoPrefab, spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0))).transform;
                    //Changes the wall prefab's color
                    ColorChange(tile.GetComponent<Renderer>(), colorPixels[i]);
                    //Sets the wall's parent
                    tile.SetParent(environmentHolder);
                }
            }
            else if (pixel.Equals(Color.yellow))
            {
                //Gets a position to place the player based on our current pixels iteration
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                //Instantiates the player prefab there
                Transform tile = Instantiate(coin, spawnPos, Quaternion.identity).transform;
                //Sets the player's parent
                tile.SetParent(transform);
                //This creates a wall if the bool 'generate walls' is active.
                if (generate_Walls)
                {
                    //Defines a "spawnPos", which is a vector 3. Give it a location based on our current pixel in the ref image
                    spawnPos = new Vector3(x * prefabSize, y * prefabSize, backgroundDist);
                    //Place the wall prefab, 
                    tile = Instantiate(decoPrefab, spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0))).transform;
                    //Changes the wall prefab's color
                    ColorChange(tile.GetComponent<Renderer>(), colorPixels[i]);
                    //Sets the wall's parent
                    tile.SetParent(environmentHolder);
                }
            }
            //Otherwise, if we found 'any' pixel
            else if (checkColor(pixel))
            {
                //This generates a ground tile that you can walk on, then gives it a good color - only if 'some color' is there though.
                //Similar logic to the above
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, 0);
                Transform tile = Instantiate(prefab, spawnPos, Quaternion.identity).transform;
                ColorChange(tile.GetComponent<Renderer>(), colorPixels[i]);
                tile.SetParent(environmentHolder);
            }
            //Otherwise, if the bool 'generate walls' is active
            else if (generate_Walls)
            {
                //This generates a wall tile if generate walls is active, only if the above is activated.
                Vector3 spawnPos = new Vector3(x * prefabSize, y * prefabSize, backgroundDist);
                Transform tile = Instantiate(decoPrefab, spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0))).transform;
                ColorChange(tile.GetComponent<Renderer>(), colorPixels[i]);
                tile.SetParent(environmentHolder);
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public string seed;
    public int currentSeed;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    public int gridX;
    public int gridY;

    public int minRooms;
    public int maxRooms;

    public GameObject room;
    public GameObject startRoom;
    public GameObject bossRoom;
    public GameObject door;

    private int[,] matrix;
    private int leftRooms;

    Camera cam;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        GetSeed();
        GenerateMatrix();
        Generate();

    }

    private void GetSeed()
    {
        if (seed.Length != 8)
        {
            seed = "";
            for (int i = 0; i < 8; i++)
            {
                seed += glyphs[Random.Range(0, glyphs.Length)];
            }
        }
        currentSeed = seed.GetHashCode();
        Random.InitState(currentSeed);
    }

    private void GenerateMatrix()
    {
        // 0 is nothing, 1 is door, more than 1 is room
        matrix = new int[gridX * 2 - 1, gridY * 2 - 1];
        // Starting room
        matrix[gridX - 1, gridY - 1] = 2;
        // Number of rooms
        leftRooms = Random.Range(minRooms, maxRooms + 1) - 1;
        PopulateMatrix();
    }

    private void PopulateMatrix()
    {
        for (int i = 2; i < gridX * 2 - 3; i = i + 2)
        {
            for (int j = 2; j < gridY * 2 - 3; j = j + 2)
            {
                if (matrix[i, j] != 0)
                {
                    int random = Random.Range(0, 100);
                    if (random < 20)
                    {
                        if (matrix[i + 2, j] == 0)
                        {
                            matrix[i + 1, j] = 1;
                            matrix[i + 2, j] = matrix[i, j] + 1;
                            leftRooms--;
                        }
                        else
                        {
                            matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 40)
                    {
                        if (matrix[i - 2, j] == 0)
                        {
                            matrix[i - 1, j] = 1;
                            matrix[i - 2, j] = matrix[i, j] + 1;
                            leftRooms--;
                        }
                        else
                        {
                            matrix[i - 1, j] = 1;
                        }
                    }
                    else if (random < 60)
                    {
                        if (matrix[i, j + 2] == 0)
                        {
                            matrix[i, j + 1] = 1;
                            matrix[i, j + 2] = matrix[i, j] + 1;
                            leftRooms--;
                        }
                        else
                        {
                            matrix[i, j + 1] = 1;
                        }
                    }
                    else if (random < 80)
                    {
                        if (matrix[i, j - 2] == 0)
                        {
                            matrix[i, j - 1] = 1;
                            matrix[i, j - 2] = matrix[i, j] + 1;
                            leftRooms--;
                        }
                        else
                        {
                            matrix[i, j - 1] = 1;
                        }
                    }
                    else if (random < 100)
                    {

                    }
                    else if (random < 102)
                    {
                        if (matrix[i + 2, j] == 0)
                        {
                            matrix[i + 2, j] = matrix[i, j];
                            matrix[i + 1, j] = matrix[i, j];
                        }
                        else
                        {
                            matrix[i + 1, j] = 1;
                        }
                    }
                    else if (random < 104)
                    {
                        if (matrix[i - 2, j] == 0)
                        {
                            matrix[i - 2, j] = matrix[i, j];
                            matrix[i - 1, j] = matrix[i, j];
                        }
                        else
                        {
                            matrix[i - 1, j] = 1;
                        }
                    }
                    else if (random < 106)
                    {
                        if (matrix[i, j + 2] == 0)
                        {
                            matrix[i, j + 2] = matrix[i, j];
                            matrix[i, j + 1] = matrix[i, j];
                        }
                        else
                        {
                            matrix[i, j + 1] = 1;
                        }
                    }
                    else
                    {
                        if (matrix[i, j - 2] == 0)
                        {
                            matrix[i, j - 2] = matrix[i, j];
                            matrix[i, j - 1] = matrix[i, j];
                        }
                        else
                        {
                            matrix[i, j - 1] = 1;
                        }
                    }
                    if (leftRooms <= 0) return;
                }
            }
        }
        if (leftRooms > 0) PopulateMatrix();
    }

    private void Generate()
    {
        GameObject grid = FindObjectOfType<Grid>().gameObject;
        GameObject rooms = new GameObject();
        rooms.name = "Rooms";
        GameObject doors = new GameObject();
        doors.name = "Doors";

        rooms.transform.SetParent(grid.transform);
        doors.transform.SetParent(grid.transform);

        for (int i = 0; i < gridX * 2 - 1; i++)
        {
            for (int j = 0; j < gridY * 2 - 1; j++)
            {
                if (matrix[i, j] == 1)
                {
                    GameObject door1 = Instantiate(door, new Vector3(i * 10, j * 7, 0), Quaternion.identity, doors.transform);
                    GameObject door2 = Instantiate(door, new Vector3(i * 10, j * 7, 0), Quaternion.identity, doors.transform);
                    if (matrix[i, j - 1] >= 1 || matrix[i, j + 1] >= 1)
                    {
                        door1.transform.position += Vector3.up * 3;
                        door1.transform.eulerAngles = new Vector3(0, 0, -90);
                        door2.transform.position += Vector3.down * 3;
                        door2.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else if (matrix[i - 1, j] >= 1 || matrix[i + 1, j] >= 1)
                    {
                        door1.transform.position += Vector3.left * 2;
                        door2.transform.position += Vector3.right * 2;
                        door2.transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                }
                else if (matrix[i, j] != 0)
                {
                    Instantiate(room, new Vector3(i * 10, j * 7, 0), Quaternion.identity, rooms.transform);
                }
            }
        }

        Vector3 maxValue = Vector3.zero;

        for (int i = 0; i < gridX * 2 - 1; i += 2)
        {
            for (int j = 0; j < gridY * 2 - 1; j += 2)
            {
                if (maxValue.z < matrix[i, j])
                {
                    maxValue = new Vector3(i, j, matrix[i, j]);
                }
            }
        }

        Instantiate(bossRoom, new Vector3(maxValue.x * 10, maxValue.y * 7, 0), Quaternion.identity, rooms.transform);
        Instantiate(startRoom, new Vector3((gridX - 1) * 10, (gridY - 1) * 7, 0), Quaternion.identity, rooms.transform);

        cam.transform.position = new Vector3((gridX - 1) * 10, (gridY - 1) * 7, 0);

    }

}

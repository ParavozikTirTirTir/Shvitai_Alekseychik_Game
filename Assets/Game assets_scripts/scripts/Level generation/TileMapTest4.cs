using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//����� �������
public class TileMapTest4 : MonoBehaviour
{
    public PlayerSpawn playerSpawn;

    public Tilemap tilemap;
    public string floorTilePath;
    public Vector3Int startPosition;
    public int walkLength;
    public int mapWidth;
    public int mapHeight;
    public int borderSize;
    public int tunnelWidth = 1;
    public float chanceToContinue = 0.8f;
    public float perlinNoiseScale = 0.1f; // ����� ���������� ��� �������� �������
    public float perlinNoiseThreshold = 0.5f; // ����� ���������� ��� ������ �������

    private TileBase floorTile;
    private List<Vector3Int> path = new List<Vector3Int>();

    void Start()
    {
        startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0);
        LoadTile();
        GenerateMap();
        playerSpawn = GetComponent<PlayerSpawn>();
        if (playerSpawn != null)
        {
            playerSpawn.SpawnPrefabs(mapWidth);
        }
    }

    void LoadTile()
    {
        floorTile = Resources.Load<TileBase>(floorTilePath);
        if (floorTile == null)
        {
            Debug.LogError("���� ���� �� ������.");
        }
    }

    void GenerateMap()
    {
        // ������������� ����� ������ �����
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }

        //��������� ���� � �������������� Perlin Noise
        GeneratePathWithPerlin();


        foreach (Vector3Int position in path)
        {
            ClearTunnel(position);
        }

        Debug.Log("����� ������������� � �������.");
    }
    void GeneratePathWithPerlin()
    {
        path.Clear();
        Vector3Int currentPosition = startPosition;
        path.Add(currentPosition);

        for (int i = 0; i < walkLength; i++)
        {
            Vector3Int nextPosition = GetNextPositionWithPerlin(currentPosition);
            path.Add(nextPosition);
            currentPosition = nextPosition;
        }
    }

    Vector3Int GetNextPositionWithPerlin(Vector3Int currentPos)
    {
        // ���������� Perlin Noise ��� ������ �����������
        int newX = currentPos.x;
        int newY = currentPos.y;

        float noiseValue = Mathf.PerlinNoise(currentPos.x * perlinNoiseScale, currentPos.y * perlinNoiseScale);

        if (noiseValue > perlinNoiseThreshold)
        {
            if (Random.value < 0.5f)
                newX += 1;
            else
                newX -= 1;

        }
        else
        {
            if (Random.value < 0.5f)
                newY += 1;
            else
                newY -= 1;
        }

        // ����������� ������
        int minX = borderSize;
        int maxX = mapWidth - 1 - borderSize;
        int minY = borderSize;
        int maxY = mapHeight - 1 - borderSize;

        newX = Mathf.Clamp(newX, minX, maxX);
        newY = Mathf.Clamp(newY, minY, maxY);

        return new Vector3Int(newX, newY, 0);
    }


    void ClearTunnel(Vector3Int centerTile)
    {
        for (int x = -tunnelWidth; x <= tunnelWidth; x++)
        {
            for (int y = -tunnelWidth; y <= tunnelWidth; y++)
            {
                Vector3Int neighbourPos = centerTile + new Vector3Int(x, y, 0);
                tilemap.SetTile(neighbourPos, null);
            }
        }
    }


}

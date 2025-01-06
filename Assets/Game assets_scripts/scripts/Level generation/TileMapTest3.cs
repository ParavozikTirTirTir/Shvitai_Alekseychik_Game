using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//��� �������

public class TileMapTest3 : MonoBehaviour
{
    public Tilemap tilemap;
    public string floorTilePath;
    public Vector3Int startPosition;
    public int mapWidth = 50;
    public int mapHeight = 50;
    public int borderSize = 10;
    public float noiseScale = 0.1f; // ������� ����
    public float threshold = 0.5f; // ����� ��� ����������� �����
    public int seed = 0;  // ��� ��� ���������
    private TileBase floorTile;

    void Start()
    {
        startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0);
        LoadFloorTile();
        GenerateMap();
    }

    void LoadFloorTile()
    {
        floorTile = Resources.Load<TileBase>(floorTilePath);
        if (floorTile == null)
        {
            Debug.LogError("���� ���� �� ������.");
        }
    }
    void GenerateMap()
    {
        // �������������� ��� ����� �����
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }

        Random.InitState(seed);

        // �������� �� ����� � ������� �����, ��� ��� ������� ���� ������.
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float perlinValue = Mathf.PerlinNoise((x + Random.value) * noiseScale, (y + Random.value) * noiseScale);


                // ��������� ����������� �������
                if (x < borderSize || x >= mapWidth - borderSize || y < borderSize || y >= mapHeight - borderSize)
                {
                    // ��������� ���� ���� �� �������
                }
                else if (perlinValue < threshold)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null); // ������� ����
                }
            }
        }
    }
}

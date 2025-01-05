using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTest : MonoBehaviour
{
    public Tilemap tilemap; // ������ �� Tilemap � �����
    public string floorTilePath; // ���� � ����� ����
    public Vector3Int startPosition; // ��������� ������� ��� ���������
    public int walkLength;   // ����� ���������� ���������
    public int mapWidth;       // ������ �����
    public int mapHeight;      // ������ �����
    public int borderSize;     // ������ ������� �� ���� �����
    public int tunnelWidth = 1;      // ������ �������
    public float chanceToContinue = 0.8f; // ����������� ���������� � ��� �� �����������

    private TileBase floorTile;    // ���� ����
    private List<Vector3Int> path = new List<Vector3Int>();// ���� ���������� ���������

    void Start()
    {
        startPosition = new Vector3Int(mapWidth / 2, mapHeight / 2, 0); // ������� ��������� ������� ��� ���������
        LoadTile();   // ��������� ���� �� Resources
        GenerateMap(); // ���������� ����� � �������
    }

    void LoadTile()
    {
        // �������� ����� ���� �� Resources
        floorTile = Resources.Load<TileBase>(floorTilePath);

        // ��������, ��� �� ���� ��������
        if (floorTile == null)
        {
            Debug.LogError("���� ���� �� ������ �� ����: " + floorTilePath + ". ���������, ��� ���� ���������� � ���� ��������� � ����� Resources.");
        }
    }


    void GenerateMap()
    {
        // 1. ��������� ����� ������� ����
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }
        // 2. ���������� ���� ���������� ���������
        GenerateRandomWalk();

        // 3. ������� ����� ���� � ������������ � ����� ���������:
        foreach (Vector3Int position in path) // ���������� �������� ���������
        {
            ClearTunnel(position);
        }


        // ��������� �� ������:
        Debug.Log("����� ������������� � ������� �� ��������� ���������� ���������.");
    }

    //�������� ���� �� ������ � ����������� �� tunnelWidth ��� ���������� ������ ����
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

    void GenerateRandomWalk()
    {
        path.Clear();
        Vector3Int currentPosition = startPosition;
        Vector3Int previousDir = Vector3Int.zero;
        path.Add(currentPosition);

        for (int i = 0; i < walkLength; i++)
        {
            Vector3Int nextPosition = GetNextPosition(currentPosition, previousDir);
            previousDir = nextPosition - currentPosition;
            path.Add(nextPosition);
            currentPosition = nextPosition;
        }
    }

    Vector3Int GetNextPosition(Vector3Int currentPos, Vector3Int previousDir)
    {
        Vector3Int nextPos = GetRandomDirection(currentPos, previousDir);

        // ���������, �� ������� �� nextPos �� �������, �������� borderSize ������
        int minX = borderSize;
        int maxX = mapWidth - 1 - borderSize;
        int minY = borderSize;
        int maxY = mapHeight - 1 - borderSize;


        nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);
        return nextPos;
    }

    Vector3Int GetRandomDirection(Vector3Int currentPos, Vector3Int previousDir)
    {
        if (Random.value <= chanceToContinue && previousDir != Vector3Int.zero)
        {
            return currentPos + previousDir;
        }

        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: return currentPos + Vector3Int.right;
            case 1: return currentPos + Vector3Int.left;
            case 2: return currentPos + Vector3Int.up;
            case 3: return currentPos + Vector3Int.down;
        }
        return currentPos;
    }
}
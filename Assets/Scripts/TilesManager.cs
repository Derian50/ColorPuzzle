using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TilesManager : MonoBehaviour
{
    [SerializeField] private int _widthArray;
    [SerializeField] private int _heightArray;
    private GameObject[,] _tiles;
    private SpriteRenderer[,] _tilesRenderer;
    private GameObject[] _tempArrTiles;
    private Vector3[,] _orderedTilesPosition;
    void Start()
    {
        _tiles = new GameObject[_heightArray, _widthArray];
        _tilesRenderer = new SpriteRenderer[_heightArray, _widthArray];
        _orderedTilesPosition = new Vector3[_heightArray, _widthArray];
        SortTilesByCoordinates();
        FillArrayTiles();
        CalculateIntermediateColors();
        MixTiles();
        IsMixedTilesOrdered();
    }

    private void OnEnable()
    {
        EventBus.onSwapTiles += IsMixedTilesOrdered;
    }
    private void OnDisable()
    {
        EventBus.onSwapTiles -= IsMixedTilesOrdered;
    }
    void MixTiles()
    {
        List<GameObject> tempTilesForSwap = new List<GameObject>();
        for (int i = 0; i < _heightArray; i++)
        {
            for (int j = 0; j < _widthArray; j++)
            {
                if (!_tiles[i, j].GetComponent<Tile>().isLock)
                {
                    tempTilesForSwap.Add(_tiles[i, j]);
                }
            }
        }
        for (int i = 0; i < tempTilesForSwap.Count; i++)
        {
            GameObject temp = tempTilesForSwap[i];
            int randomIndex = Random.Range(i, tempTilesForSwap.Count);
            tempTilesForSwap[i] = tempTilesForSwap[randomIndex];
            tempTilesForSwap[randomIndex] = temp;
        }
        Vector3 t = tempTilesForSwap[0].transform.position;
        for(int i = 0; i < tempTilesForSwap.Count; i++)
        {
            if(i == tempTilesForSwap.Count - 1)
            {
                tempTilesForSwap[i].transform.position = t;
            }
            else
            {
                tempTilesForSwap[i].transform.position = tempTilesForSwap[i + 1].transform.position;
            }
            
        }
    }
    void IsMixedTilesOrdered()
    {
        bool isWin = true;
        for (int i = 0; i < _heightArray; i++)
        {
            for (int j = 0; j < _widthArray; j++)
            {
                //if (tiles[i, j].gameObject.name != snuffledTiles[i, j].gameObject.name) return false;

                if (_tiles[i, j].transform.position != _orderedTilesPosition[i, j])
                {
                    isWin = false;
                    break;
                }
            }
        }
        if(isWin)
        {
            EventBus.onWin?.Invoke();
            EventBus.onLevelCompleted?.Invoke(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void CalculateIntermediateColors()
    {
        Color topLeftColor = _tilesRenderer[0, 0].color;
        Color topRightColor = _tilesRenderer[0, _tilesRenderer.GetLength(1) - 1].color;
        Color bottomLeftColor = _tilesRenderer[_tilesRenderer.GetLength(0) - 1, 0].color;
        Color bottomRightColor = _tilesRenderer[_tilesRenderer.GetLength(0) - 1, _tilesRenderer.GetLength(1) - 1].color;

        for (int i = 0; i < _tilesRenderer.GetLength(0); i++)
        {
            for (int j = 0; j < _tilesRenderer.GetLength(1); j++)
            {
                float tX = (float)i / (_tilesRenderer.GetLength(0) - 1);
                float tY = (float)j / (_tilesRenderer.GetLength(1) - 1);

                Color colorTop = Color.Lerp(topLeftColor, topRightColor, tY);
                Color colorBottom = Color.Lerp(bottomLeftColor, bottomRightColor, tY);
                Color finalColor = Color.Lerp(colorTop, colorBottom, tX);

                _tilesRenderer[i, j].color = finalColor;
            }
        }
    }
    void FillArrayTiles()
    {
        for(int i = 0; i < _heightArray; i++)
        {
            for(int j = 0 ; j < _widthArray; j++)
            {
                _tiles[i, j] = _tempArrTiles[_widthArray * i + j];
                _tilesRenderer[i, j] = _tiles[i, j].GetComponent<SpriteRenderer>();
                _orderedTilesPosition[i, j] = _tiles[i, j].transform.position;
            }
        }
    }
    void SortTilesByCoordinates()
    {
        _tempArrTiles = GameObject.FindGameObjectsWithTag("Tile");
        System.Array.Sort(_tempArrTiles, (a, b) =>
        {
            Vector3 posA = a.transform.position;
            Vector3 posB = b.transform.position;

            if (posA.y != posB.y)
            {
                return posA.y.CompareTo(posB.y);
            }
            else
            {
                return posA.x.CompareTo(posB.x);
            }
        });
    }
}

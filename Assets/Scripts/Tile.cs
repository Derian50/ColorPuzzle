using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    private SpriteRenderer _renderer;
    public bool isLock;
    private Vector2 _startPosition;
    private BoxCollider2D _collider2D;

    void Start()
    {

        _renderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<BoxCollider2D>();
        if (!isLock)
        {
            transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _collider2D.enabled = false;
        if (!isLock)
        {
            _startPosition = transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isLock)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        var results = new List<RaycastResult>();
        //graphicRaycaster.Raycast(eventData, results);
        if (!isLock)
        {
            transform.position = _startPosition;
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                TrySwapTiles(eventData.pointerCurrentRaycast.gameObject);
            }
            _collider2D.enabled = true;
        }

    }
    public void TrySwapTiles(GameObject swappedTile)
    {
        if (!swappedTile.GetComponent<Tile>().isLock)
        {
            this.transform.position = swappedTile.transform.position;
            swappedTile.transform.position = new Vector3(swappedTile.transform.position.x, swappedTile.transform.position.y, swappedTile.transform.position.z - 1);
            swappedTile.GetComponent<Collider2D>().enabled = false;
            swappedTile.transform.DOMove(_startPosition, 0.5f);
            StartCoroutine(TileIsSwapped(swappedTile));
        }
    }
    IEnumerator TileIsSwapped(GameObject swappedTile)
    {
        yield return new WaitForSeconds(0.6f);
        swappedTile.GetComponent<Collider2D>().enabled = true;
        EventBus.onSwapTiles?.Invoke();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] float offset;
    [SerializeField] GameObject prefab;
    private Vector3 positionCreation;
    private Vector3 sizePrefab;
    [SerializeField] GameObject player1Checker;
    [SerializeField] GameObject player2Checker;
    private bool isPlayer1Turn;
    private GameObject checkerToPlace;
    private int[,] virtualBoard;

    void Awake()
    {
        isPlayer1Turn = true;
        virtualBoard = new int[y, x];
        createBoard();
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            PlaceChecker();
        }
    }
    private void createBoard()
    {
        sizePrefab = prefab.GetComponent<Renderer>().bounds.size;
        positionCreation = Vector3.zero;
        List<GameObject> row = new List<GameObject>();
        
        //cree la premier rangé
        for (int i = x, j=0; i > 0; i--, j++)
        {
            GameObject newPreFab = Instantiate(prefab, positionCreation ,Quaternion.identity);
            Slot slot = newPreFab.GetComponent<Slot>();
            slot.x = 0;
            slot.y = j;
            newPreFab.transform.SetParent(transform);
            positionCreation.x += sizePrefab.x + offset;
            row.Add(newPreFab);
        }
        /* foreach (GameObject slot in row)
        {
            positionCreation = slot.transform.position;
            for (int i = y-1; i > 0; i--)
            {
                positionCreation.z += sizePrefab.z + offset;
                GameObject newPreFab = Instantiate(prefab, positionCreation ,Quaternion.identity);
                newPreFab.transform.SetParent(transform);
            }
        } */
        for (int i = 0; i < row.Count; i++)
        {
            positionCreation = row[i].transform.position;
            for (int j = y-1, k = 1; j > 0; j--, k++)
            {
                positionCreation.z += sizePrefab.z + offset;
                GameObject newPreFab = Instantiate(prefab, positionCreation ,Quaternion.identity);
                Slot slot = newPreFab.GetComponent<Slot>();
                slot.x = i;
                slot.y = k;
                newPreFab.transform.SetParent(transform);
            }
            
        }
        float boardWidth = ((x-1) * sizePrefab.x) + ((x -1) * offset);
        float boardHeigth = ((y-1) * sizePrefab.z) + ((y -1) * offset);
        Vector3 boardCenter = new Vector3(boardWidth/2, 0f, boardHeigth/2);
        
        foreach (Transform child in transform)
        {
            child.localPosition -= boardCenter;
        }
    }

    private void PlaceChecker()
    {
        checkerToPlace = isPlayer1Turn ? player1Checker : player2Checker;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction*100, Color.green);
        if(Physics.Raycast(ray,out hit))
        {
            Transform slotTarget = hit.collider.gameObject.transform;
            Debug.Log(hit.collider.gameObject.tag);
            if (slotTarget.CompareTag("Slot") && slotTarget.childCount == 0)
            {
                GameObject newCheckerOnBoard = Instantiate(checkerToPlace, slotTarget.position ,Quaternion.identity);
                newCheckerOnBoard.transform.SetParent(slotTarget);
                isPlayer1Turn = !isPlayer1Turn;
            }
        }
    }
}






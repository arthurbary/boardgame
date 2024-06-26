using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] GameObject player1Checker;
    [SerializeField] GameObject player2Checker;
    private bool isPlayer1Turn;
    private GameObject checkerToPlace;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer1Turn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            PlaceChecker();
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
            if (slotTarget.CompareTag("Slot") && slotTarget.childCount == 0)
            {
                Slot slot = slotTarget.GetComponent<Slot>();
                Debug.Log("x: " + slot.x + "y:" + slot.y);
                GameObject newCheckerOnBoard = Instantiate(checkerToPlace, slotTarget.position ,Quaternion.identity);
                newCheckerOnBoard.transform.SetParent(slotTarget);
                isPlayer1Turn = !isPlayer1Turn;
            }
        }
    }
}

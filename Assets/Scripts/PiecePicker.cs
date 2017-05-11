using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using GGL;

public class PiecePicker : MonoBehaviour
{
    public float pieceHeight = 5f;
    public float rayDistance = 1000f;
    public LayerMask selectionIgnoreLayer;

    private Piece selectedPiece;
    private CheckerBoard board;
    // Use this for initialization
    void Start()
    {
        board = FindObjectOfType<CheckerBoard>();
        if (board == null)
        {
            Debug.LogError("there is no CheckerBoard in the Scene!");

        }
    }

    void CheckSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GizmosGL.color = Color.red;
        GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0))
        {
            
            if(Physics.Raycast(ray,out hit, rayDistance))
            {
                selectedPiece = hit.collider.GetComponent<Piece>();

                if(selectedPiece == null)
                {
                    Debug.Log("Cannot pick up boject:" + hit.collider.name);
                }
            }
        }
    }
    void MoveSelection()
    {
        if(selectedPiece !=null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GizmosGL.color = Color.yellow;
            GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDistance);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, rayDistance, ~selectionIgnoreLayer))
            {
                GizmosGL.color = Color.blue;
                GizmosGL.AddSphere(hit.point, 0.5f);

                Vector3 piecePos = hit.point + Vector3.up * pieceHeight;
                selectedPiece.transform.position = piecePos;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckSelection();
    }
}

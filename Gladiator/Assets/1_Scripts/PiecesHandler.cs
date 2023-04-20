using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PiecesHandler : MonoBehaviour
{
    [Header("Explosion Control Variables")]
    public float explosionForce = 10.0f;
    public float explosionRadius = 5.0f;
    private Vector3 explosionPosition;

    [Header("References")]
    [SerializeField] private List<Vector3> piecePositions;
    [SerializeField] private List<Vector3> pieceRotations;
    [SerializeField] private List<Rigidbody> rbs;
    private EnemyManager _enemyManager;

    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }

    public void StartExplode(Transform pointOfExplosion)
    {
        explosionPosition = pointOfExplosion.position;
        Explode();
    }

    private void Explode()
    {
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
        StartCoroutine(ReturnThis());
    }

    IEnumerator ReturnThis()
    {
        yield return new WaitForSeconds(3);
        NormalState();
        _enemyManager.ReturnDeadEnemy(this);
    }

    private void NormalState()
    {
        int counter = 0;
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        foreach (Transform child in transform)
        {
            child.transform.position = piecePositions[counter];
            child.transform.eulerAngles = piecePositions[counter];
            counter++;
        }
    }

    #if UNITY_EDITOR

    [CustomEditor(typeof(PiecesHandler))]
    public class PiecesHandlerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PiecesHandler piecesHandler = (PiecesHandler)target;
            if(GUILayout.Button("Set Up Base Position", GUILayout.Height(20)))
            {
                SetUpNewPiecesPoisitons(piecesHandler);
            }
        }

        private void SetUpNewPiecesPoisitons(PiecesHandler piecesHandler)
        {
            piecesHandler.piecePositions = new List<Vector3>();
            piecesHandler.pieceRotations = new List<Vector3>();
            foreach (Transform child in piecesHandler.transform)
            {
                piecesHandler.pieceRotations.Add(child.eulerAngles);
                piecesHandler.piecePositions.Add(child.position);
                piecesHandler.rbs.Add(child.GetComponent<Rigidbody>());
            }
        }
    }

    #endif
}

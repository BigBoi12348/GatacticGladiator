using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PiecesHandler : MonoBehaviour
{
    [SerializeField] private Material _litUpmat;
    [SerializeField] private Material _shaderMat;
    [SerializeField] private LayerMask _piecesLayer;
    private Material _usedShaderMat;

    [Header("Explosion Control Variables")]
    public float explosionForce = 10.0f;
    public float explosionRadius;
    private Vector3 explosionPosition;

    [Header("References")]
    [SerializeField] private List<Vector3> piecePositions;
    [SerializeField] private List<Vector3> pieceRotations;
    [SerializeField] private List<Rigidbody> rbs;
    private EnemyManager _enemyManager;
    bool _type;
    bool _AmIOptimised;

    public void Init(EnemyManager enemyManager, bool less, bool optimised)
    {
        _enemyManager = enemyManager;
        _type = less;
        _AmIOptimised = optimised;
    }

    public void StartExplode(Vector3 pointOfExplosion)
    {
        explosionPosition = pointOfExplosion;
        _usedShaderMat = new Material(_shaderMat);
        foreach (Transform child in transform)
        {
            child.GetComponent<MeshRenderer>().material = _usedShaderMat;
        }
        Explode();
    }

    private void Explode()
    {   
        if(!_AmIOptimised)
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(explosionPosition, 0.08f, Vector3.up, 1, _piecesLayer);
            foreach (var ray in raycastHits)
            {
                ray.transform.GetComponent<MeshRenderer>().material = _litUpmat;
            }
        }
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0, ForceMode.Impulse);
        }
        StartCoroutine(ReturnThis());
    }

    IEnumerator ReturnThis()
    {
        yield return new WaitForSeconds(3);
        float alphaValue = 0;
        while (_usedShaderMat.GetFloat("_Alpha") < 1)
        {
            _usedShaderMat.SetFloat("_Alpha", alphaValue);
            alphaValue += Time.deltaTime;
            yield return null;
        }
        NormalState();
        _enemyManager.ReturnDeadEnemy(this, _type);
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
            piecesHandler.rbs = new List<Rigidbody>();
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

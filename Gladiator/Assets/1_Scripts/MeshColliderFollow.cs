using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderFollow : MonoBehaviour
{
    public SkinnedMeshRenderer _Arm_L_skinnedMesh;
    public MeshCollider _Arm_L_meshCollider;
    public SkinnedMeshRenderer _Arm_R_skinnedMesh;
    public MeshCollider _Arm_R_meshCollider;
    public SkinnedMeshRenderer _ForeArm_L_skinnedMesh;
    public MeshCollider _ForeArm_L_meshCollider;
    public SkinnedMeshRenderer _ForeArm_R_skinnedMesh;
    public MeshCollider _ForeArm_R_meshCollider;
    public SkinnedMeshRenderer _Calf_L_skinnedMesh;
    public MeshCollider _Calf_L_meshCollider;
    public SkinnedMeshRenderer _Calf_R_skinnedMesh;
    public MeshCollider _Calf_R_meshCollider;
    public SkinnedMeshRenderer _Torso_skinnedMesh;
    public MeshCollider _Torso_meshCollider;
    public SkinnedMeshRenderer _Head_skinnedMesh;
    public MeshCollider _Head_meshCollider;
    public SkinnedMeshRenderer _Legs_skinnedMesh;
    public MeshCollider _Legs_meshCollider;
    Mesh bakedMesh;
    private float delayTimer;
    private float delayTotalTime = 0.2f;
    private void Start() 
    {
        bakedMesh = new Mesh();
    }

    void Update()
    {
        if(delayTimer < 0)
        {
            if(_Arm_L_skinnedMesh != null)
            {
                _Arm_L_skinnedMesh.BakeMesh(bakedMesh);

                _Arm_L_meshCollider.sharedMesh = null;
                _Arm_L_meshCollider.sharedMesh = bakedMesh;
            }
            
            if(_Arm_R_skinnedMesh != null)
            {
                _Arm_R_skinnedMesh.BakeMesh(bakedMesh);

                _Arm_R_meshCollider.sharedMesh = null;
                _Arm_R_meshCollider.sharedMesh = bakedMesh;
            }
            
            if(_ForeArm_L_skinnedMesh != null)
            {
                _ForeArm_L_skinnedMesh.BakeMesh(bakedMesh);

                _ForeArm_L_meshCollider.sharedMesh = null;
                _ForeArm_L_meshCollider.sharedMesh = bakedMesh;
            }

            if(_ForeArm_R_skinnedMesh != null)
            {
                _ForeArm_R_skinnedMesh.BakeMesh(bakedMesh);

                _ForeArm_R_meshCollider.sharedMesh = null;
                _ForeArm_R_meshCollider.sharedMesh = bakedMesh;
            }
            
            if(_Calf_L_skinnedMesh != null)
            {
                _Calf_L_skinnedMesh.BakeMesh(bakedMesh);

                _Calf_L_meshCollider.sharedMesh = null;
                _Calf_L_meshCollider.sharedMesh = bakedMesh;
            }

            if(_Calf_R_skinnedMesh != null)
            {
                _Calf_R_skinnedMesh.BakeMesh(bakedMesh);

                _Calf_R_meshCollider.sharedMesh = null;
                _Calf_R_meshCollider.sharedMesh = bakedMesh;
            }

            if(_Torso_skinnedMesh != null)
            {
                _Torso_skinnedMesh.BakeMesh(bakedMesh);

                _Torso_meshCollider.sharedMesh = null;
                _Torso_meshCollider.sharedMesh = bakedMesh;
            }

            if(_Head_skinnedMesh != null)
            {
                _Head_skinnedMesh.BakeMesh(bakedMesh);

                _Head_meshCollider.sharedMesh = null;
                _Head_meshCollider.sharedMesh = bakedMesh;
            }
            
            if(_Legs_skinnedMesh != null)
            {
                _Legs_skinnedMesh.BakeMesh(bakedMesh);

                _Legs_meshCollider.sharedMesh = null;
                _Legs_meshCollider.sharedMesh = bakedMesh;
            }
            
            delayTimer = delayTotalTime;
        }
        delayTimer -= Time.deltaTime;
    }
}

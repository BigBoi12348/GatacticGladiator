using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Slice : MonoBehaviour
{
    public SliceOptions sliceOptions;
    public CallbackOptions callbackOptions;

    /// <summary>
    /// </summary>
    private int currentSliceCount;

    /// <summary>
    /// </summary>
    private GameObject fragmentRoot;

    /// <summary>
    /// </summary>
    /// <param name="sliceNormalWorld">The cut plane normal vector in world coordinates.</param>
    /// <param name="sliceOriginWorld">The cut plane origin in world coordinates.</param>
    public void ComputeSlice(Vector3 sliceNormalWorld, Vector3 sliceOriginWorld)
    {
        var mesh = this.GetComponent<MeshFilter>().sharedMesh;

        if (mesh != null)
        {
            if (this.fragmentRoot == null)
            {
                this.fragmentRoot = new GameObject($"{this.name}Slices");
                this.fragmentRoot.transform.SetParent(this.transform.parent);

                this.fragmentRoot.transform.position = this.transform.position;
                this.fragmentRoot.transform.rotation = this.transform.rotation;
                this.fragmentRoot.transform.localScale = Vector3.one;
            }
            
            var sliceTemplate = CreateSliceTemplate();
            var sliceNormalLocal = this.transform.InverseTransformDirection(sliceNormalWorld);
            var sliceOriginLocal = this.transform.InverseTransformPoint(sliceOriginWorld);

            Fragmenter.Slice(this.gameObject,
                             sliceNormalLocal,
                             sliceOriginLocal,
                             this.sliceOptions,
                             sliceTemplate,
                             this.fragmentRoot.transform);
                    
           
            GameObject.Destroy(sliceTemplate);

            this.gameObject.SetActive(false);

            if (callbackOptions.onCompleted != null)
            {
                callbackOptions.onCompleted.Invoke();
            }
        }
    }
    
    /// <summary>
    /// </summary>
    /// <returns></returns>
    private GameObject CreateSliceTemplate()
    {
        GameObject obj = new GameObject();
        obj.name = "Slice";
        obj.tag = this.tag;

        obj.AddComponent<MeshFilter>();

        var meshRenderer = obj.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterials = new Material[2] {
            this.GetComponent<MeshRenderer>().sharedMaterial,
            this.sliceOptions.insideMaterial
        };

        var thisCollider = this.GetComponent<Collider>();
        var fragmentCollider = obj.AddComponent<MeshCollider>();
        fragmentCollider.convex = true;
        fragmentCollider.sharedMaterial = thisCollider.sharedMaterial;
        fragmentCollider.isTrigger = thisCollider.isTrigger;
        
        var thisRigidBody = this.GetComponent<Rigidbody>();
        var fragmentRigidBody = obj.AddComponent<Rigidbody>();
        fragmentRigidBody.velocity = thisRigidBody.velocity;
        fragmentRigidBody.angularVelocity = thisRigidBody.angularVelocity;
        fragmentRigidBody.drag = thisRigidBody.drag;
        fragmentRigidBody.angularDrag = thisRigidBody.angularDrag;
        fragmentRigidBody.useGravity = thisRigidBody.useGravity;
    
        if (this.sliceOptions.enableReslicing &&
           (this.currentSliceCount < this.sliceOptions.maxResliceCount))
        {
            CopySliceComponent(obj);
        }

        return obj;
    }
    
    /// <summary>
    /// </summary>
    /// <param name="obj">The GameObject to copy this component to</param>
    private void CopySliceComponent(GameObject obj)
    {
        var sliceComponent = obj.AddComponent<Slice>();

        sliceComponent.sliceOptions = this.sliceOptions;
        sliceComponent.callbackOptions = this.callbackOptions;
        sliceComponent.currentSliceCount = this.currentSliceCount + 1;
        sliceComponent.fragmentRoot = this.fragmentRoot;
    }
}
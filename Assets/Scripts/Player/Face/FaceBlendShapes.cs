using UnityEngine;

public enum MuscleSide
{
    Right,
    Left
}

public class FaceBlendShapes : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;


    #region BlendShapes
    public void MoveBrowInner(MuscleSide side, Vector2 direction)
    {
        if (side == MuscleSide.Right)
        {
            meshRenderer.SetBlendShapeWeight(0, direction.y);
            meshRenderer.SetBlendShapeWeight(1, direction.x);
        }
        else
        {
            meshRenderer.SetBlendShapeWeight(3, direction.y);
            meshRenderer.SetBlendShapeWeight(4, direction.x * -1);
        }
    }
    public void MoveBrowOuter(MuscleSide side, Vector2 direction)
    {
        if (side == MuscleSide.Right)
        {
            meshRenderer.SetBlendShapeWeight(2, direction.y);
        }
        else
        {
            meshRenderer.SetBlendShapeWeight(5, direction.y);
        }
    }
    public void MoveMouth(MuscleSide side, Vector2 direction)
    {
        if (side == MuscleSide.Right)
        {
            meshRenderer.SetBlendShapeWeight(6, direction.y);
            meshRenderer.SetBlendShapeWeight(7, direction.x);
        }
        else
        {
            meshRenderer.SetBlendShapeWeight(8, direction.y);
            meshRenderer.SetBlendShapeWeight(9, direction.x * -1);
        }
    }
    public void MoveEyesAndEyelids(MuscleSide side, Vector2 direction)
    {
        if (side == MuscleSide.Right)
        {
            meshRenderer.SetBlendShapeWeight(12, direction.y);
            meshRenderer.SetBlendShapeWeight(10, direction.x * -1);
        }
        else
        {
            meshRenderer.SetBlendShapeWeight(13, direction.y); 
            meshRenderer.SetBlendShapeWeight(11, direction.x);
        }
    }
    public void OpenMouth(float isActive)
    {
        meshRenderer.SetBlendShapeWeight(14, isActive * 100);
    }
    public void Shrug(float isActive)
    {
        meshRenderer.SetBlendShapeWeight(15, isActive * 100);
    }
    public void Nod(float isActive)
    {
        meshRenderer.SetBlendShapeWeight(16, isActive * 100);
    }
    public void Headshake(float isActive)
    {
        meshRenderer.SetBlendShapeWeight(17, isActive * 100);
    }
    #endregion
}

using Improbable.Gdk.Subscriptions;
using Cubes;
using UnityEngine;

namespace Game
{
    [WorkerType(WorkerUtils.UnityClient)]
    public class CubeClientVisibility : MonoBehaviour
    {
        [Require] private CubeReader cubeReader;

        private MeshRenderer cubeMeshRenderer;

        private void OnEnable()
        {
            cubeMeshRenderer = GetComponentInChildren<MeshRenderer>();
            cubeReader.OnUpdate += cubeComponentUpdated;
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            cubeMeshRenderer.enabled = true; //cubeReader.Data.IsActive;
        }

        private void cubeComponentUpdated(Cube.Update update)
        {
            UpdateVisibility();
        }
    }
}


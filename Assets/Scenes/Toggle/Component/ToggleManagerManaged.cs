using TMPro;
using Unity.Entities;
using UnityEngine;

namespace Toggle
{
    public class ToggleManagerManaged : IComponentData
    {
        public GameObject cube;
        public TMP_Text text;
        public UnityEngine.UI.Toggle toggle;
    }
}
using System;
using UnityEngine;

namespace Game.Shaders.PaletteSwap
{
    [ExecuteInEditMode]
    public class PaletteSwapper : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private Texture2D _palette;
        [SerializeField] private int _index = 1;

        [SerializeField] private Vector3 _position;
        [SerializeField] private float _radius;


        private Matrix4x4 _currentPalette;
        private static readonly int ColorMatrix = Shader.PropertyToID("_ColorMatrix");

        private Vector4 ColorToVec4(Color color)
        {
            return new Vector4(color.r, color.g, color.b, color.a);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (_material == null) return;

            _currentPalette = new Matrix4x4();

            _currentPalette.SetRow(0, ColorToVec4(_palette.GetPixel(0, _index)));
            _currentPalette.SetRow(1, ColorToVec4(_palette.GetPixel(1, _index)));
            _currentPalette.SetRow(2, ColorToVec4(_palette.GetPixel(2, _index)));
            _currentPalette.SetRow(3, ColorToVec4(_palette.GetPixel(3, _index)));

            _material.SetMatrix(ColorMatrix, _currentPalette);
            
            _material.SetFloat("_Radius", _radius);
            _material.SetVector("_Position", _position);

            Graphics.Blit(src, dest, _material);
        }

        private void Start()
        {
        }
    }
}
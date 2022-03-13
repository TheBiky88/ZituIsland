using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Gameplay;
using Unity.FPS.Game;

namespace nickCode
{
    public enum Orientation
    {
        A,
        B,
        C
    }

    public class Pillar : MonoBehaviour, IInteractable 
    {
        [SerializeField] private float rotationTime;
        private float changeTime;
        [SerializeField] private Vector3 rotationToAdd;
        private Quaternion currentRotation;
        private Quaternion targetRotation;

        [SerializeField] private Orientation orientation;
        public Orientation Orientation => orientation;
        
        public InteractableType InteractType { get { return InteractableType.PicturePuzzle; } }

        private bool isSolved = false;
        private bool isRotating = false;

        private void Awake()
        {
            if (orientation == Orientation.B)
            {
                transform.rotation *= Quaternion.Euler(rotationToAdd);
            }
            else if (orientation == Orientation.C)
            {
                transform.rotation *= Quaternion.Euler(rotationToAdd * 2);
            }
        }

        public void Interact(GameObject user, MouseClick mouseClick)
        {
            if (!isSolved && !isRotating && mouseClick == MouseClick.Left)
            {
                isRotating = true;

                ChangeOrientation();
                
                changeTime = Time.time;
                currentRotation = transform.rotation;
                targetRotation = currentRotation * Quaternion.Euler(rotationToAdd);

                PillarManager.instance.OnPillarChanged();
            }
        }

        public void Solve()
        {
            isSolved = true;
        }

        private void ChangeOrientation()
        {
            if (orientation == Orientation.A)
            {
                orientation = Orientation.B;
            }

            else if (orientation == Orientation.B)
            {
                orientation = Orientation.C;
            }

            else if (orientation == Orientation.C)
            {
                orientation = Orientation.A;
            }
        }

        private void Update()
        {
            if (isRotating)
            {
                float progress = (Time.time - changeTime) / rotationTime;
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Mathf.Clamp01(progress));

                if (progress >= 1)
                {
                    isRotating = false;
                }
            }
        }
    }
}
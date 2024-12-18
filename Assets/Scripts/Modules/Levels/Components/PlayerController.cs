using System;
using UnityEngine;

namespace Components
{
    public class PlayerController:MonoBehaviour
    {
        public float moveSpeed = 2.5f;
        public Animator anim;
        private void Start()
        {
            GameManager.CameraManager.SetPos(transform.position);
        }

        private void Update()
        {
            float inputDirection = Input.GetAxis("Horizontal");
            if (inputDirection == 0)
            {
                anim.Play("idle");
            }
            else
            {
                if (inputDirection > 0 && transform.localScale.x < 0 || inputDirection < 0 && transform.localScale.x > 0)
                {
                    Flip();
                }
                transform.Translate(inputDirection*moveSpeed*Time.deltaTime*Vector3.right);
                //to set the range of the camera's movement (-32, 24)
                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(pos.x, -32, 24);
                GameManager.CameraManager.SetPos(pos);
                
                anim.Play("move");
            }
        }

        public void Flip()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
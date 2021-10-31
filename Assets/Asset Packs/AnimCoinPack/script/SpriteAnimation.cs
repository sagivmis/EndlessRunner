﻿using UnityEngine;

namespace RainbowArt
{
    public class SpriteAnimation : MonoBehaviour
    {
        public int mCurFrameIndex = 0;

        public bool mIsLoop = true;
        public float mFps = 30f;
        public Sprite[] mSpriteFrames;

        SpriteRenderer mSpriteRenderer;
        float mSecondPreFrame = 1f/30f;
        float mCurFrameLeftTime = 0;


        void Awake()
        {
            mSpriteRenderer = GetComponent<SpriteRenderer>();
        }


        void Start()
        {
            Play();
        }

        public void Play()
        {
            if(mSpriteFrames == null || mSpriteFrames.Length == 0)
            {
                return;
            }
            ResetToBeginning();
        }


        public void ResetToBeginning()
        {
            mSecondPreFrame = 1f / mFps;
            mCurFrameIndex = 0;
            UpdateSprite();
        }

        


        void Update()
        {
            mSecondPreFrame = 1f / mFps;
            if (mSpriteFrames == null || mSpriteFrames.Length == 0)
            {
                this.enabled = false;
                return;
            }
            float dt = Time.deltaTime;
            mCurFrameLeftTime -= dt;
            if(mCurFrameLeftTime <= 0)
            {
                mCurFrameLeftTime = mSecondPreFrame;
                mCurFrameIndex++;
                if(mCurFrameIndex >= mSpriteFrames.Length)
                {
                    mCurFrameIndex = mIsLoop ? 0 : mSpriteFrames.Length;
                }
                UpdateSprite();
            }
        }

        void UpdateSprite()
        {
            if (mSpriteRenderer == null)
            {
                this.enabled = false;
                return;
            }
            if (mSpriteFrames == null || mSpriteFrames.Length == 0)
            {
                this.enabled = false;
                return;
            }
            if (mCurFrameIndex <0 || mCurFrameIndex >= mSpriteFrames.Length)
            {
                return;
            }
            mSpriteRenderer.sprite = mSpriteFrames[mCurFrameIndex];
        }
    }


}


using UnityEngine;
using System.Collections;


namespace yuyu
{
    public class AnimationManager : MonoBehaviour
    {
        /// <summary>
        /// 创建动画动画控制委托，后面创建播放动画方法，然后就调用就可以直接用委托的对象来调用
        /// 例：animationHandler = xxx， 
        /// </summary>
        public delegate void AnimationHandler();
        /// <summary>
        /// 创建动画对象
        /// </summary>
        Animation animation;
        /// <summary>
        /// 创建动画控制器单例
        /// </summary>
        public static AnimationManager instance;
        /// <summary>
        /// 创建各个动画，外面赋值
        /// </summary>
        public AnimationClip Ani0;
        public AnimationClip Ani1;
        public AnimationClip Ani2;
        public AnimationClip Ani3;
        public AnimationClip Ani4;
        public AnimationClip Ani5;
        public AnimationClip Ani6;
        public AnimationClip Ani7;
        public AnimationClip Ani8;
        /// <summary>
        /// 实例化委托
        /// </summary>
        public AnimationHandler animationHandler;

        // Use this for initialization
        void Start()
        {
            //初始化单例
            instance = this;
            //默认播放动画
            //animationHandler = state0;
            //拿到动画组件，挂载在本体上的
            animation = GetComponent<Animation>();
        }
        /// <summary>
        /// 播放各个动画
        /// </summary>
        #region
        public void state0()
        {
            animation.Play(Ani0.name);
        }
        public void state1()
        {
            animation.Play(Ani1.name);
        }
        public void state2()
        {
            animation.Play(Ani2.name);
        }
        public void state3()
        {
            animation.Play(Ani3.name);
        }
        public void state4()
        {
            animation.Play(Ani4.name);
        }
        public void state5()
        {
            animation.Play(Ani5.name);
        }
        public void state6()
        {
            animation.Play(Ani6.name);
        }
        public void state7()
        {
            animation.Play(Ani7.name);
        }
        public void state8()
        {
            animation.Play(Ani8.name);
        }




        #endregion
        // Update is called once per frame
        void Update()
        {
            if (animationHandler != null)
            {
                //在update里面实时判断委托播放动画
                animationHandler();
            }
        }
    }
}


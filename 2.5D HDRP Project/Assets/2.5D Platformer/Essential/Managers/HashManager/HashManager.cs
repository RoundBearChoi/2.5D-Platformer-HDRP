using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum MainParameterType
    {
        Move,
        Left,
        Right,
        Up,
        Down,
        Jump,
        ForceTransition,
        Grounded,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
        LockTransition,
    }

    public enum CameraTrigger
    {
        Default,
        Shake,
    }

    public enum AI_Transition
    {
        start_walking,
        jump_platform,
        fall_platform,
    }

    public enum AI_State_Name
    {
        SendPathfindingAgent,
        AI_Attack,
    }

    public enum Hit_Reaction_States
    {
        Head_Hit,
        Zombie_Death,
    }

    public enum Instant_Transition_States
    {
        Jump_Normal_Landing = 0,
        Jump_3m_Prep = 1,
        Hanging_Idle = 2,
        Idle = 4,

        Jump_Normal_Prep = 5,
        Jump_Running = 6,

        Running_Jump = 3,
        Run_Stop_InPlace = 7,

        AirCombo_Smash = 8,
    }

    public enum Ledge_Trigger_States
    {
        Jump_Running_Fall,

        // normal jump
        Jump_Normal,
        Heroic_Fall,

        // running jump
        Running_Jump,
        Running_Heroic_Fall,
        Jump_Running,

        Fall,
        WallSlide,
        WallJump,
    }

    public enum Camera_States
    {
        Default,
        Shake,
    }

    public class HashManager : Singleton<HashManager>
    {
        public int[] ArrMainParams = new int[HashTool.GetMaxValue(typeof(MainParameterType))];
        public int[] ArrCameraParams = new int[HashTool.GetMaxValue(typeof(CameraTrigger))];
        public int[] ArrAITransitionParams = new int[HashTool.GetMaxValue(typeof(AI_Transition))];
        public int[] ArrAIStateNames = new int[HashTool.GetMaxValue(typeof(AI_State_Name))];
        public int[] ArrInstantTransitionStates = new int[HashTool.GetMaxValue(typeof(Instant_Transition_States))];
        public int[] ArrLedgeTriggerStates = new int[HashTool.GetMaxValue(typeof(Ledge_Trigger_States))];

        public Dictionary<Hit_Reaction_States, int> DicHitReactionStates =
            new Dictionary<Hit_Reaction_States, int>();

        public Dictionary<Camera_States, int> DicCameraStates =
            new Dictionary<Camera_States, int>();

        private void Awake()
        {
            // animation transitions
            for (int i = 0; i < HashTool.GetMaxValue(typeof(MainParameterType)); i++)
            {
                ArrMainParams[i] = Animator.StringToHash(((MainParameterType)i).ToString());
            }

            // camera transitions
            for (int i = 0; i < HashTool.GetMaxValue(typeof(CameraTrigger)); i++)
            {
                ArrCameraParams[i] = Animator.StringToHash(((CameraTrigger)i).ToString());
            }

            // ai transitions
            for (int i = 0; i < HashTool.GetMaxValue(typeof(AI_Transition)); i++)
            {
                ArrAITransitionParams[i] = Animator.StringToHash(((AI_Transition)i).ToString());
            }

            // ai states
            for (int i = 0; i < HashTool.GetMaxValue(typeof(AI_State_Name)); i++)
            {
                ArrAIStateNames[i] = Animator.StringToHash(((AI_State_Name)i).ToString());
            }

            // hit reaction states
            Hit_Reaction_States[] arrHitReactionStates = System.Enum.GetValues(typeof(Hit_Reaction_States))
                as Hit_Reaction_States[];

            foreach(Hit_Reaction_States t in arrHitReactionStates)
            {
                DicHitReactionStates.Add(t, Animator.StringToHash(t.ToString()));
            }

            // instant transition states
            for (int i = 0; i < HashTool.GetMaxValue(typeof(Instant_Transition_States)); i++)
            {
                ArrInstantTransitionStates[i] = Animator.StringToHash(((Instant_Transition_States)i).ToString());
            }

            // ledge trigger states
            for (int i = 0; i < HashTool.GetMaxValue(typeof(Ledge_Trigger_States)); i++)
            {
                ArrLedgeTriggerStates[i] = Animator.StringToHash(((Ledge_Trigger_States)i).ToString());
            }

            // camera states
            Camera_States[] arrCameraStates = System.Enum.GetValues(typeof(Camera_States))
                as Camera_States[];

            foreach(Camera_States t in arrCameraStates)
            {
                DicCameraStates.Add(t, Animator.StringToHash(t.ToString()));
            }
        }
    }
}
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
        Jump_Normal_Landing,
        Jump_3m_Prep,
        Hanging_Idle,
        Running_Jump,
        Idle,
        Jump_Normal_Prep,
        Jump_Running,
        Run_Stop_InPlace,
        AirCombo_Smash,
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
        public int[] ArrMainParams = new int[HashTool.GetLength(typeof(MainParameterType))];
        public int[] ArrCameraParams = new int[HashTool.GetLength(typeof(CameraTrigger))];
        public int[] ArrAITransitionParams = new int[HashTool.GetLength(typeof(AI_Transition))];
        public int[] ArrAIStateNames = new int[HashTool.GetLength(typeof(AI_State_Name))];
        public int[] ArrInstantTransitionStates = new int[HashTool.GetLength(typeof(Instant_Transition_States))];
        public int[] ArrLedgeTriggerStates = new int[HashTool.GetLength(typeof(Ledge_Trigger_States))];

        public Dictionary<Hit_Reaction_States, int> DicHitReactionStates =
            new Dictionary<Hit_Reaction_States, int>();

        public Dictionary<Camera_States, int> DicCameraStates =
            new Dictionary<Camera_States, int>();

        private void Awake()
        {
            HashTool.AddNameHashToArray(typeof(MainParameterType), ArrMainParams);
            HashTool.AddNameHashToArray(typeof(CameraTrigger), ArrCameraParams);
            HashTool.AddNameHashToArray(typeof(AI_Transition), ArrAITransitionParams);
            HashTool.AddNameHashToArray(typeof(AI_State_Name), ArrAIStateNames);
            HashTool.AddNameHashToArray(typeof(Instant_Transition_States), ArrInstantTransitionStates);
            HashTool.AddNameHashToArray(typeof(Ledge_Trigger_States), ArrLedgeTriggerStates);

            // hit reaction states
            Hit_Reaction_States[] arrHitReactionStates = System.Enum.GetValues(typeof(Hit_Reaction_States))
                as Hit_Reaction_States[];

            foreach(Hit_Reaction_States t in arrHitReactionStates)
            {
                DicHitReactionStates.Add(t, Animator.StringToHash(t.ToString()));
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
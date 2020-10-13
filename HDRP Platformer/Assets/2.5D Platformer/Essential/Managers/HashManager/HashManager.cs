using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class HashManager : Singleton<HashManager>
    {
        HashInitializer hashInitializer = null;

        public void SetupHashInitializer()
        {
            if (hashInitializer == null)
            {
                hashInitializer = Instantiate(
                    Resources.Load("HashInitializer",
                    typeof(HashInitializer)))
                    as HashInitializer;

                hashInitializer.name = typeof(HashInitializer).ToString();
                hashInitializer.transform.position = Vector3.zero;
                hashInitializer.transform.rotation = Quaternion.identity;

                hashInitializer.InitAllHashKeys();
            }
        }

        public int[] ArrMainParams = new int[HashTool.GetLength(typeof(MainParameterType))];
        public int[] ArrCameraParams = new int[HashTool.GetLength(typeof(CameraTrigger))];
        public int[] ArrAITransitionParams = new int[HashTool.GetLength(typeof(AI_Transition))];
        public int[] ArrAIStateNames = new int[HashTool.GetLength(typeof(AI_State_Name))];
        public int[] ArrInstantTransitionStates = new int[HashTool.GetLength(typeof(Instant_Transition_States))];
        public int[] ArrLedgeTriggerStates = new int[HashTool.GetLength(typeof(Ledge_Trigger_States))];
        public int[] ArrMirrorParameters = new int[HashTool.GetLength(typeof(MirrorParameterType))];

        //sumo fighter
        //public int[] ArrNonMovingStates = new int[HashTool.GetLength(typeof(NonMovingStateNames))];
        //public int[] ArrWalkStates = new int[HashTool.GetLength(typeof(WalkStateNames))];
        //public int[] ArrRunStates = new int[HashTool.GetLength(typeof(RunStateNames))];
        //public int[] ArrCombo01States = new int[HashTool.GetLength(typeof(Combo01StateNames))];
        //public int[] ArrStandingJumpStates = new int[HashTool.GetLength(typeof(StandingJumpStateNames))];
        //public int[] ArrLedgeStates = new int[HashTool.GetLength(typeof(LedgeStateNames))];
        //public int[] ArrFallStates = new int[HashTool.GetLength(typeof(FallStateNames))];

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
            HashTool.AddNameHashToArray(typeof(MirrorParameterType), ArrMirrorParameters);

            // sumo fighter
            //HashTool.AddNameHashToArray(typeof(NonMovingStateNames), ArrNonMovingStates);
            //HashTool.AddNameHashToArray(typeof(WalkStateNames), ArrWalkStates);
            //HashTool.AddNameHashToArray(typeof(RunStateNames), ArrRunStates);
            //HashTool.AddNameHashToArray(typeof(Combo01StateNames), ArrCombo01States);
            //HashTool.AddNameHashToArray(typeof(StandingJumpStateNames), ArrStandingJumpStates);
            //HashTool.AddNameHashToArray(typeof(LedgeStateNames), ArrLedgeStates);
            //HashTool.AddNameHashToArray(typeof(FallStateNames), ArrFallStates);

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
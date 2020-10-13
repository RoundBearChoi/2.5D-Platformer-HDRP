namespace Roundbeargames
{
    public class DamageReaction : CharacterFunction
    {
        public override void RunFunction(AttackCondition info)
        {
            if (control.GetBool(typeof(CharacterDead)))
            {
                control.RunFunction(typeof(GetPushedAsRagdoll), info);
            }
            else
            {
                if (!control.GetBool(typeof(BlockedAttack), info))
                {
                    control.RunFunction(typeof(TakeDamage), info);
                }
            }
        }
    }
}
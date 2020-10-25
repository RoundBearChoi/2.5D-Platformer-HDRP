namespace Roundbeargames
{
    public class Dataset_Movement : DatasetBase
    {
        public override void InitDataset()
        {
            SetDefaultValues();
        }

        public override void SetDefaultValues()
        {
            arrFloats = new FloatData[(int)MovementData_Floats.COUNT];

            arrFloats[(int)MovementData_Floats.MOMENTUM] = new FloatData();

            arrFloats[(int)MovementData_Floats.MOMENTUM].name = MovementData_Floats.MOMENTUM.ToString();
            arrFloats[(int)MovementData_Floats.MOMENTUM].fValue = 0f;
        }

        public override float GetFloat(int dataIndex)
        {
            return arrFloats[dataIndex].fValue;
        }

        public override void SetFloat(int dataIndex, float value)
        {
            arrFloats[dataIndex].fValue = value;
        }
    }
}
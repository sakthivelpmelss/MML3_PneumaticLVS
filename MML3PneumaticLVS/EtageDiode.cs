using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    // Derived class
    public class EtageDiode : IStepable
    {
        private bool _bInverse;
        private bool _bAnodeInverse;
        private bool _bCathodeInverse;

        public EtageDiode(bool bInverse, int nPatteInverse)
        {
            _bInverse = bInverse;
            _bAnodeInverse = (1 & nPatteInverse) != 0;
            _bCathodeInverse = (2 & nPatteInverse) != 0;
        }

        public void Step(params Signal[] signals)
        {
            if (signals.Length != 4)
                throw new ArgumentException("EtageDiode expects exactly 4 signals.");

            Signal sAnode = signals[0];
            Signal sCathode = signals[1];
            Signal sForceMod = signals[2];
            Signal sForceVal = signals[3];

            // (Implementation should done here)
        }
    }
}

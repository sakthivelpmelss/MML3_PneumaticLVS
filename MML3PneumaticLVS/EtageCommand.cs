using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    public class EtageCommande : IStepable
    {
        private const int NB_MICRO_CYCLES = 50;

        private bool _bInverse;
        private bool _bPattePlusInverse;
        private bool _bPatteMoinsInverse;
        public int MicroCycles { get; private set; }

        public EtageCommande(bool bInverse, int nPatteInverse)
        {
            _bInverse = bInverse;
            _bPattePlusInverse = (1 & nPatteInverse) != 0;
            _bPatteMoinsInverse = (2 & nPatteInverse) != 0;
            MicroCycles = 0;
        }

        public void Step(params Signal[] signals)
        {
            if (signals.Length != 5)
                throw new ArgumentException("EtageCommande expects exactly 5 signals.");

            Signal sPattePlus = signals[0];
            Signal sPatteMoins = signals[1];
            Signal sCommande = signals[2];
            Signal sForceMod = signals[3];
            Signal sForceVal = signals[4];

            // (Implementation should done here)
        }
    }
}

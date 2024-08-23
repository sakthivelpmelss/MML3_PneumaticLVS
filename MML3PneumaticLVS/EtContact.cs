using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    public abstract class EtContact
    {
        public abstract void Step(Signal sNO, Signal sNC, Signal sCOMMUN, Signal sCOMMANDE);
    }
}

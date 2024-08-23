using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    public abstract class ControlValve
    {
        //public abstract void Step(Pipe pNO, Pipe pNC, Pipe pCOMMON, Signal sCOMMAND);
        public abstract void Step(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command);
    }
}

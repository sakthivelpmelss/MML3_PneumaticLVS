using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS.Components
{
    public class AuxiliaryReservoir : ControlValve
    {
        public override void Step(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command)
        {
            // AuxiliaryReservoir logic: Distribute pressure to pipe3 and pipe4
            pipe3.pressureValue = pipe2.pressureValue - pipe2.pressureReductionRate;
            pipe4.pressureValue = pipe2.pressureValue - pipe2.pressureReductionRate;
        }
    }
}

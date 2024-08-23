using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS.Components
{
    public class MainReservoir : ControlValve
    {
        public override void Step(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command)
        {
            // MainReservoir logic: Maintain or distribute pressure to pipe2
            pipe2.pressureValue = pipe1.pressureValue - pipe1.pressureReductionRate;
        }
    }
}

using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS.Components
{
    // FlowControlValve: Allows fluid to flow through the valve at a controlled rate
    public class FlowControlValve : ControlValve
    {
        public override void Step(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command)
        {
            // Calculate pressure for each pipe considering the leak
            pipe1.CalculatePressure();
            pipe2.CalculatePressure();
            pipe3.CalculatePressure();
            pipe4.CalculatePressure();

            if (command == 0)
            {
                // Transfer pressure from pipe1 to pipe2 with reduction
                pipe2.pressureValue = Math.Max(0, pipe1.pressureValue - pipe1.pressureReductionRate);
            }
            else if (command == 1)
            {
                // Transfer pressure from pipe3 to pipe4 with reduction
                pipe4.pressureValue = Math.Max(0, pipe3.pressureValue - pipe3.pressureReductionRate);
            }
        }
    }
}

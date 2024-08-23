using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS.Components
{
    // CheckValve: Allows fluid to flow in only one direction, blocks reverse flow
    public class CheckValve : ControlValve
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
                // Allow flow from pipe1 to pipe2, block reverse
                if (pipe1.pressureValue > pipe2.pressureValue)
                {
                    pipe2.pressureValue = pipe1.pressureValue;
                    pipe1.pressureValue -= pipe1.pressureReductionRate;
                }
            }
            else if (command == 1)
            {
                // Allow flow from pipe3 to pipe4, block reverse
                if (pipe3.pressureValue > pipe4.pressureValue)
                {
                    pipe4.pressureValue = pipe3.pressureValue;
                    pipe3.pressureValue -= pipe3.pressureReductionRate;
                }
            }
        }
    }
}

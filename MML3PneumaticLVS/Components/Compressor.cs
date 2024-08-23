using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS.Components
{
    public class Compressor : ControlValve
    {
        public override void Step(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4, int command)
        {
            // Compressor logic: Increase pressure in pipe1
            if (command == 1)
            {
                pipe1.pressureValue += 10;
            }
        }
    }
}

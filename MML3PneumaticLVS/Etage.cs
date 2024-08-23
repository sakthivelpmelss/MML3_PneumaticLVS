using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    // Abstract base class
    public abstract class Etage
    {
        // Abstract method to be implemented by derived classes
        public abstract void Step(Signal s1, Signal s2, Signal s3, Signal s4, Signal s5);
    }
}

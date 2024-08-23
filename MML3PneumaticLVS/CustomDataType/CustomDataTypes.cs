using Newtonsoft.Json;

namespace MML3PneumaticLVS.CustomDataType
{
    public class CustomDataTypes
    {


    }
    public enum PipeState
    {
        Leak = 0,
        Good = 1
    }
    public class Pipe : IEquatable<Pipe>
    {
        public PipeState State { get; set; }
        public PipeState PreState { get; set; }
        public float pressureCapacity { get; set; }
        public float pressureValue { get; set; }
        public float pressureReductionRate { get; set; }  // Pressure reduction rate
        public bool hasLeakage { get; private set; }      // Indicates if there's a leakage

        // Default constructor
        public Pipe(PipeState state, PipeState preState = 0)
        {
            pressureCapacity = 0;
            pressureValue = 0;
            State = state;
            PreState = preState;
            pressureReductionRate = 0;
            hasLeakage = false;  // Initialize with no leak
        }

        // Parameterized constructor
        public Pipe(float pressureCapacity, float pressureValue, float pressureReductionRate = 0)
        {
            this.pressureCapacity = pressureCapacity;
            this.pressureValue = pressureValue;
            this.pressureReductionRate = pressureReductionRate;
            hasLeakage = false;
        }

        // Copy constructor
        public Pipe(Pipe pipe)
        {
            pressureCapacity = pipe.pressureCapacity;
            pressureValue = pipe.pressureValue;
            pressureReductionRate = pipe.pressureReductionRate;
            hasLeakage = pipe.hasLeakage;
        }

        // Method to set the leak condition
        public void SetLeakage(bool leakage)
        {
            hasLeakage = leakage;
        }

        // Adjusts pressure considering leakage and reduction rate
        public void CalculatePressure()
        {
            if (hasLeakage)
            {
                pressureValue -= pressureReductionRate;
                if (pressureValue < 0)
                    pressureValue = 0;
            }
        }

        // Implementation of IEquatable<Pipe>.Equals
        public bool Equals(Pipe? other)
        {
            if (other == null)
                return false;
            return pressureCapacity == other.pressureCapacity && pressureValue == other.pressureValue;
        }

        // Override Object.Equals
        public override bool Equals(object obj)
        {
            return Equals(obj as Pipe);
        }

        // Override Object.GetHashCode
        public override int GetHashCode()
        {
            // Combine hash codes of all properties used in equality comparison
            return HashCode.Combine(pressureCapacity, pressureValue, pressureReductionRate);
        }

        // Override ToString method
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class Signal : IEquatable<Signal>
    {
        public int Etat { get; set; }
        public int EtatPrec { get; set; }
        public int VS_handle { get; set; } // Placeholder for additional properties
        public int VS_handleIn { get; set; }

        public int SignalCapacity { get; set; }
        public double VoltageValue { get; set; }
        public bool SignalStatus { get; set; }
        public Signal(int etat, int etatPrec = 0)
        {
            SignalStatus = false;
            VoltageValue = 0;
            SignalCapacity = 0;
            Etat = etat;
            EtatPrec = etatPrec;
            VS_handleIn = 0;
            VS_handle = 0;
        }

        public Signal(int signalCapacity, double voltageValue, bool signalStatus)
        {
            SignalCapacity = signalCapacity;
            VoltageValue = voltageValue;
            SignalStatus = signalStatus;
        }

        public Signal(Signal _other)
        {
            this.SignalCapacity = _other.SignalCapacity;
            this.SignalStatus = _other.SignalStatus;
            this.VoltageValue = _other.VoltageValue;
        }

        //This must be defined
        public bool Equals(Signal? other)
        {
            if (other == null)
                return false;
            return SignalCapacity == other.SignalCapacity && VoltageValue == other.VoltageValue && SignalStatus == other.SignalStatus;
        }


        //This must be defined
        public override bool Equals(object obj)
        {
            return Equals(obj as Signal);
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override int GetHashCode()
        {
            // Combine hash codes of all properties used in equality comparison
            return HashCode.Combine(SignalCapacity, VoltageValue, SignalStatus);
        }
    }
}

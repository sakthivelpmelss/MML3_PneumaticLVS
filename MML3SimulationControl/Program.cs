using MML3PneumaticLVS;
using MML3PneumaticLVS.Components;
using MML3PneumaticLVS.CustomDataType;

namespace MML3SimulationControl
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Initialize PipeDataCollections
            PipeDataCollections pipeCollection = new PipeDataCollections();

            // Create pipes
            pipeCollection.AddPipe("pipe1", new Pipe(100, 0, 2));  // Pipe from Compressor to MainReservoir
            pipeCollection.AddPipe("pipe2", new Pipe(100, 0, 1));  // Pipe from MainReservoir to AuxiliaryReservoir
            pipeCollection.AddPipe("pipe3", new Pipe(100, 0, 0.5f));  // Pipe from AuxiliaryReservoir to some other component
            pipeCollection.AddPipe("pipe4", new Pipe(100, 0, 0.5f));  // Another pipe from AuxiliaryReservoir to some other component

            // Create components
            Compressor compressor = new Compressor();
            MainReservoir mainReservoir = new MainReservoir();
            AuxiliaryReservoir auxiliaryReservoir = new AuxiliaryReservoir();
            PressureSwitch pressureSwitch = new PressureSwitch();
            FlowControlValve flowControlValve = new FlowControlValve();
            CheckValve checkValve = new CheckValve();

            // Register components with the controller
            ComponentController.RegisterComponent("Compressor", compressor.Step);
            ComponentController.RegisterComponent("MainReservoir", mainReservoir.Step);
            ComponentController.RegisterComponent("AuxiliaryReservoir", auxiliaryReservoir.Step);
            ComponentController.RegisterComponent("PressureSwitch", pressureSwitch.Step);
            ComponentController.RegisterComponent("FlowControlValve", flowControlValve.Step);
            ComponentController.RegisterComponent("CheckValve", checkValve.Step);

            // Simulate the operation
            Console.WriteLine("Initial Pressures:");
            PrintPipePressures(pipeCollection);

            // Step 1: Compressor increases pressure in pipe1
            //compressor.Step(pipeCollection.GetPipe("pipe1"), null, null, null, 1);

            // Step 1: Execute the step function for the Compressor with command 1
            ComponentController.ExecuteStep("Compressor",
                pipeCollection.GetPipe("pipe1"), null, null, null, 1);
            CheckPressureBalance(pipeCollection); // Check after each step
            Console.WriteLine("\nAfter Compressor Step (Command = 1):");
            PrintPipePressures(pipeCollection);


            // Step 2: MainReservoir distributes pressure to pipe2
            //mainReservoir.Step(pipeCollection.GetPipe("pipe1"), pipeCollection.GetPipe("pipe2"), null, null, 1);

            // Step 2: Execute the step function for the MainReservoir with command 1
            ComponentController.ExecuteStep("MainReservoir",
                pipeCollection.GetPipe("pipe1"), pipeCollection.GetPipe("pipe2"), null, null, 1);
            CheckPressureBalance(pipeCollection); // Check after each step
            Console.WriteLine("\nAfter MainReservoir Step (Command = 1):");
            PrintPipePressures(pipeCollection);


            // Step 3: AuxiliaryReservoir distributes pressure to pipe3 and pipe4
            //auxiliaryReservoir.Step(pipeCollection.GetPipe("pipe2"), pipeCollection.GetPipe("pipe3"), pipeCollection.GetPipe("pipe4"), null, 1);

            // Step 2: Execute the step function for the AuxiliaryReservoir with command 1
            ComponentController.ExecuteStep("AuxiliaryReservoir",
                null, pipeCollection.GetPipe("pipe2"), pipeCollection.GetPipe("pipe3"), pipeCollection.GetPipe("pipe4"), 1);
            CheckPressureBalance(pipeCollection); // Check after each step
            Console.WriteLine("\nAfter AuxiliaryReservoir Step (Command = 1):");
            PrintPipePressures(pipeCollection);


            // Enable leakage on pipe2 and pipe3
            pipeCollection.GetPipe("pipe2").SetLeakage(true);
            pipeCollection.GetPipe("pipe3").SetLeakage(true);


            // Step 4: Use PressureSwitch to decide pressure distribution
            //pressureSwitch.Step(pipeCollection.GetPipe("pipe1"), pipeCollection.GetPipe("pipe2"), pipeCollection.GetPipe("pipe3"), pipeCollection.GetPipe("pipe4"), 0);

            // Execute the step function for the PressureSwitch with command 0
            ComponentController.ExecuteStep("PressureSwitch",
                pipeCollection.GetPipe("pipe1"),
                pipeCollection.GetPipe("pipe2"),
                pipeCollection.GetPipe("pipe3"),
                pipeCollection.GetPipe("pipe4"),
                0);
            CheckPressureBalance(pipeCollection);
            Console.WriteLine("\nAfter PressureSwitch Step (Command = 0):");
            PrintPipePressures(pipeCollection);

            // Execute the step function for the FlowControlValve with command 1
            ComponentController.ExecuteStep("FlowControlValve",
                pipeCollection.GetPipe("pipe1"),
                pipeCollection.GetPipe("pipe2"),
                pipeCollection.GetPipe("pipe3"),
                pipeCollection.GetPipe("pipe4"),
                1);
            CheckPressureBalance(pipeCollection);
            Console.WriteLine("\nAfter FlowControlValve Step (Command = 1):");
            PrintPipePressures(pipeCollection);

            // Execute the step function for the CheckValve with command 1
            ComponentController.ExecuteStep("CheckValve",
                pipeCollection.GetPipe("pipe1"),
                pipeCollection.GetPipe("pipe2"),
                pipeCollection.GetPipe("pipe3"),
                pipeCollection.GetPipe("pipe4"),
                1);
            CheckPressureBalance(pipeCollection);
            Console.WriteLine("\nAfter CheckValve Step (Command = 1):");
            PrintPipePressures(pipeCollection);

            //// Print pressures after simulation steps
            //Console.WriteLine("\nAfter Simulation Steps:");
            //PrintPipePressures(pipeCollection);
        }
        public static void Main_07(string[] args)
        {
            // Initialize PipeDataCollections
            PipeDataCollections pipeCollection = new PipeDataCollections();

            // Create pipes and add them to the collection with specific pressure reduction rates
            pipeCollection.AddPipe("pipe1", new Pipe(100, 80, 0));  // High pressure, no reduction rate
            pipeCollection.AddPipe("pipe2", new Pipe(100, 60, 2));  // Moderate pressure, with a reduction rate
            pipeCollection.AddPipe("pipe3", new Pipe(100, 70, 3));  // Higher pressure, with a higher reduction rate
            pipeCollection.AddPipe("pipe4", new Pipe(100, 50, 1));  // Low pressure, with a low reduction rate

            // Modify the pressure reduction rate externally
            pipeCollection.GetPipe("pipe2").pressureReductionRate = 5;  // Increasing reduction rate for pipe2
            pipeCollection.GetPipe("pipe3").pressureReductionRate = 1;  // Decreasing reduction rate for pipe3

            // Enable leakage on pipe2 and pipe3
            pipeCollection.GetPipe("pipe2").SetLeakage(true);
            pipeCollection.GetPipe("pipe3").SetLeakage(true);

            // Create instances of different components
            PressureSwitch pressureSwitch = new PressureSwitch();
            FlowControlValve flowControlValve = new FlowControlValve();
            CheckValve checkValve = new CheckValve();

            // Register components with the controller
            ComponentController.RegisterComponent("PressureSwitch", pressureSwitch.Step);
            ComponentController.RegisterComponent("FlowControlValve", flowControlValve.Step);
            ComponentController.RegisterComponent("CheckValve", checkValve.Step);

            // Simulate the leakage scenario
            Console.WriteLine("Initial Pressures:");
            PrintPipePressures(pipeCollection);

            // Execute the step function for the PressureSwitch with command 0
            ComponentController.ExecuteStep("PressureSwitch",
                pipeCollection.GetPipe("pipe1"),
                pipeCollection.GetPipe("pipe2"),
                pipeCollection.GetPipe("pipe3"),
                pipeCollection.GetPipe("pipe4"),
                0);

            Console.WriteLine("\nAfter PressureSwitch Step (Command = 0):");
            PrintPipePressures(pipeCollection);

            // Execute the step function for the FlowControlValve with command 1
            ComponentController.ExecuteStep("FlowControlValve",
                pipeCollection.GetPipe("pipe1"),
                pipeCollection.GetPipe("pipe2"),
                pipeCollection.GetPipe("pipe3"),
                pipeCollection.GetPipe("pipe4"),
                1);

            Console.WriteLine("\nAfter FlowControlValve Step (Command = 1):");
            PrintPipePressures(pipeCollection);

            // Execute the step function for the CheckValve with command 1
            ComponentController.ExecuteStep("CheckValve",
                pipeCollection.GetPipe("pipe1"),
                pipeCollection.GetPipe("pipe2"),
                pipeCollection.GetPipe("pipe3"),
                pipeCollection.GetPipe("pipe4"),
                1);

            Console.WriteLine("\nAfter CheckValve Step (Command = 1):");
            PrintPipePressures(pipeCollection);
        }

        //verifies the pressure balance by checking that the pressure in the output pipes is consistent with the inputs
        public static void CheckPressureBalance(PipeDataCollections pipeCollection)
        {
            foreach (var pipeKey in PipeDataCollections.PipeCollections.Keys)
            {
                var pipe = pipeCollection.GetPipe(pipeKey);
                if (pipe.hasLeakage)
                {
                    // Check if pressure reduction due to leakage is within expected range
                    if (pipe.pressureValue < 0)
                    {
                        Console.WriteLine($"{pipeKey} has invalid pressure (below 0) due to leakage.");
                    }
                }

                // Assuming the pressure should not exceed its capacity even with leaks
                if (pipe.pressureValue > pipe.pressureCapacity)
                {
                    Console.WriteLine($"{pipeKey} has pressure exceeding its capacity.");
                }
            }
        }
        // Helper method to print the pressures of all pipes in the collection
        public static void PrintPipePressures(PipeDataCollections pipeCollection)
        {
            // Ensure PipeCollections is initialized before iterating
            if (PipeDataCollections.PipeCollections != null && PipeDataCollections.PipeCollections.Count > 0)
            {
                foreach (var pipeKey in PipeDataCollections.PipeCollections.Keys)
                {
                    var pipe = pipeCollection.GetPipe(pipeKey);
                    Console.WriteLine($"{pipeKey} Pressure: {pipe.pressureValue} (Leakage: {pipe.hasLeakage})");
                }
            }
            else
            {
                Console.WriteLine("PipeCollections is empty or not initialized.");
            }
        }

        public static void Main_06(string[] args)
        {
            // Create pipes with different pressure capacities and initial pressures
            Pipe pipe1 = new Pipe(100, 80, 5);
            Pipe pipe2 = new Pipe(100, 60, 3);
            Pipe pipe3 = new Pipe(100, 70, 4);
            Pipe pipe4 = new Pipe(100, 50, 2);

            // Enable leakage on pipe2 and pipe3
            pipe2.SetLeakage(true);
            pipe3.SetLeakage(true);

            // Create instances of different components
            PressureSwitch pressureSwitch = new PressureSwitch();
            FlowControlValve flowControlValve = new FlowControlValve();
            CheckValve checkValve = new CheckValve();

            // Register components with the controller
            ComponentController.RegisterComponent("PressureSwitch", pressureSwitch.Step);
            ComponentController.RegisterComponent("FlowControlValve", flowControlValve.Step);
            ComponentController.RegisterComponent("CheckValve", checkValve.Step);

            // Simulate the leakage scenario
            Console.WriteLine("Initial Pressures:");
            PrintPipePressures(pipe1, pipe2, pipe3, pipe4);

            // Execute the step function for the PressureSwitch with command 0
            ComponentController.ExecuteStep("PressureSwitch", pipe1, pipe2, pipe3, pipe4, 0);
            Console.WriteLine("\nAfter PressureSwitch Step (Command = 0):");
            PrintPipePressures(pipe1, pipe2, pipe3, pipe4);

            // Execute the step function for the FlowControlValve with command 1
            ComponentController.ExecuteStep("FlowControlValve", pipe1, pipe2, pipe3, pipe4, 1);
            Console.WriteLine("\nAfter FlowControlValve Step (Command = 1):");
            PrintPipePressures(pipe1, pipe2, pipe3, pipe4);

            // Execute the step function for the CheckValve with command 1
            ComponentController.ExecuteStep("CheckValve", pipe1, pipe2, pipe3, pipe4, 1);
            Console.WriteLine("\nAfter CheckValve Step (Command = 1):");
            PrintPipePressures(pipe1, pipe2, pipe3, pipe4);
        }

        // Helper method to print the pressures of the pipes
        public static void PrintPipePressures(Pipe pipe1, Pipe pipe2, Pipe pipe3, Pipe pipe4)
        {
            Console.WriteLine($"Pipe1 Pressure: {pipe1.pressureValue}");
            Console.WriteLine($"Pipe2 Pressure: {pipe2.pressureValue} (Leakage: {pipe2.hasLeakage})");
            Console.WriteLine($"Pipe3 Pressure: {pipe3.pressureValue} (Leakage: {pipe3.hasLeakage})");
            Console.WriteLine($"Pipe4 Pressure: {pipe4.pressureValue}");
        }
        public static void Main_05(string[] args)
        {
            // Create pipes
            Pipe pipe1 = new Pipe(100, 80, 5);
            Pipe pipe2 = new Pipe(100, 60, 3);
            Pipe pipe3 = new Pipe(100, 70, 4);
            Pipe pipe4 = new Pipe(100, 50, 2);

            // Create instances of different components
            PressureSwitch pressureSwitch = new PressureSwitch();
            FlowControlValve flowControlValve = new FlowControlValve();
            CheckValve checkValve = new CheckValve();

            // Register components with the controller
            ComponentController.RegisterComponent("PressureSwitch", pressureSwitch.Step);
            ComponentController.RegisterComponent("FlowControlValve", flowControlValve.Step);
            ComponentController.RegisterComponent("CheckValve", checkValve.Step);

            // Execute the step function for the PressureSwitch
            ComponentController.ExecuteStep("PressureSwitch", pipe1, pipe2, pipe3, pipe4, 0);

            // Output the pressure values after stepping
            Console.WriteLine("After PressureSwitch Step:");
            Console.WriteLine($"Pipe1 Pressure: {pipe1.pressureValue}");
            Console.WriteLine($"Pipe2 Pressure: {pipe2.pressureValue}");
            Console.WriteLine($"Pipe3 Pressure: {pipe3.pressureValue}");
            Console.WriteLine($"Pipe4 Pressure: {pipe4.pressureValue}");

            // Execute the step function for the FlowControlValve
            ComponentController.ExecuteStep("FlowControlValve", pipe1, pipe2, pipe3, pipe4, 1);

            // Output the pressure values after stepping
            Console.WriteLine("After FlowControlValve Step:");
            Console.WriteLine($"Pipe1 Pressure: {pipe1.pressureValue}");
            Console.WriteLine($"Pipe2 Pressure: {pipe2.pressureValue}");
            Console.WriteLine($"Pipe3 Pressure: {pipe3.pressureValue}");
            Console.WriteLine($"Pipe4 Pressure: {pipe4.pressureValue}");

            // Execute the step function for the CheckValve
            ComponentController.ExecuteStep("CheckValve", pipe1, pipe2, pipe3, pipe4, 1);

            // Output the pressure values after stepping
            Console.WriteLine("After CheckValve Step:");
            Console.WriteLine($"Pipe1 Pressure: {pipe1.pressureValue}");
            Console.WriteLine($"Pipe2 Pressure: {pipe2.pressureValue}");
            Console.WriteLine($"Pipe3 Pressure: {pipe3.pressureValue}");
            Console.WriteLine($"Pipe4 Pressure: {pipe4.pressureValue}");
        }

        public static void Main_04(string[] args)
        {
            // Create pipes
            Pipe pipe1 = new Pipe(100, 80, 5);
            Pipe pipe2 = new Pipe(100, 60, 3);
            Pipe pipe3 = new Pipe(100, 0, 0);
            Pipe pipe4 = new Pipe(100, 0, 0);

            // Create a PressureSwitch instance
            PressureSwitch pressureSwitch = new PressureSwitch();

            // Register the PressureSwitch component with the controller
            ComponentController.RegisterComponent("PressureSwitch", pressureSwitch.Step);

            // Execute the step function for the PressureSwitch
            ComponentController.ExecuteStep("PressureSwitch", pipe1, pipe2, pipe3, pipe4, 0);

            // Output the pressure values after stepping
            Console.WriteLine($"Pipe1 Pressure: {pipe1.pressureValue}");
            Console.WriteLine($"Pipe2 Pressure: {pipe2.pressureValue}");
            Console.WriteLine($"Pipe3 Pressure: {pipe3.pressureValue}");
            Console.WriteLine($"Pipe4 Pressure: {pipe4.pressureValue}");

            // Execute the step function again with a different command
            ComponentController.ExecuteStep("PressureSwitch", pipe1, pipe2, pipe3, pipe4, 1);

            // Output the pressure values after the second step
            Console.WriteLine($"Pipe1 Pressure: {pipe1.pressureValue}");
            Console.WriteLine($"Pipe2 Pressure: {pipe2.pressureValue}");
            Console.WriteLine($"Pipe3 Pressure: {pipe3.pressureValue}");
            Console.WriteLine($"Pipe4 Pressure: {pipe4.pressureValue}");
        }

        public static void Main_03()
        {
            // Create pipes with different pressures, capacities, and reduction rates
            Pipe pipe1 = new Pipe(100, 80, 5); // Capacity: 100, Pressure: 80, Reduction Rate: 5
            Pipe pipe2 = new Pipe(100, 60, 3); // Capacity: 100, Pressure: 60, Reduction Rate: 3

            // Simulate leakage in pipe1
            pipe1.SetLeakage(true);

            // Calculate pressures considering the leakages
            pipe1.CalculatePressure();
            pipe2.CalculatePressure();

            Console.WriteLine($"Pipe1 Pressure after calculation: {pipe1.pressureValue}"); // Expected to be 75
            Console.WriteLine($"Pipe2 Pressure after calculation: {pipe2.pressureValue}"); // Expected to be 57
        }
        public static void Main_02()
        {
            // Initialize pipes with different pressure values
            var pipe1 = new Pipe(100.0f, 50.0f);  // pipe1 has 50 pressure
            var pipe2 = new Pipe(150.0f, 75.0f);  // pipe2 has 75 pressure
            var pipe3 = new Pipe(200.0f, 25.0f);  // pipe3 has 25 pressure
            var pipe4 = new Pipe(250.0f, 10.0f);  // pipe4 has 10 pressure

            // Create an instance of the EtageDiode component
            var pressureSwitch = new PressureSwitch();
            Signal signalcmd = new Signal(0);
            // Execute the Step function with command 0
            //pressureSwitch.Step(pipe1, pipe2, pipe3, signalcmd);
            pressureSwitch.Step(pipe1, pipe2, pipe3, pipe4, 0);

            // After Step function with command 0:
            // pipe3 should have the maximum pressure of pipe1 and pipe2 (75)
            Console.WriteLine($"Pipe3 Pressure after command 0: {pipe3.pressureValue}");  // Output: 75
            signalcmd = new Signal(1);
            // Execute the Step function with command 1
            //pressureSwitch.Step(pipe1, pipe3, pipe4, signalcmd);
            pressureSwitch.Step(pipe1, pipe2, pipe3, pipe4, 1);

            // After Step function with command 1:
            // pipe4 should have the maximum pressure of pipe1 and pipe3 (75)
            Console.WriteLine($"Pipe4 Pressure after command 1: {pipe4.pressureValue}");  // Output: 75
        }

        static void Main_01(string[] args)
        {
            PipeDataCollections pipeDataCollections = new PipeDataCollections();

            // Creating and adding pipes to the collection
            var pipe1 = new Pipe(150.0f, 75.0f);
            pipeDataCollections.AddPipe("Pipe1", pipe1);

            var pipe2 = new Pipe(200.0f, 100.0f);
            pipeDataCollections.AddPipe("Pipe2", pipe2);

            // Retrieve and display a pipe
            var retrievedPipe = pipeDataCollections.GetPipe("Pipe1");
            Console.WriteLine(retrievedPipe); // Output: Pipe(PressureCapacity: 150, PressureValue: 75)

            // Update a pipe
            var newPipe = new Pipe(180.0f, 85.0f);
            pipeDataCollections.UpdatePipe("Pipe1", newPipe);
            Console.WriteLine(pipeDataCollections.GetPipe("Pipe1")); // Output: Pipe(PressureCapacity: 180, PressureValue: 85)

            // Remove a pipe
            pipeDataCollections.RemovePipe("Pipe2");
            Console.WriteLine(pipeDataCollections.ContainsKey("Pipe2")); // Output: False

            // Display the number of pipes
            Console.WriteLine(pipeDataCollections.CountPipes()); // Output: 1

            // Create signals
            Signal sNO = new Signal(0);
            Signal sNC = new Signal(1);
            Signal sCOMMUN = new Signal(1);
            Signal sCOMMANDE = new Signal(0);

            Signal sAnode = new Signal(1);
            Signal sCathode = new Signal(0);

            // Create instances of different components
            EtageContact etageContact = new EtageContact(bInverse: false, nPatteInverse: 3);
            EtageDiode etageDiode = new EtageDiode(bInverse: false, nPatteInverse: 3);
            EtageCommande etageCommande = new EtageCommande(bInverse: false, nPatteInverse: 3);

            // Print initial state for EtageContact
            Console.WriteLine("=== EtageContact ===");
            Console.WriteLine($"Before Step: NO={sNO.Etat}, NC={sNC.Etat}, COMMUN={sCOMMUN.Etat}, COMMANDE={sCOMMANDE.Etat}");
            etageContact.Step(sNO, sNC, sCOMMUN, sCOMMANDE);
            Console.WriteLine($"After Step: NO={sNO.Etat}, NC={sNC.Etat}, COMMUN={sCOMMUN.Etat}, COMMANDE={sCOMMANDE.Etat}");

            // Print initial state for EtageDiode
            Console.WriteLine("\n=== EtageDiode ===");
            Console.WriteLine($"Before Step: Anode={sAnode.Etat}, Cathode={sCathode.Etat}");
            etageDiode.Step(sAnode, sCathode, sCOMMUN, sCOMMANDE);
            Console.WriteLine($"After Step: Anode={sAnode.Etat}, Cathode={sCathode.Etat}");

            // Print initial state for EtageCommande
            Console.WriteLine("\n=== EtageCommande ===");
            Console.WriteLine($"Before Step: PattePlus={sNO.Etat}, PatteMoins={sNC.Etat}, COMMUN={sCOMMUN.Etat}, COMMANDE={sCOMMANDE.Etat}");
            etageCommande.Step(sNO, sNC, sCOMMUN, sCOMMANDE);
            Console.WriteLine($"After Step: PattePlus={sNO.Etat}, PatteMoins={sNC.Etat}, COMMUN={sCOMMUN.Etat}, COMMANDE={sCOMMANDE.Etat}");
        }
    }
}

using System;
using System.Collections.Generic;

namespace MathModelingSimulator.Models;

public partial class SimulatorTask
{
    public int Id { get; set; }

    public int IdSimulator { get; set; }

    public int[,] ZadanieMatrix { get; set; }

    public int Answer { get; set; }

    public virtual Simulator IdSimulatorNavigation { get; set; } = null!;
}

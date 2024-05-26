using System;
using System.Collections.Generic;

namespace MathModelingSimulator.Models;

public partial class Simulator
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Theory { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<SimulatorTask> SimulatorTasks { get; set; } = new List<SimulatorTask>();
}

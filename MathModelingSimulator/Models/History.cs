using System;
using System.Collections.Generic;

namespace MathModelingSimulator.Models;

public partial class History
{
    public int Id { get; set; }

    public Guid IdUser { get; set; }

    public int IdSimulator { get; set; }

    public bool Result { get; set; }

    public DateTime? PassageDateTime { get; set; }

    public virtual Simulator IdSimulatorNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}

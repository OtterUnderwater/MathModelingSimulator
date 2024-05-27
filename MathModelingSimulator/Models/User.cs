using System;
using System.Collections.Generic;

namespace MathModelingSimulator.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Telephone { get; set; }

    public string Email { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRole { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Role IdRoleNavigation { get; set; } = null!;
}

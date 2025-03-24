using System;
using System.Collections.Generic;

namespace wiga.Models;

public partial class StoreAddress
{
    public Guid Id { get; set; }

    public Guid StoreId { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string? Complement { get; set; }

    public string Neighborhood { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Store Store { get; set; } = null!;
}

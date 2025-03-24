using System;
using System.Collections.Generic;

namespace wiga.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public Guid StoreId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Store Store { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace wiga.Models;

public partial class Store
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid CompanyId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<StoreAddress> StoreAddresses { get; set; } = new List<StoreAddress>();
}

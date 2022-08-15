namespace Meli.Proxies.Interfaces;
using System;
using Meli.Entities;
public interface ICouponProxy
{
    Task<Product> ObtainProductAsync(string item); 
}


namespace DataFileGenerator.Models;

public sealed class CategoryProductRow
{
    public string? CategoryName { get; init; }  
    public bool CategoryIsActive { get; init; } 
    public int ProductCode { get; init; } 
    public string? ProductName { get; init; } 
    public decimal Price { get; init; } 
    public short Quantity { get; init; } 
    public bool ProductIsActive { get; init; } 
}
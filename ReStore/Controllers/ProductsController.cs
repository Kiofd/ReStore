using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Re_Store.Data;
using Re_Store.Entities;
using Re_Store.Extensions;
using Re_Store.RequestHelpers;

namespace Re_Store.Controllers;

public class ProductsController : BaseApiController
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
    {
        var query = _context.Products
            .Sort(productParams.OrderBy)
            .Search(productParams.SearchTerm)
            .Filter(productParams.Brands, productParams.Types)
            .AsQueryable();

        var products = await PagedList<Product>.ToPagedList(query,
            productParams.PageNumber, productParams.PageSize);
        
        Response.AddPaginationHeader(products.MetaData);        

       return products;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var products = await _context.Products.FindAsync(id);

        if (products is null) return NotFound();

        return products;
    }
    [HttpGet("filters")]
    public async Task<IActionResult> GetFilters()
    {
        var brands = await _context.Products.Select(b => b.Brand).Distinct().ToListAsync();
        var types  = await _context.Products.Select(b => b.Type).Distinct().ToListAsync();

        return Ok(new {brands, types});
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Re_Store.Data;
using Re_Store.Entities;

namespace Re_Store.Controllers;

public class BasketController : BaseApiController
{
    private readonly StoreContext _context;
    public BasketController(StoreContext context)
    {
        _context = context;
    }
    // GET
    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasket()
    {
        var basket = await RetrieveBasket();

        if (basket == null) return NotFound();

        return basket;
    }


    [HttpPost]
    public async Task<ActionResult<Basket>> AddItemToBasket(int productId, int quantity)
    {
        var basket = await RetrieveBasket();

        if (basket == null) basket == CreateBasket();
        //get basket
        //create basket
        //get product
        //add item
        //save changes
        return StatusCode(201);
    }


    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
    {
        //get basket
        //remove item or quantity
        //save changes
        return Ok();
    }
    
    private async Task<Basket> RetrieveBasket()
    {
        return await _context.Baskets
            .Include(i => i.Items)
            .ThenInclude(b => b.Product)
            .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
    }

    private Basket CreateBasket()
    {
        var buyerId = Guid.NewGuid().ToString();
    }
}

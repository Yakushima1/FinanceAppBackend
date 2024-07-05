using FinanceApp.Data;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FinanceApp.Dtos.Stock;
using FinanceApp.Helpers;
using FinanceApp.Interfaces;
using FinanceApp.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;
    
    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var stocks = await _stockRepository.GetAllStockAsync(query);
        var StockDto =  stocks.Select(x => x.ToStackDto()).ToList();
        return Ok(StockDto);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var stock = await _stockRepository.GetStockByIdAsync(id);
        return Ok(stock.ToStackDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var stock = stockDto.ToStockFromCreateDTO();
        await _stockRepository.CreateStockAsync(stock);
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStackDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, UpdateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var stock = await _stockRepository.UpdateStockAsync(id, stockDto);
        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock.ToStackDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DelteStock([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var stock = _stockRepository.DeleteStockAsync(id);
        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }
    
    
}
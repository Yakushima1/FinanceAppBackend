using FinanceApp.Dtos.Comment;
using FinanceApp.Extensions;
using FinanceApp.Interfaces;
using FinanceApp.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    private readonly UserManager<AppUser> _userManager;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository,
        UserManager<AppUser> userManager)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<List<Comment>> GetAll()
    {
        return await _commentRepository.GetCommentsAsync();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(id);
        return Ok(comment);
    }
    
    [HttpPost]
    [Route("{stockId:int}")]
    public async Task<IActionResult> createComment([FromRoute] int stockId,[FromBody]CreateCommentDto commentDto)
    {
        var username = User.GetUsername();
        var appuser = await _userManager.FindByNameAsync(username);
        
        var comment = commentDto.ToCommentFromCreateCommentDto(stockId);
        
        comment.AppUserId = appuser.Id;
        await _commentRepository.CreateComment(comment);
        return Ok(comment);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    { 
        var data = await _commentRepository.DeleteComment(id);
        return Ok(data);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, 
        [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
    {
        var comment = updateCommentRequestDto.ToCommentFromUpdateCommentDto(id);
        await _commentRepository.UpdateCommentAsync(id,comment);
        return Ok(comment);

    }
}
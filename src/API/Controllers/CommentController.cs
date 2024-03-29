﻿using Business.Services.CommentService;
using Business.Services.CommentService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService) => 
        _commentService = commentService;

    // GET
    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _commentService.GetAsync(id);
        if(response == null) return NotFound();

        return Ok(response);
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var responseList = await _commentService.GetAllAsync();

        return Ok(responseList);
    }
    [HttpGet("~/api/Users/{userId:int:min(1)}/Comments")]
    public async Task<IActionResult> GetAllByUser(int userId)
    {
        var responseList = await _commentService.GetAllByUserIdAsync(userId);

        return Ok(responseList);
    }
    [HttpGet("~/api/Posts/{id:int:min(1)}/Comments")]
    public async Task<IActionResult> GetAllByPost(int id)
    {
        var responseList = await _commentService.GetAllByPostIdAsync(id);

        return Ok(responseList);
    }
    [HttpGet("{userId:int:min(1)}/Children")]
    public async Task<IActionResult> GetChildren(int userId)
    {
        var responseList = await _commentService.GetChildrenAsync(userId);

        return Ok(responseList);
    }
    [HttpGet("{id:int:min(1)}/IsExist")]
    public async Task<IActionResult> IsExist(int id)
    {
        var response = await _commentService.IsExistAsync(id);

        return Ok(response);
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Add(AddCommentRequest request)
    {
        var affectedRows = await _commentService.AddAsync(request);

        return Ok(affectedRows);
    }

    // TODO : Update method must implemented

    // PATCH
    [HttpPatch]
    public async Task<IActionResult> UpdateContent(UpdateCommentContentRequest request)
    {
        var affectedRows = await _commentService.UpdateContentAsync(request);

        return Ok(affectedRows);
    }
    [HttpPatch("React")]
    public async Task<IActionResult> React(ReactionCommentRequest request)
    {
        var affectedRows = await _commentService.ReactAsync(request);

        return Ok(affectedRows);
    }

    // DELETE
    [Authorize]
    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var affectedRows = await _commentService.DeleteAsync(id);

        return Ok(affectedRows);
    }
}


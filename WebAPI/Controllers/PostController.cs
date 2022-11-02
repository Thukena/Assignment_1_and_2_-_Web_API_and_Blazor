﻿using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    
        private readonly IPostLogic postLogic;

        public PostController(IPostLogic postLogic)
        {
            this.postLogic = postLogic;
        }
 
        [HttpPost]
        public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto)
        {
        
            try
            {
                Post post = await postLogic.CreateAsync(dto);
                return Created($"/posts/{post.Id}", post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? title)
        {
            try
            {
                PostSearchParametersDto parameters = new(title);
                var posts = await postLogic.GetAsync(parameters);
                return Ok(posts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
}
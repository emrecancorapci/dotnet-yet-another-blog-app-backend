﻿using System.ComponentModel.DataAnnotations;

namespace Business.Services.PostService.Dtos;

public class UpdatePostContentRequest
{
    [Required]
    public int PostId { get; set; }
    
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    [Required]
    [MinLength(32)]
    public string Content { get; set; }
    
    public string? ThumbnailUrl { get; set; }
    public string? PostSummary { get; set; }

    public int CategoryId { get; set; }

    public bool CommentsEnabled { get; set; }
    public bool ReactionsEnabled { get; set; }

    [Required]
    public int EditorId { get; set; }
    public string? EditionSummary { get; set; }
}
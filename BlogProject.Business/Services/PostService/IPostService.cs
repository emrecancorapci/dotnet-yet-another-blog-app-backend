﻿using BlogProject.Business.Services.PostService.Dtos;
using BlogProject.Entities.Base;

namespace BlogProject.Business.Services.PostService;

public interface IPostService
{
    Task<int> AddAsync(AddPostRequest request);
    Task<int> UpdateAsync(Post post);
    Task<int> UpdateContentAsync(UpdatePostContentRequest request);
    Task<int> DeleteAsync(int id);
    Task<bool> IsExistAsync(int postId);
    
    Task<GetPostResponse?> GetAsync(int id);
    Task<IList<GetPostResponse>> GetAllAsync();
    Task<IList<GetPostResponse>> GetAllByEditorIdAsync(int editorId);
    Task<IList<GetPostResponse>> GetAllByUserIdAsync(int userId);
    Task<IList<GetPostResponse>> GetAllByTagIdAsync(int tagId);
    Task<IList<GetPostResponse>> GetAllByCategoryIdAsync(int categoryId);
    Task<IList<GetPostResponse>> GetAllBySearchAsync(string search);

    Task<int> ReactAsync(ReactionPostRequest request);
    
    Task<bool> DeleteEditorRelation(int postId, int editorId);
    Task<int> DeleteRelationByEditorIdAsync(int editorId);

    // Temporary
    Task<int> AddTagAsync(string name, string? desc);
    Task<int> AddCategoryAsync(string name, string? desc);

}

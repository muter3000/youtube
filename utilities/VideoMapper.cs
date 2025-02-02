﻿using TAIBackend.Model;

namespace TAIBackend.Utilities;

public static class VideoMapper
{
    public static object Map(Video video)
    {
        return new
        {
            id = video.Id,
            userId = video.OwneraccountId,
            userFullName = video.Owneraccount.Fullname,
            profilePictureSrc = video.Owneraccount.ProfilePicUrl,
            title = video.Title,
            description = video.Description,
            category = video.Category,
            createdAt = video.CreatedAt.ToUniversalTime(),
            views = video.Views,
            thumbnailSrc = $"/videos/{video.Id}/thumbnail.jpg"
        };
    }
}
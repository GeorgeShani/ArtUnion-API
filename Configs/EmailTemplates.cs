using System.Text;
using ArtUnion_API.Models;

namespace ArtUnion_API.Configs;

public static class EmailTemplates
{
    public static string VerifyEmail(User user)
    {
        return $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="preconnect" href="https://fonts.googleapis.com">
          <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
          <link href="https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&family=Playfair+Display:ital,wght@0,400..900;1,400..900&display=swap" rel="stylesheet">
          <title>Verify Your ArtUnion Account</title>
        </head>
        <body style="margin: 0; padding: 0; font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif; line-height: 1.6; color: #333333; background-color: #f5f5f5;">
          <div style="max-width: 600px; margin: 0 auto; background-color: #fcfaff; border-left: 4px solid #a78bfa; border-radius: 0; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);">
            <div style="padding: 25px 20px; text-align: center; background-color: #f3e8ff; border-bottom: 2px solid #e9d5ff;">
              <h1 style="font-family: 'Playfair Display', sans-serif; font-size: 26px; font-weight: bold; color: #6d28d9; margin: 0; font-style: italic;">Art<span style="color: #7c3aed; font-style: italic;">Union</span></h1>
            </div>
            <div style="padding: 30px 20px; background-color: #fcfaff; background-image: url('https://www.transparenttextures.com/patterns/watercolor.png');">
              <h1 style="font-family: 'Playfair Display', sans-serif; color: #4c1d95; font-size: 24px; margin-top: 0; margin-bottom: 20px; border-bottom: 1px solid #e9d5ff; padding-bottom: 10px;">Verify Your ArtUnion Account</h1>
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello {user.FirstName} {user.LastName},</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">Thank you for joining our creative community! To complete your registration and start your artistic journey, please use the verification code below:</p>
              <div style="text-align: center;">
                <div style="display: inline-block; padding: 12px 24px; background-color: #f5f3ff; color: #4c1d95; font-family: monospace; font-size: 24px; font-weight: bold; border-radius: 0; margin: 20px 0; letter-spacing: 5px; border-left: 4px solid #a78bfa;">{user.VerificationCode}</div>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">This code will expire in 24 hours for security reasons.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">If you didn't sign up for an ArtUnion account, you can safely ignore this email.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">We're excited to see what you'll create!</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">- The ArtUnion Team</p>
            </div>
            <div style="padding: 20px; text-align: center; background-color: #f3e8ff; color: #6b7280; font-size: 12px; border-top: 2px solid #e9d5ff;">
              <div style="margin-bottom: 10px;">
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Instagram</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Twitter</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Facebook</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">© 2025 ArtUnion. All rights reserved.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">248 Valencia Street, San Francisco, CA 94103</p>
              <p style="margin-bottom: 16px; line-height: 1.7;"><a href="#" style="color: #9ca3af; text-decoration: underline;">Unsubscribe</a> from these emails.</p>
            </div>
          </div>
        </body>
        </html>
        """;
    }

    public static string NewCommentNotification(User artist, User commenter, Artwork artwork, Critique comment)
    { 
        return $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="preconnect" href="https://fonts.googleapis.com">
          <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
          <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:ital,wght@0,400;0,700;1,400&family=Montserrat:wght@400;500;700&display=swap" rel="stylesheet">
          <title>New Comment on "{artwork.Title}" - ArtUnion</title>
        </head>
        <body style="margin: 0; padding: 0; font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif; line-height: 1.6; color: #333333; background-color: #f5f5f5;">
          <div style="max-width: 600px; margin: 0 auto; background-color: #ffffff; border-top: 4px solid #8b5cf6; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);">
            <div style="padding: 25px 20px; text-align: center; background-color: #8b5cf6; border-bottom: none;">
              <h1 style="font-family: 'Montserrat', sans-serif; font-size: 26px; font-weight: bold; color: #ffffff; margin: 0;">Art<span style="color: #e9d5ff;">Union</span></h1>
            </div>
            <div style="padding: 30px 20px; background-color: #ffffff;">
              <h1 style="font-family: 'Playfair Display', sans-serif; color: #6d28d9; font-size: 24px; margin-top: 0; margin-bottom: 20px; letter-spacing: 0.5px;">New Comment on Your Artwork</h1>
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello {artist.FirstName} {artist.LastName},</p>
              <p style="margin-bottom: 16px; line-height: 1.7;"><strong>{commenter.FirstName} {commenter.LastName}</strong> just left a comment on your artwork <strong>"{artwork.Title}"</strong>:</p>
              <div style="padding: 15px; background-color: #ede9fe; border-radius: 8px; margin: 20px 0; border-left: 4px solid #8b5cf6;">
                <p style="font-style: italic; margin: 0; line-height: 1.7;">"{comment.Comment}"</p>
              </div>
              <div style="margin-bottom: 30px; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 12px rgba(139, 92, 246, 0.15);">
                <img src="{artwork.ImageUrl}" alt="{artwork.Title}" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px; background-color: #fcfaff;">
                  <h3 style="font-family: 'Playfair Display', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">{artwork.Title}</h3>
                  <p style="font-size: 14px; color: #7c3aed; margin: 0; font-weight: 500;">by {artist.FirstName} {artist.LastName}</p>
                </div>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">Continue the conversation and respond to this feedback:</p>
              <div style="text-align: center; margin: 30px 0;">
                <a href="#" style="display: inline-block; padding: 12px 24px; background-color: #7c3aed; color: #ffffff !important; text-decoration: none; border-radius: 4px; font-weight: bold; text-align: center; text-transform: uppercase; letter-spacing: 1px; font-size: 14px;">View Comment</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">Keep creating!</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">- The ArtUnion Team</p>
            </div>
            <div style="padding: 20px; text-align: center; background-color: #f3e8ff; color: #6b7280; font-size: 12px;">
              <div style="margin-bottom: 10px;">
                <a href="#" style="display: inline-block; margin: 0 5px; color: #7c3aed; text-decoration: none; font-weight: 500;">Instagram</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #7c3aed; text-decoration: none; font-weight: 500;">Twitter</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #7c3aed; text-decoration: none; font-weight: 500;">Facebook</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">© 2025 ArtUnion. All rights reserved.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">248 Valencia Street, San Francisco, CA 94103</p>
              <p style="margin-bottom: 16px; line-height: 1.7;"><a href="#" style="color: #9ca3af; text-decoration: underline;">Unsubscribe</a> from these emails.</p>
            </div>
          </div>
        </body>
        </html>    
        """;
    }
    
    public static string NewArtworkNotification(User user, User artist, Artwork artwork)
    { 
        return $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="preconnect" href="https://fonts.googleapis.com">
          <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
          <link href="https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&family=Playfair+Display:ital,wght@0,400..900;1,400..900&display=swap" rel="stylesheet">
          <title>{artist.FirstName} {artist.LastName} Just Shared New Artwork - ArtUnion</title>
        </head>
        <body style="margin: 0; padding: 0; font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif; line-height: 1.6; color: #333333; background-color: #f5f5f5;">
          <div style="max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);">
            <div style="padding: 15px 20px; text-align: center; background-color: #f9fafb; border-bottom: 1px solid #f0f0f0;">
              <h1 style="font-family: 'Montserrat', sans-serif; font-size: 22px; font-weight: bold; color: #6d28d9; margin: 0;">Art<span style="color: #7c3aed;">Union</span></h1>
            </div>
            <div style="padding: 30px 20px; background-color: #ffffff;">
              <h1 style="font-family: 'Montserrat', sans-serif; color: #4c1d95; font-size: 20px; margin-top: 0; margin-bottom: 20px;">{artist.FirstName} {artist.LastName} Just Published New Artwork</h1>
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello {user.FirstName} {user.LastName},</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">An artist you follow has just shared a new masterpiece with the world!</p>
              <div style="margin-bottom: 30px; border: 1px solid #f3f4f6; border-radius: 8px; overflow: hidden;">
                <img src="{artwork.ImageUrl}" alt="{artwork.Title}" style="width: 100%; max-height: 400px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">{artwork.Title}</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by {artist.FirstName} {artist.LastName}</p>
                  <p style="margin-top: 10px; line-height: 1.7;">{artwork.Description}</p>
                </div>
              </div>
              <div style="text-align: center; margin: 30px 0;">
                <a href="#" style="display: inline-block; padding: 12px 24px; background-color: #8b5cf6; color: #ffffff !important; text-decoration: none; border-radius: 4px; font-weight: bold; text-align: center;">View Artwork</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">Discover more inspiring creations from your favorite artists on ArtUnion.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">- The ArtUnion Team</p>
            </div>
            <div style="padding: 20px; text-align: center; background-color: #f9fafb; color: #6b7280; font-size: 12px;">
              <div style="margin-bottom: 10px;">
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Instagram</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Twitter</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Facebook</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">© 2025 ArtUnion. All rights reserved.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">248 Valencia Street, San Francisco, CA 94103</p>
              <p style="margin-bottom: 16px; line-height: 1.7;"><a href="#" style="color: #9ca3af; text-decoration: underline;">Unsubscribe</a> from these emails.</p>
            </div>
          </div>
        </body>
        </html>
        """;
    }

    public static string WeeklyDigest(string recipientName, string weekRange, List<Artwork> artworks)
    {
        var artworksHtml = new StringBuilder();

        foreach (var art in artworks)
        {
            var artworkAvgRating = art.Critiques!.Count > 0 ? art.Critiques!.Average(critique => critique.Rating) : 0;
          
            artworksHtml.Append($"""
            <div style="margin-bottom: 30px; border: 1px solid #f0f0f0; border-radius: 8px; overflow: hidden;">
                <img src="{art.ImageUrl}" alt="{art.Title}" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">{art.Title}</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by {art.Artist!.FirstName} {art.Artist.LastName}</p>
                  <p style="margin-top: 10px; line-height: 1.7;">❤️ {art.Likes!.Count} likes &nbsp; 💬 {art.Critiques!.Count} comments &nbsp; ⭐ {artworkAvgRating}</p>
                </div>
            </div>
            """);
        }

        return $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="preconnect" href="https://fonts.googleapis.com">
          <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
          <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:ital,wght@0,400;0,700;1,400&family=Montserrat:wght@400;500;700&display=swap" rel="stylesheet">
          <title>Weekly Art Digest - ArtUnion</title>
        </head>
        <body style="margin: 0; padding: 0; font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif; line-height: 1.6; color: #333333; background-color: #f5f5f5;">
          <div style="max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);">
            <div style="padding: 25px 20px; text-align: center; background-color: #ffffff; border-bottom: 1px solid #f0f0f0;">
              <h1 style="font-family: 'Montserrat', sans-serif; font-size: 26px; font-weight: bold; color: #6d28d9; margin: 0;">Art<span style="color: #7c3aed;">Union</span></h1>
            </div>
            <div style="padding: 30px 20px; background-color: #ffffff;">
              <h1 style="font-family: 'Montserrat', sans-serif; color: #4c1d95; font-size: 24px; margin-top: 0; margin-bottom: 20px;">Your Weekly Art Digest</h1>
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello {recipientName},</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">Here's your curated selection of trending artworks for the week of {weekRange}:</p>
              {artworksHtml}
              <div style="text-align: center; margin: 30px 0;">
                <a href="#" style="display: inline-block; padding: 12px 24px; background-color: #6d28d9; color: #ffffff !important; text-decoration: none; border-radius: 4px; font-weight: bold; text-align: center;">Explore More Art</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">Stay inspired!</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">- The ArtUnion Team</p>
            </div>
            <div style="padding: 20px; text-align: center; background-color: #f9fafb; color: #6b7280; font-size: 12px;">
              <div style="margin-bottom: 10px;">
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Instagram</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Twitter</a>
                <a href="#" style="display: inline-block; margin: 0 5px; color: #6d28d9; text-decoration: none;">Facebook</a>
              </div>
              <p style="margin-bottom: 16px; line-height: 1.7;">© 2025 ArtUnion. All rights reserved.</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">248 Valencia Street, San Francisco, CA 94103</p>
              <p style="margin-bottom: 16px; line-height: 1.7;"><a href="#" style="color: #9ca3af; text-decoration: underline;">Unsubscribe</a> from these emails.</p>
            </div>
          </div>
        </body>
        </html>
        """;
    }
}
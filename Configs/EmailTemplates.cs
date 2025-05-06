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

    public static string NewCommentNotification()
    { 
        return """
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="preconnect" href="https://fonts.googleapis.com">
          <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
          <link href="https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&family=Playfair+Display:ital,wght@0,400..900;1,400..900&display=swap" rel="stylesheet">
          <title>David Kim Just Shared New Artwork - ArtUnion</title>
        </head>
        <body style="margin: 0; padding: 0; font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif; line-height: 1.6; color: #333333; background-color: #f5f5f5;">
          <div style="max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);">
            <div style="padding: 15px 20px; text-align: center; background-color: #f9fafb; border-bottom: 1px solid #f0f0f0;">
              <h1 style="font-family: 'Montserrat', sans-serif; font-size: 22px; font-weight: bold; color: #6d28d9; margin: 0;">Art<span style="color: #7c3aed;">Union</span></h1>
            </div>
            <div style="padding: 30px 20px; background-color: #ffffff;">
              <h1 style="font-family: 'Montserrat', sans-serif; color: #4c1d95; font-size: 20px; margin-top: 0; margin-bottom: 20px;">David Kim Just Published New Artwork</h1>
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello Jamie Taylor,</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">An artist you follow has just shared a new masterpiece with the world!</p>
              <div style="margin-bottom: 30px; border: 1px solid #f3f4f6; border-radius: 8px; overflow: hidden;">
                <img src="https://images.unsplash.com/photo-1582201942988-13e60e4556ee?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2670&q=80" alt="Ethereal Dreams" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">Ethereal Dreams</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by David Kim</p>
                  <p style="margin-top: 10px; line-height: 1.7;">A surrealist exploration of dreamscapes and subconscious symbolism using mixed media techniques.</p>
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

    public static string NewArtworkNotification()
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
        </head>
        <body style="margin: 0; padding: 0; font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif; line-height: 1.6; color: #333333; background-color: #f5f5f5;">
          <div style="max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);">
            <div style="padding: 15px 20px; text-align: center; background-color: #f9fafb; border-bottom: 1px solid #f0f0f0;">
              <h1 style="font-family: 'Montserrat', sans-serif; font-size: 22px; font-weight: bold; color: #6d28d9; margin: 0;">Art<span style="color: #7c3aed;">Union</span></h1>
            </div>
            <div style="padding: 30px 20px; background-color: #ffffff;">
              <h1 style="font-family: 'Montserrat', sans-serif; color: #4c1d95; font-size: 20px; margin-top: 0; margin-bottom: 20px;">David Kim Just Published New Artwork</h1>
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello Jamie Taylor,</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">An artist you follow has just shared a new masterpiece with the world!</p>
              <div style="margin-bottom: 30px; border: 1px solid #f3f4f6; border-radius: 8px; overflow: hidden;">
                <img src="https://images.unsplash.com/photo-1582201942988-13e60e4556ee?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2670&q=80" alt="Ethereal Dreams" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">Ethereal Dreams</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by David Kim</p>
                  <p style="margin-top: 10px; line-height: 1.7;">A surrealist exploration of dreamscapes and subconscious symbolism using mixed media techniques.</p>
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

    public static string WeeklyDigest()
    {
        return """
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
              <p style="margin-bottom: 16px; line-height: 1.7;">Hello Riley Morgan,</p>
              <p style="margin-bottom: 16px; line-height: 1.7;">Here's your curated selection of trending artworks for the week of May 1 - May 7, 2025:</p>
              <div style="margin-bottom: 30px; border: 1px solid #f0f0f0; border-radius: 8px; overflow: hidden;">
                <img src="https://images.unsplash.com/photo-1541961017774-22349e4a1262?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2658&q=80" alt="Cosmic Journey" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">Cosmic Journey</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by Elena Petrova</p>
                  <p style="margin-top: 10px; line-height: 1.7;">❤️ 342 likes</p>
                </div>
              </div>
              <div style="margin-bottom: 30px; border: 1px solid #f0f0f0; border-radius: 8px; overflow: hidden;">
                <img src="https://images.unsplash.com/photo-1513519245088-0e12902e5a38?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2670&q=80" alt="Autumn Whispers" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">Autumn Whispers</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by Jackson Lee</p>
                  <p style="margin-top: 10px; line-height: 1.7;">❤️ 287 likes</p>
                </div>
              </div>
              <div style="margin-bottom: 30px; border: 1px solid #f0f0f0; border-radius: 8px; overflow: hidden;">
                <img src="https://images.unsplash.com/photo-1547891654-e66ed7ebb968?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2670&q=80" alt="Urban Solitude" style="width: 100%; max-height: 300px; object-fit: cover;">
                <div style="padding: 15px;">
                  <h3 style="font-family: 'Montserrat', sans-serif; font-size: 18px; font-weight: bold; color: #4c1d95; margin: 0 0 5px 0;">Urban Solitude</h3>
                  <p style="font-size: 14px; color: #6b7280; margin: 0;">by Olivia Martinez</p>
                  <p style="margin-top: 10px; line-height: 1.7;">❤️ 245 likes</p>
                </div>
              </div>
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
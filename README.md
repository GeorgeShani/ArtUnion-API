# 🎨 ArtUnion API

ArtUnion is a comprehensive ASP.NET Web API platform that connects artists and critics, enabling artists to showcase their work and receive professional feedback from critics.

## 📋 Project Overview

ArtUnion API serves as the backend for a portfolio and criticism platform that allows:
- 👨‍🎨 Artists to upload and manage their artwork in personal portfolios
- 🧐 Critics to provide professional feedback and ratings on artworks
- 👥 Users to follow their favorite artists and receive updates
- 👮‍♂️ Administrators to manage the platform's content and categories

## 💻 Tech Stack

- **Framework**: ASP.NET Core (.NET 6+)
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Email Service**: SMTP integration
- **Background Jobs**: 🔄 Hangfire for automated tasks
- **Cloud Services**: 
  - ☁️ AWS RDS for database
  - 📂 AWS S3 for artwork image storage
- **Design Patterns**: Repository Pattern
- **Tools**:
  - 🔄 AutoMapper for object mapping
  - ✅ FluentValidation for Model validation
  - 📄 Pagination for large data sets
  - 🛡️ Proper error handling and logging

## ✨ Core Features

### 👤 User Management
- Multiple user roles: Artist, Critic, Admin
- User registration and authentication
- Profile management

### 🖼️ Portfolio & Artwork Management
- Artists can create and manage portfolios
- Upload artwork with details (title, description, category)
- Organize artwork by categories
- ❤️ Like/unlike artwork functionality

### 📝 Criticism System
- Critics can provide ratings and textual feedback on artwork
- Artists receive notifications about new critiques

### 🔔 Subscription System
- Users can follow artists they like
- Followers receive notifications about new artwork from artists they follow

### 📧 Email Notifications
- Registration confirmation
- New criticism alerts
- New artwork notifications
- Weekly digest of popular works (automated via Hangfire)

### 🗄️ Storage
- Artwork images stored in AWS S3
- Database hosted on AWS RDS

## 🔌 API Endpoints

### 🔐 Authentication
- `POST /api/auth/register` - Register as Artist or Critic
- `POST /api/auth/login` - Login and receive JWT token

### 👥 Users
- `GET /api/users` - List all users (Admin only)
- `GET /api/users/{id}` - Get specific user information
- `PUT /api/users/{id}` - Update user information
- `DELETE /api/users/{id}` - Delete user (Admin only)

### 📁 Portfolios
- `GET /api/portfolios` - List all portfolios
- `GET /api/portfolios/{id}` - View specific portfolio
- `POST /api/portfolios` - Create new portfolio (Artist only)
- `PUT /api/portfolios/{id}` - Update portfolio (Owner only)
- `DELETE /api/portfolios/{id}` - Delete portfolio (Owner/Admin)

### 🖼️ Artworks
- `GET /api/artworks` - List artworks with filtering
- `GET /api/artworks/{id}` - View specific artwork
- `POST /api/artworks` - Add new artwork (Artist only)
- `PUT /api/artworks/{id}` - Update artwork (Owner only)
- `DELETE /api/artworks/{id}` - Delete artwork (Owner/Admin)
- `GET /api/artworks/category/{categoryId}` - Get artworks by category
- `POST /api/artworks/{artworkId}/like` - Like/unlike an artwork

### 🏷️ Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get specific category
- `POST /api/categories` - Add new category (Admin only)
- `PUT /api/categories/{id}` - Update category (Admin only)
- `DELETE /api/categories/{id}` - Delete category (Admin only)

### 📝 Critiques
- `GET /api/critiques/artwork/{artworkId}` - Get reviews for specific artwork
- `POST /api/critiques` - Add review (Critic only)
- `PUT /api/critiques/{id}` - Update review (Owner only)
- `DELETE /api/critiques/{id}` - Delete review (Owner/Admin)

### 🔔 Subscriptions
- `GET /api/subscriptions/followers/{artistId}` - Get artist's followers
- `GET /api/subscriptions/following` - Get artists followed by current user
- `POST /api/subscriptions/{artistId}` - Follow an artist
- `DELETE /api/subscriptions/{artistId}` - Unfollow an artist

## 🗂️ Data Models

### 👤 User
- User information (name, email, password hash, bio, profile image)
- Role (Artist, Critic, Admin)

### 📁 Portfolio
- Title
- Description
- Creation date
- Owner (Artist)

### 🖼️ Artwork
- Title
- Description
- Category
- Creation date
- Image URL
- Portfolio reference

### 🏷️ Category
- Name
- Description

### 📝 Critique
- Rating
- Comment text
- Creation date
- Critic reference
- Artwork reference

### 🔔 Subscription
- Follower reference
- Artist reference
- Subscription date

### ❤️ ArtworkLike
- User reference
- Artwork reference
- Liked timestamp

## 🔒 Authorization Rules

- API access requires authentication
- Critique creation limited to users with Critic role
- Artwork upload limited to users with Artist role
- Category management limited to users with Admin role
- Content modification restricted to content owners or administrators

## 🚀 Getting Started

### Prerequisites
- .NET 6 SDK or later
- AWS account with S3 and RDS access
- SMTP server access for email notifications

### Installation

1. Clone the repository:
```
git clone https://github.com/GeorgeShani/ArtUnion-API.git
```

2. Navigate to the project directory:
```
cd ArtUnion-API
```

3. Restore NuGet packages:
```
dotnet restore
```

4. Create a `.env` file with your environment variables.

5. Apply migrations to create the database:
```
dotnet ef database update
```

6. Run the application:
```
dotnet run
```

## ⚙️ Configuration

The application uses environment variables (.env file) for configuration. Key settings include:

- 🗄️ Database connection information (AWS RDS)
- 🔐 JWT authentication settings
- 📧 SMTP configuration for email notifications
- 📂 AWS S3 credentials for artwork storage
- 🔄 Hangfire configuration for background jobs

## 🔄 Background Tasks

The application uses Hangfire to schedule and execute background tasks, including:

- 📅 Weekly digest emails of popular artworks
- 📧 Automated email notifications

## 📞 Contact

For any questions or suggestions regarding this project, please contact:
- Giorgi Shanidze - [GitHub Profile](https://github.com/GeorgeShani)

# Amazon Review Generator

This project is a Markov Chain-based Amazon review generator. It includes functionality to train a Markov Chain model on a set of sentences and generate text based on the trained model. Additionally, it can generate reviews with a template and determine star ratings based on the content of the review.

## Features

- Train a Markov Chain model with a set of sentences.
- Generate text based on the trained Markov Chain model.
- Generate reviews using a predefined template.
- Determine star ratings for reviews based on positive and negative words.

## Technologies Used
  
- Backend and Frontend
- ASP.NET Core 6.0 MVC: For the API, business logic, and user interface.
- C#: Language for implementing the backend and front-end logic.
- Bootstrap: For responsive styling.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio or any other C# compatible IDE

### Installation

- Clone the repository: git clone https://github.com/saoriol/LoyalTechnicalAssessment.git
- Navigate to the project directory: cd AmazonReviewGenerator

### Setup and Run the Project

- Navigate to the project directory: cd AmazonReviewGenerator.Api
- Restore dependencies: dotnet restore
- Run the application: dotnet run
- The application will be available at: http://localhost:44360

### Usage

- Open your web browser and navigate to: http://localhost:44360
- Click "Generate Review" to generate a new review.
- The generated review and star rating will appear in the Generated Review section.
- View previously generated reviews in the Review History section.
- Run the tests to ensure everything is working correctly.

### Features Overview

- Generate Review
  - Button: Click "Generate Review" to call the /api/generate endpoint.
  - Displayed Information:
    - A randomly generated review (e.g., "This is a fantastic product!")
    - A randomized star rating (1-5 stars).
- Review History
  - Displays: A list of previously generated reviews with their corresponding star ratings.
   Dynamic Updates: Each newly generated review is added to the history in real-time.

### API Endpoints
- The app uses an API endpoint internally for generating reviews.
  - Generate a Review
    - Endpoint: /api/generate
    - Method: GET
    - Response:
      `{
        "review": "This is a fantastic product! Highly recommend.",
        "starRating": 5
      }`
      
### Running Tests

The project includes unit tests to verify the functionality of the Markov Chain generator and data loader. To run the tests, use the Test Explorer in Visual Studio or run the following command in the terminal:


## Project Structure

- `AmazonReviewGenerator.API`: Contains the API and front-end view.
- `AmazonReviewGenerator.Business`: Contains the core business logic for the Markov Chain generator.
- `AmazonReviewGenerator.Data`: Contains the data loading logic.
- `AmazonReviewGenerator.Tests`: Contains unit tests for the business and data layers.

## Contributors

Stanley A. Oriol

## License

This project is licensed under the MIT License. 


  
    

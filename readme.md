# SatooxApi - A .Net minimal api for portfolio
Welcome to the SatooxApi project! This minimal API project is designed to help you build a powerful web API for your portfolio. There is tw react project namely protifilo and cms which will be used as forntend with tailwindcss integrated and more comming soon. Below, you'll find instructions on setting up the project and running it using Docker Compose.

# Prerequisites
Before you begin, make sure you have the following installed on your machine:

1. [Docker](https://www.docker.com/) - To run the application using Docker Compose.

# Getting Started
1. Clone this repository to your local machine.
```bash
git clone https://github.com/saimankhatiwada/SatooxApi.git
```
2. Inside `Backend` > `Api`, you will find a `.env.sample` file. Copy this file and rename it to `.env`. This file will contain environment-specific configurations for the application.

# Configuring Environment Variables
Open the `.env` file and update the values as per your environment requirements. The file will typically include settings like database connection string, JWT configuration as of now.

# Running the Application

1. Start the application using Docker Compose:
```bash
docker compose up
```

Docker Compose will now pull the necessary images and start the application with the configured settings. The API should now be accessible at http://localhost:5010 in your web browser.

# Forntend 

SatooxApi includes two frontend React projects, `portifilo` and `cms` which are integrated with Tailwind CSS for smooth styling and responsiveness. Both projects will be able to interact seamlessly with the API. They can be accessed via http://localhost:3000 and http://localhost:4000 respectively.

Stay tuned for upcoming updates and exciting features!

Feel free to reach out for any assistance or further information. Happy coding!

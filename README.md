# Weather API Project

Build a weather API that fetches and returns weather data from a 3rd party API. This project will help you understand how to work with 3rd party APIs, caching, and environment variables.
[Weather API LINK](https://roadmap.sh/projects/weather-api-wrapper-service)

![Weather API](https://assets.roadmap.sh/guest/weather-api-f8i1q.png)  

---

## Overview

Instead of relying on our own weather data, this project involves building a weather API that fetches and returns weather data from a 3rd party API. You can use your favorite weather API, such as [Visual Crossing’s API](https://www.visualcrossing.com/), which is free and easy to use.

---

## Key Features

1. **Fetch Weather Data**: Fetch weather data from a 3rd party API.
2. **Caching**: Use an in-memory cache like Redis to store weather data.
3. **Environment Variables**: Store sensitive information like API keys and Redis connection strings in environment variables.
4. **Error Handling**: Handle errors gracefully, such as when the 3rd party API is down or the city code is invalid.
5. **Rate Limiting**: Implement rate limiting to prevent abuse of your API.
---

## Suggested Tools and Libraries

- **Weather API**: [Visual Crossing’s API](https://www.visualcrossing.com/)
- **Caching**: Use Redis for in-memory caching.
  - Set expiration time for cache keys (e.g., 12 hours) using the `EX` flag in the `SET` command.
- **HTTP Requests**:
  - Node.js: Use the `axios` package.
  - Python: Use the `requests` module.
- **Rate Limiting**:
  - Node.js: Use the `express-rate-limit` package.
  - Python: Use the `flask-limiter` package.

---

## Steps to Build the API

**Create a Simple API**:
   - Start by creating a simple API that returns a hardcoded weather response. This will help you understand the structure and request handling.

**Integrate 3rd Party API**:
   - Replace the hardcoded response with real weather data fetched from the 3rd party API.

**Implement Caching**:
   - Use Redis to cache weather data. Use the city code as the key and set an expiration time for the cache.

**Use Environment Variables**:
   - Store the API key and Redis connection string in environment variables for security and flexibility.

**Handle Errors**:
   - Implement proper error handling for cases like API downtime or invalid city codes.

**Add Rate Limiting**:
   - Implement rate limiting to prevent abuse of your API.

---
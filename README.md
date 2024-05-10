# Post Service

The **Post Service** handles the creation, retrieval, updating, and deletion of posts within a social networking platform. It also enables authorized users to retrieve posts by specific users.

## Controller

### PostController

This controller manages posts and user-specific post operations. It includes secure and non-secure endpoints for CRUD (Create, Read, Update, Delete) operations.

- **Endpoints:**

  - `POST /post`  
    - **Description:** Creates a new post for an authenticated user.
    - **Authorization:** Requires a valid JWT token.
    - **Request Body:** 
      - `PostDTO` containing:
        - `Title`: The post title.
        - `Content`: The content of the post.
    - **Response Codes:**
      - `200`: Returns the created post.
      - `401`: Unauthorized, user ID not found in the token.

  - `GET /post/all`  
    - **Description:** Retrieves all posts in the platform.
    - **Response Codes:**
      - `200`: Returns an array of all posts.

  - `GET /post`  
    - **Description:** Retrieves all posts created by the authenticated user.
    - **Authorization:** Requires a valid JWT token.
    - **Response Codes:**
      - `200`: Returns an array of posts by the authenticated user.
      - `401`: Unauthorized, user ID not found in the token.

  - `GET /post/{id}`  
    - **Description:** Retrieves a specific post by its unique identifier.
    - **Path Parameter:** `id` representing the post's unique identifier.
    - **Authorization:** Requires a valid JWT token.
    - **Response Codes:**
      - `200`: Returns the post data.
      - `404`: Post not found.

  - `PUT /post/{id}`  
    - **Description:** Updates a post's title and/or content.
    - **Path Parameter:** `id` representing the post's unique identifier.
    - **Authorization:** Requires a valid JWT token.
    - **Request Body:** 
      - `Post` containing:
        - `Id`: The post ID (should match the `id` path parameter).
        - `Title`: The updated title.
        - `Content`: The updated content.
    - **Response Codes:**
      - `204`: Post updated successfully.
      - `400`: ID mismatch between path parameter and request body.
      - `403`: Forbidden, user cannot modify the post (unauthorized).
      - `404`: Post not found.

  - `DELETE /post/{id}`  
    - **Description:** Deletes a post by its unique identifier.
    - **Path Parameter:** `id` representing the post's unique identifier.
    - **Authorization:** Requires a valid JWT token.
    - **Response Codes:**
      - `204`: Post deleted successfully.
      - `403`: Forbidden, user cannot delete the post (unauthorized).
      - `404`: Post not found.

  - `POST /post/byUserIds`  
    - **Description:** Retrieves posts by a list of user IDs.
    - **Request Body:** A list of integers representing user IDs.
    - **Response Codes:**
      - `200`: Returns an array of posts matching the provided user IDs.
      - `400`: Bad request, user IDs list is missing or empty.
      - `404`: No posts found for the provided user IDs.

## Security

- **JWT Authentication:** Most endpoints require a valid JWT token to ensure only authenticated users can modify and access their own posts.

### About
The app is built using .NET 6 and SqLight local database. 
It allows adding simple weight and weight goal entries as well as listing the existing entries with a simple date filter.

### Running requirements
- Without Docker - locally installed .NET 6 SDK.
- With Docker - well, Docker. App tested with Docker Desktop for Windows.

### Running
- Building the image: run `docker build . -t weighttrack` in the repo root directory where the Dockerfile sits
- Running the image: run `docker run -d -p 8090:80 -e ASPNETCORE_ENVIRONMENT=Development weighttrack`. The environment is set to dev because it enables using the Swagger GUI for easy testing.
- Swagger GUI will be available in browser at `http://localhost:8090/swagger/index.html`


